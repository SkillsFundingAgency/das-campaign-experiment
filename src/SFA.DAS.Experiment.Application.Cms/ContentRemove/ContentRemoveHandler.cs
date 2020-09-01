using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiment.Application.Cms.Mapping;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.ContentRemove
{
    public class ContentRemoveHandler : IRequestHandler<ContentRemoveRequest, ContentRemoveResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly IContentService _contentService;
        private readonly ILogger<ContentRemoveHandler> _logger;
        private readonly IArticleMapping _articleMapping;

        public ContentRemoveHandler(ICacheService cacheService, IContentService contentService, ILogger<ContentRemoveHandler> logger, IArticleMapping articleMapping)
        {
            _cacheService = cacheService;
            _contentService = contentService;
            _logger = logger;
            _articleMapping = articleMapping;
        }

        public async Task<ContentRemoveResponse> Handle(ContentRemoveRequest request, CancellationToken cancellationToken)
        {
            var response = new ContentRemoveResponse();
            try
            {
                response.Success = true;
                response.ArticlesRemoved = new List<string>();

                switch (request.EntryType)
                {
                    case "article":
                        response.ArticlesRemoved.Add(await RemoveArticle(request.EntryId));
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

        private async Task<string> RemoveArticle(string entryId)
        {           
            var articleSlug = await _cacheService.Get("articleIdSlugLookup_" + entryId);
            await _cacheService.Delete("article_" + articleSlug);
            await _cacheService.Delete("articleIdSlugLookup_" + entryId);

            return articleSlug;
        }
    }
}