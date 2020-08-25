using System.Collections.Generic;

namespace SFA.DAS.Experiment.Application.Cms.Models
{
    public class DomainArticle
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string Hub { get; set; }
        public List<DomainArticleSection> Sections { get; set; }
    }
}
