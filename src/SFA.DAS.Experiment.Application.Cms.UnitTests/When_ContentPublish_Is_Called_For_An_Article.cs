using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.Experiment.Application.Cms.ContentPublish;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Mapping;
using SFA.DAS.Experiment.Application.Cms.Models;
using SFA.DAS.Experiment.Application.Cms.Services;

[TestFixture]
public class When_ContentPublish_Is_Called_For_An_Article
{
    private string _entryId;
    private ICacheService _cacheService;
    private IContentService _contentService;

    private ContentPublishHandler _handler;

    private string _expectedJson;

    [SetUp]
    public void SetUp()
    {
        _entryId = "articleId1";

        _cacheService = Substitute.For<ICacheService>();
        _contentService = Substitute.For<IContentService>();

        _contentService.GetEntry<Article>(_entryId).Returns(new Article{
            Sys = new SystemProperties{Id = "articleId1"},
            Slug = "article1Slug",
            Title = "Article Title",
            PageTitle = "Article Page Title",
            LandingPage = new LandingPage{Sys = new SystemProperties{Id = "landingPage1Id"}},
            MetaDescription = "Article meta",
            HubType = "Apprentices", 
            Sections = new List<ArticleSection>()
        });

        _contentService.GetEntry<LandingPage>("landingPage1Id").Returns(new LandingPage{Slug = "landing-page", Title = "Landing Page 1"}); 

        _expectedJson = JsonConvert.SerializeObject(new Page<DomainArticle>{
            Title = "Article Title",
            PageTitle = "Article Page Title", 
            Slug = "article1Slug",
            HubType = HubType.Apprentices,
            MetaDescription = "Article meta",
            Content = new DomainArticle{
                LandingPageSlug = "landing-page",
                LandingPageTitle = "Landing Page 1", 
                Sections = new List<DomainArticleSection>()
            }
        });

        _handler = new ContentPublishHandler(_cacheService, _contentService, Substitute.For<ILogger<ContentPublishHandler>>(), new ArticleMapping(_contentService, _cacheService));
    }

    [Test]
    public async Task Then_the_published_article_is_stored()
    {
        var result = await _handler.Handle(new ContentPublishRequest{EntryId=_entryId, EntryType = "article"}, new CancellationToken());

        await _cacheService.Received().Set("article_article1Slug", _expectedJson);
    }

    [Test]
    public async Task Then_the_articleId_slug_lookup_is_stored()
    {
        var result = await _handler.Handle(new ContentPublishRequest{EntryId=_entryId, EntryType = "article"}, new CancellationToken());

        await _cacheService.Received().Set("articleIdSlugLookup_" + _entryId, "article1Slug");
    }
}