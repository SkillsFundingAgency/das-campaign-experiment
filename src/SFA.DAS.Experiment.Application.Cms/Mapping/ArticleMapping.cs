using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Models;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.Mapping
{
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
                HubType = (HubType)Enum.Parse(typeof(HubType), article.HubType),
                MetaDescription = article.MetaDescription,
                PageTitle = article.PageTitle
            };

            var domainArticle = new DomainArticle
            {
                LandingPageSlug = landingPage.Slug,
                LandingPageTitle = landingPage.Title,
                Sections = new List<DomainArticleSection>(),
                Summary = article.Summary
            };

            foreach (var section in article.Sections)
            {
                await UpdateArticleSectionLookup(section.Sys.Id, article.Sys.Id);
                
                var articleSectionType = section.Sys.ContentType.SystemProperties.Id;

                if(articleSectionType == "articleTable")
                {
                    var contentArticleSection = await _contentService.GetEntry<ArticleTable>(section.Sys.Id);
                    
                    if(contentArticleSection != null)
                    {
                        var tableHtml = GetTableHtml((JProperty)contentArticleSection.Table.First);
                    
                        var articleSection = new DomainArticleSection();
                        articleSection.Title = contentArticleSection.Title;
                        articleSection.Body = new HtmlString(tableHtml);
                        domainArticle.Sections.Add(articleSection);
                    }
                }
                else
                {
                    var contentArticleSection = await _contentService.GetEntry<ArticleSection>(section.Sys.Id);
                    if(contentArticleSection != null)
                    {
                        var articleSection = new DomainArticleSection();
                        articleSection.Title = contentArticleSection.Title;
                        articleSection.Slug = contentArticleSection.Slug;
                        articleSection.Body = new HtmlString(htmlRenderer.ToHtml(contentArticleSection.Body).Result);
                        domainArticle.Sections.Add(articleSection);
                    }
                }
            }

            page.Content = domainArticle;

            var articleCard = new ArticleCard();
            articleCard.HubType = page.HubType;
            articleCard.LandingPageSlug = domainArticle.LandingPageSlug;
            articleCard.LandingPageTitle = domainArticle.LandingPageTitle;
            articleCard.Summary = domainArticle.Summary;
            articleCard.Title = page.Title;
            articleCard.Slug = page.Slug;

            await _cacheService.Set($"articleCard_{article.Slug}", JsonConvert.SerializeObject(articleCard));

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

        static string GetTableHtml(JProperty tableData)
        {
            var sb = new StringBuilder();

            sb.Append("<table class=\"fiu-table\">");

            var listOfRows = tableData.Children().First().Children();

            GetTableHead(sb, (JArray)listOfRows.First());
            GetTableBody(sb, listOfRows.Skip(1).ToList());
           
            sb.Append("</table>");
            
            return sb.ToString();
        }

        private static void GetTableBody(StringBuilder sb, List<JToken> bodyRows)
        {
            sb.Append("<tbody>");
            foreach (var row in bodyRows)
            {
                sb.Append("<tr>");
                foreach (string column in row)
                {
                    sb.Append("<td>" + column.Trim() + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
        }

        private static void GetTableHead(StringBuilder sb, JArray row)
        {
            sb.Append("<thead>");
            sb.Append("<tr>");
            foreach (string column in row)
            {
                sb.Append("<th>" + column.Trim() + "</th>");    
            }
            sb.Append("<tr>");
                    
            sb.Append("</thead>");
        }
    }
}