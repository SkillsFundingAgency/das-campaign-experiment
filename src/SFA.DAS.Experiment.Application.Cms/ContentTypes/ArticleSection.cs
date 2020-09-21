using Contentful.Core.Models;
using Newtonsoft.Json.Linq;

namespace SFA.DAS.Experiment.Application.Cms.ContentTypes
{
    public class ArticleSection : IContentType
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        
        public Document Body { get; set; }
        public string Slug { get; set; }
    }

    public class ArticleTable : IContentType
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        
        public JObject Table { get; set; }
    }
}