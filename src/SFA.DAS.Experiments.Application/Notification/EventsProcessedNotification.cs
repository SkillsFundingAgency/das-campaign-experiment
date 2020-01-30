using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Queries;

namespace SFA.DAS.Experiments.Application.Handlers
{
   public class EventsProcessedNotification : IRequest
    {
        public IList<EventData> Events { get; set; }
        public EventsProcessedNotification(IList<EventData> processedEvents)
        {
            Events = processedEvents;
        }
    }
}
