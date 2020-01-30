using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Domain.Interfaces
{
    public interface IMarketoLeadService
    {
        Task<IEnumerable<NewLead>> PushLeads(IList<EventData> user);
    }
}
