using Contentful.Core.Models;

namespace SFA.DAS.Experiment.Application.Cms.ContentTypes
{
    public class ArticleSection : IContentType
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        
        public Document Body { get; set; }
        public string Slug { get; set; }
    }
}