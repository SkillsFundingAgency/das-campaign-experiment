using System.Collections.Generic;

namespace SFA.DAS.Experiment.Application.Cms.Models
{
    public class DomainArticle
    {        
        public string LandingPageSlug { get; set; }
        public string LandingPageTitle { get; set; }
        public List<DomainArticleSection> Sections { get; set; }
    }
}
