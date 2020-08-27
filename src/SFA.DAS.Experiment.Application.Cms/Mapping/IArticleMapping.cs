using System.Threading.Tasks;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;
using SFA.DAS.Experiment.Application.Cms.Models;

namespace SFA.DAS.Experiment.Application.Cms.Mapping
{
    public interface IArticleMapping
    {
        Task<Page<DomainArticle>> MapArticleToPage(Article article);
    }
}