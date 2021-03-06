using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.Experiment.Application.Cms.ContentRefresh;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Mapping;
using SFA.DAS.Experiment.Application.Cms.Services;

namespace SFA.DAS.Experiment.Application.Cms.UnitTests
{
    public class When_ContentRefresh_is_called
    {
        private ILogger<ContentRefreshHandler> _logger;
        private IContentService _contentService;

        private ICacheService _cacheService;

        private ContentRefreshHandler _handler;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<ContentRefreshHandler>>();
            _contentService = Substitute.For<IContentService>();

            _contentService.GetEntriesByType<Article>().Returns(new List<Article>(){
                new Article
                {
                    Sys = new Contentful.Core.Models.SystemProperties(){Id = "article1"}, 
                    Slug = "the-article-slug", 
                    Title = "", 
                    MetaDescription = "", 
                    PageTitle = "", 
                    HubType = "Apprentices", 
                    LandingPage = new LandingPage(){Sys = new Contentful.Core.Models.SystemProperties(){Id = "abc123"}}, 
                    Sections = new List<SectionBase>()
                },
                new Article
                {
                    Sys = new Contentful.Core.Models.SystemProperties(){Id = "article2"}, 
                    Slug = "another-article-slug", 
                    Title = "", 
                    MetaDescription = "", 
                    PageTitle = "", 
                    HubType = "Apprentices", 
                    LandingPage = new LandingPage(){Sys = new Contentful.Core.Models.SystemProperties(){Id = "abc123"}}, 
                    Sections = new List<SectionBase>()
                }
            });

            _contentService.GetEntry<LandingPage>("abc123").Returns(new LandingPage{Slug = "landing-page", Title = "Landing Page"}); 

            _cacheService = Substitute.For<ICacheService>();
            _handler = new ContentRefreshHandler(_logger, _contentService, _cacheService, new ArticleMapping(_contentService, _cacheService));
        }

        [Test]
        public async Task Then_the_articles_key_is_the_slug()
        {
            await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());
            await _cacheService.Received().Set("article_the-article-slug", Arg.Any<string>());
        }

        [Test]
        public async Task Then_existing_cache_keys_are_removed()
        {
            await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());

            await _cacheService.Received().ClearKeysStartingWith("article");
            await _cacheService.Received().ClearKeysStartingWith("articleSectionLookup");
        }

        [Test]
        public async Task Then_a_ContentRefreshResult_is_returned()
        {
            var result = await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());
            result.Should().BeOfType<ContentRefreshResponse>();
        }

        [Test]
        public async Task Then_a_ContentRefreshResult_is_returned_with_the_expected_result_data()
        {
            var result = await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());
            
            result.Success.Should().Be(true);
            result.ArticlesStored.Count().Should().Be(2);
            result.ArticlesStored[0].Should().Be("the-article-slug");
            result.ArticlesStored[1].Should().Be("another-article-slug");
        }

        [Test]
        public async Task And_an_error_occurs_Then_ContentRefreshResult_returns_success_false()
        {
            _cacheService.Set("article_the-article-slug", Arg.Any<string>()).Returns(x => {throw new System.Exception();});

            var result = await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());

            result.Success.Should().BeFalse();
            result.Exception.Should().BeOfType<System.Exception>(); 
        }

        [Test]
        public async Task Then_an_id_to_slug_reference_is_stored_for_each_article()
        {
            await _handler.Handle(new ContentRefreshRequest(), new System.Threading.CancellationToken());

            await _cacheService.Received().Set("articleIdSlugLookup_article1", "the-article-slug");
            await _cacheService.Received().Set("articleIdSlugLookup_article2", "another-article-slug");
        }
    }
}