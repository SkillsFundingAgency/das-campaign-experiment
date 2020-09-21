using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.ContentRemove
{
    public class ContentRemoveHandler : IRequestHandler<ContentRemoveRequest, ContentRemoveResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<ContentRemoveHandler> _logger;

        public ContentRemoveHandler(ICacheService cacheService, ILogger<ContentRemoveHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
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
            await _cacheService.Delete("articleCard_" + articleSlug);
            await _cacheService.Delete("articleIdSlugLookup_" + entryId);

            return articleSlug;
        }
    }
}