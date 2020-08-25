using Contentful.Core.Models;

namespace SFA.DAS.Experiment.Application.Cms.ContentTypes
{
    public class LandingPage : IContentType
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string HubType { get; set; }
        public string Summary { get; set; }
    }
}