﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiments.Application.Domain.Models;
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

       public ProcessEventsHandler(IMediator mediator, ILogger<ProcessEventsHandler> log, IEventsService eventsService)
       {
           _mediator = mediator;
           _log = log;
           _eventsService = eventsService;
       }

       public async Task<Unit> Handle(ProcessEventsCommand request, CancellationToken cancellationToken)
        { 
           _log.LogDebug($"Proceesing the handler {nameof(ProcessEventsHandler)}");

            var events = _eventsService.GetUnprocessed() as List<EventData>;

            var processedEvents = await _mediator.Send(new PushMarketoLeadsRequest(events));


            events.ForEach(async e => e.MarketoId = processedEvents.Leads.Single(w => w.Email == e.CandidateEmailAddress).Id);

            _eventsService.UpdateAll(events);

            await _mediator.Publish(new EventsProcessedNotification(_eventsService.GetUnprocessed()));


            return Unit.Value;
       }
    }
}
