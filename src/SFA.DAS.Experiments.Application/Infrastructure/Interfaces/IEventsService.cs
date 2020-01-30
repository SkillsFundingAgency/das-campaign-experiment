using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Experiments.Application.Domain.Models;

namespace SFA.DAS.Experiments.Application.Services
{
    public interface IEventsService
    {
        IList<EventData> GetUnprocessed();
        Task UpdateAll(List<EventData> events);
    }
}