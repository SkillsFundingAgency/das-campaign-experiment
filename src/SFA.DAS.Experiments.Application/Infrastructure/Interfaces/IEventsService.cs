using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;

namespace SFA.DAS.Experiments.Application.Services
{
    public interface IEventsService
    {
        IList<EventData> GetUnprocessed();
        IDictionary<string, int> GetKnownMarketoIds();
        void UpdateAll(List<EventData> events);
    }
}