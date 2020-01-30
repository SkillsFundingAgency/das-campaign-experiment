using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;
using SFA.DAS.Experiments.Application.Queries;

namespace SFA.DAS.Experiments.Application.Handlers
{
    public class EventsProcessedNotification : IRequest
    {

        public EventsProcessedNotification(IList<EventData> processedEvents, EventType eventType, string apiName,
            int activityTypeId, string idField)
        {
            Events = processedEvents;
            EventType = eventType;
            ApiName = apiName;
            ActivityTypeId = activityTypeId;
            IdField = idField;  
        }

        public string IdField { get; set; }

        public int ActivityTypeId { get; set; }
        public string ApiName { get; set; }
        public EventType EventType { get; set; }
        public IList<EventData> Events { get; set; }
    }
}
