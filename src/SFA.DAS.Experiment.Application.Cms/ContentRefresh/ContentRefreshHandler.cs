using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Mapping;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.ContentRefresh
{
    public class ContentRefreshHandler : IRequestHandler<ContentRefreshRequest, ContentRefreshResponse>
    {
        private readonly ILogger<ContentRefreshHandler> _logger;
        private readonly IContentService _contentService;
        private readonly ICacheService _cacheService;
        private readonly IArticleMapping _articleMapping;

        public ContentRefreshHandler(ILogger<ContentRefreshHandler> logger, IContentService contentService, ICacheService cacheService, IArticleMapping articleMapping)
        {
            _logger = logger;
            _contentService = contentService;
            _cacheService = cacheService;
            _articleMapping = articleMapping;
        }

        public async Task<ContentRefreshResponse> Handle(ContentRefreshRequest request, CancellationToken cancellationToken)
        {
            try{
                await RemoveExistingKeys();

                var articles = await _contentService.GetEntriesByType<Article>();

                foreach (var article in articles)
                {
                    var page = await _articleMapping.MapArticleToPage(article);
                    var pageJson = JsonConvert.SerializeObject(page);

                    await _cacheService.Set("article_" + article.Slug, pageJson);
                    await _cacheService.Set("articleIdSlugLookup_" + article.Sys.Id, article.Slug);

                    _logger.LogInformation($"Stored {article.Slug} json");
                }

                return new ContentRefreshResponse
                {
                    Success = true,
                    ArticlesStored = articles.Select(a => a.Slug).ToList()
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ContentRefreshResponse
                {
                    Success = false,
                    Exception = ex
                };
            }   
        }

        public async Task RemoveExistingKeys()
        {
            await _cacheService.ClearKeysStartingWith("article");
            await _cacheService.ClearKeysStartingWith("articleSectionLookup");
        }
    }
}