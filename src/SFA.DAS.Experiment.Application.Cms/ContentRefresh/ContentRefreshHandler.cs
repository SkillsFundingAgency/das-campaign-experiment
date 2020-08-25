using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contentful.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Models;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.ContentRefresh
{
    public class ContentRefreshHandler : IRequestHandler<ContentRefreshRequest, ContentRefreshResult>
    {
        private readonly ILogger<ContentRefreshHandler> _logger;
        private readonly IContentService _contentService;
        private readonly ICacheService _cacheService;

        public ContentRefreshHandler(ILogger<ContentRefreshHandler> logger, IContentService contentService, ICacheService cacheService)
        {
            _logger = logger;
            _contentService = contentService;
            _cacheService = cacheService;
        }

        public async Task<ContentRefreshResult> Handle(ContentRefreshRequest request, CancellationToken cancellationToken)
        {
            try{
                await RemoveExistingKeys();

                var articles = await _contentService.GetEntriesByType<Article>();

                foreach (var article in articles)
                {
                    var page = await MapArticleToPage(article);
                    var pageJson = JsonConvert.SerializeObject(page);

                    await _cacheService.Set("article_" + article.Slug, pageJson);

                    _logger.LogInformation($"Stored {article.Slug} json");
                }

                return new ContentRefreshResult
                {
                    Success = true,
                    ArticlesStored = articles.Select(a => a.Slug).ToList()
                };
            }
            catch(Exception ex)
            {
                return new ContentRefreshResult
                {
                    Success = false,
                    Exception = ex
                };
            }

            
        }

        public async Task<Page<DomainArticle>> MapArticleToPage(Article contentfulArticle)
        {
            var htmlRenderer = new HtmlRenderer();
            var landingPage = await _contentService.GetEntry<LandingPage>(contentfulArticle.LandingPage.Sys.Id);

            var page = new Page<DomainArticle>
            {
                Slug = contentfulArticle.Slug,
                Title = contentfulArticle.Title,
                HubType = contentfulArticle.HubType,
                MetaDescription = contentfulArticle.MetaDescription,
                PageTitle = contentfulArticle.PageTitle,
                LandingPageSlug = landingPage.Slug,
                LandingPageTitle = landingPage.Title
            };

            var article = new DomainArticle
            {
                Hub = contentfulArticle.HubType,
                Slug = contentfulArticle.Slug,
                Title = contentfulArticle.Title,
                PageTitle = contentfulArticle.PageTitle,
                Sections = new List<DomainArticleSection>()
            };

            foreach (var contentfulInfoPageSection in contentfulArticle.Sections)
            {
                await UpdateArticleSectionLookup(contentfulInfoPageSection.Sys.Id, contentfulArticle.Sys.Id);
                var section = await _contentService.GetEntry<ArticleSection>(contentfulInfoPageSection.Sys.Id);

                var articleSection = new DomainArticleSection();
                articleSection.Title = section.Title;
                articleSection.Slug = section.Slug;
                articleSection.Body = new HtmlString(htmlRenderer.ToHtml(section.Body).Result);
                article.Sections.Add(articleSection);
            }

            page.Content = article;

            return page;
        }

        public async Task UpdateArticleSectionLookup(string articleSectionId, string articleId)
        {
            var lookupKey = $"articleSectionLookup_{articleSectionId}";

            ArticleSectionLookup lookup = await _cacheService.KeyExists(lookupKey) 
                ? JsonConvert.DeserializeObject<ArticleSectionLookup>(await _cacheService.Get(lookupKey)) 
                : new ArticleSectionLookup{ArticleIds = new List<string>()};
            
            if(!lookup.ArticleIds.Contains(articleId))
            {
                lookup.ArticleIds.Add(articleId);
            }

            await _cacheService.Set(lookupKey, JsonConvert.SerializeObject(lookup));
        }

        public async Task RemoveExistingKeys()
        {
            await _cacheService.ClearKeysStartingWith("article");
            await _cacheService.ClearKeysStartingWith("articleSectionLookup");
        }
    }

}
