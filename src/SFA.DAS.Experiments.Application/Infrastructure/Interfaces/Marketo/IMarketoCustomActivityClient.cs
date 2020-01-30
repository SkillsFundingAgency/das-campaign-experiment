using System.Threading.Tasks;
using Refit;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo
{
    [Headers("Authorization: Bearer","Content-Type: application/json")]
    public interface IMarketoActivityClient
    {
        [Post("/activities/external.json")]
        Task<ResponseOfLead> AddExternal(CustomActivityRequest activity);
       
    }
}
