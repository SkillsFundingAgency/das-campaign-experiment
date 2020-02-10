using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Domain.Interfaces
{
    public interface IMarketoActivityService
    {
        Task<IList<EventData>> AddActivities(IList<EventData> allEvents, IEnumerable<IList<EventData>> startedEvents,
            int activityTypeId, string apiName, string fieldId);
    }
}
