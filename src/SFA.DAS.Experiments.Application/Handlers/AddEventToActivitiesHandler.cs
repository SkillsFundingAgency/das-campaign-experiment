using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.Experiments.Application.Domain.Interfaces;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Queries;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Models.Marketo;
using SFA.DAS.Experiments.Application.Helpers;

namespace SFA.DAS.Experiments.Application.Handlers
{
   public class AddEventToActivitiesHandler : IRequestHandler<EventsProcessedNotification>
   {
       private readonly ILogger<AddEventToActivitiesHandler> _log;
       private readonly IMarketoActivityService _activityService;
       private readonly IEventsService _eventsService;
       private readonly MarketoConfiguration _marketoConfig;

       public AddEventToActivitiesHandler( ILogger<AddEventToActivitiesHandler> log, IEventsService eventsService, IMarketoActivityService activityService, IOptions<MarketoConfiguration> marketoConfig)
       {
           _log = log;
           _eventsService = eventsService;
           _activityService = activityService;
           _marketoConfig = marketoConfig.Value;
       }


       public async Task<Unit> Handle(EventsProcessedNotification notification, CancellationToken cancellationToken)
       {
           var startedEvents = notification.Events.Where(w => w.EventType == (EventType) notification.EventType && w.Processed == false).ToList().SplitList().ToList();

           if (!startedEvents.Any()) return new Unit();

           var activityTypeId = notification.ActivityTypeId;
           var apiName = notification.ApiName;

           var successfulUpdates = await _activityService.AddActivities(notification.Events, startedEvents, activityTypeId, apiName, notification.IdField);

           _eventsService.UpdateAll(successfulUpdates.ToList());


           return new Unit();
       }

   }
}
