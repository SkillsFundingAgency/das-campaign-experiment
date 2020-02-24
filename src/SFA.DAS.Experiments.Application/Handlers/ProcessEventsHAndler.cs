using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;
using SFA.DAS.Experiments.Application.Queries;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Handlers
{
    public class ProcessEventsHandler : IRequestHandler<ProcessEventsCommand>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessEventsHandler> _log;
        private readonly IEventsService _eventsService;
        private readonly MarketoConfiguration _marketoConfiguration;

        public ProcessEventsHandler(IMediator mediator, ILogger<ProcessEventsHandler> log, IEventsService eventsService, IOptions<MarketoConfiguration> marketoOptions)
        {
            _mediator = mediator;
            _log = log;
            _eventsService = eventsService;
            _marketoConfiguration = marketoOptions.Value;
        }

        public async Task<Unit> Handle(ProcessEventsCommand request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"Proceesing the handler {nameof(ProcessEventsHandler)}");

            var events = _eventsService.GetUnprocessed() as List<EventData>;

            var processedEvents = await _mediator.Send(new PushMarketoLeadsRequest(events.Where(w => w.MarketoId == null).ToList()));


            events.Where(w => w.MarketoId == null).ToList().ForEach(async e => e.MarketoId = processedEvents.Leads.FirstOrDefault(w => w.Email == e.CandidateEmailAddress).Id);

            events.ForEach(e => e.Processed = false);
            _eventsService.UpdateAll(events);


            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.AppStartedConf.EventTypeId,
                _marketoConfiguration.AppStartedConf.ApiName,
                _marketoConfiguration.AppStartedConf.ActivityTypeId, 
                _marketoConfiguration.AppStartedConf.IdField));

            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.AppCompletedConf.EventTypeId,
                _marketoConfiguration.AppCompletedConf.ApiName,
                _marketoConfiguration.AppCompletedConf.ActivityTypeId, 
                _marketoConfiguration.AppCompletedConf.IdField));

            // handles _marketoConfiguration.CanDetailsUpdatedConf
            await _mediator.Send(new UpdateMarketoLeadsRequest(events));

            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.CanAcountDeleted.EventTypeId,
                _marketoConfiguration.CanAcountDeleted.ApiName,
                _marketoConfiguration.CanAcountDeleted.ActivityTypeId, 
                _marketoConfiguration.CanAcountDeleted.IdField));

            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.CanClosingEmailConf.EventTypeId,
                _marketoConfiguration.CanClosingEmailConf.ApiName,
                _marketoConfiguration.CanClosingEmailConf.ActivityTypeId, 
                _marketoConfiguration.CanClosingEmailConf.IdField));

            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.CanSavedEmailConf.EventTypeId,
                _marketoConfiguration.CanSavedEmailConf.ApiName,
                _marketoConfiguration.CanSavedEmailConf.ActivityTypeId,
                _marketoConfiguration.CanSavedEmailConf.IdField));


            return new Unit();
        }
    }
}
