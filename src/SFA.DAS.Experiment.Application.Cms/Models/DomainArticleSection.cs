using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Experiment.Application.Cms.Models
{
    public class DomainArticleSection
    {
        public string Title { get; set; }
        public HtmlString Body { get; set; }
        public string Slug { get; set; }
    }
}
