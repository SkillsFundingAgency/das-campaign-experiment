using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Mapping;
using SFA.DAS.Experiment.Application.Cms.Models;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.ContentPublish
{
    public class ContentPublishHandler : IRequestHandler<ContentPublishRequest, ContentPublishResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly IContentService _contentService;
        private readonly ILogger<ContentPublishHandler> _logger;
        private readonly IArticleMapping _articleMapping;

        public ContentPublishHandler(ICacheService cacheService, IContentService contentService, ILogger<ContentPublishHandler> logger, IArticleMapping articleMapping)
        {
            _cacheService = cacheService;
            _contentService = contentService;
            _logger = logger;
            _articleMapping = articleMapping;
        }

        public async Task<ContentPublishResponse> Handle(ContentPublishRequest request, CancellationToken cancellationToken)
        {
            var response = new ContentPublishResponse();
            try
            {
                response.Success = true;
                response.ArticlesPublished = new List<string>();

                switch (request.EntryType)
                {
                    case "article":
                        response.ArticlesPublished.Add(await StorePublishedArticle(request.EntryId));
                        break;
                    case "articleSection":
                        response.ArticlesPublished.AddRange(await UpdateAssociatedArticles(request.EntryId));
                        break;
                    default:
                        response.Success = false;
                        response.Message = $"Unknown entry type {request.EntryType}";
                        return response;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                response.Success = false;
                response.Exception = ex;
            }
            
            return response;
        }

        private async Task<string> StorePublishedArticle(string entryId)
        {           
            var articleEntry = await _contentService.GetEntry<Article>(entryId);

            var page = _articleMapping.MapArticleToPage(articleEntry);
            var pageJson = JsonConvert.SerializeObject(page);

            await _cacheService.Set("article_" + articleEntry.Slug, pageJson);

            _logger.LogInformation($"Stored {articleEntry.Slug + " json"}");

            return articleEntry.Slug;
        }

        private async Task<List<string>> UpdateAssociatedArticles(string articleSectionId)
        {
            var articlesUpdated = new List<string>();
            var lookupKey = $"articleSectionLookup_{articleSectionId}";

            if(await _cacheService.KeyExists(lookupKey))
            {
                var lookup = JsonConvert.DeserializeObject<ArticleSectionLookup>(await _cacheService.Get(lookupKey));
                foreach (var articleId in lookup.ArticleIds)
                {
                    articlesUpdated.Add(await StorePublishedArticle(articleId));
                }
            }

            return articlesUpdated;
        }
    }
}