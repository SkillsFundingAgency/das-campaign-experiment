using System.Collections.Generic;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Models;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.Mapping
{
    public interface IArticleMapping
    {
        Task<Page<DomainArticle>> MapArticleToPage(Article article);
    }

    public class ArticleMapping : IArticleMapping
    {
        private readonly IContentService _contentService;
        private readonly ICacheService _cacheService;

        public ArticleMapping(IContentService contentService, ICacheService cacheService)
        {
            _contentService = contentService;
            _cacheService = cacheService;
        }

        public async Task<Page<DomainArticle>> MapArticleToPage(Article article)
        {
            var htmlRenderer = new HtmlRenderer();
            var landingPage = await _contentService.GetEntry<LandingPage>(article.LandingPage.Sys.Id);

            var page = new Page<DomainArticle>
            {
                Slug = article.Slug,
                Title = article.Title,
                HubType = article.HubType,
                MetaDescription = article.MetaDescription,
                PageTitle = article.PageTitle,
                LandingPageSlug = landingPage.Slug,
                LandingPageTitle = landingPage.Title
            };

            var domainArticle = new DomainArticle
            {
                Hub = article.HubType,
                Slug = article.Slug,
                Title = article.Title,
                PageTitle = article.PageTitle,
                Sections = new List<DomainArticleSection>()
            };

            foreach (var section in article.Sections)
            {
                await UpdateArticleSectionLookup(section.Sys.Id, article.Sys.Id);
                var contentSection = await _contentService.GetEntry<ArticleSection>(section.Sys.Id);

                var articleSection = new DomainArticleSection();
                articleSection.Title = contentSection.Title;
                articleSection.Slug = contentSection.Slug;
                articleSection.Body = new HtmlString(htmlRenderer.ToHtml(contentSection.Body).Result);
                domainArticle.Sections.Add(articleSection);
            }

            page.Content = domainArticle;

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
    }
}