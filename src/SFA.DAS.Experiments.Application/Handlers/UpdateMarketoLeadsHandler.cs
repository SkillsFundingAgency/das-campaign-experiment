using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.Experiments.Application.Domain.Interfaces;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;
using SFA.DAS.Experiments.Application.Helpers;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Mapping.Interfaces;
using SFA.DAS.Experiments.Application.Queries;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Handlers
{
    public class UpdateMarketoLeadsHandler : IRequestHandler<UpdateMarketoLeadsRequest, UpdateMarketoLeadsResponse>
    {
        private readonly IMarketoLeadService _marketoLeadService;
        private readonly IMediator _mediator;
        private readonly IEventsService _eventsService;
        private readonly MarketoConfiguration _marketoConfiguration;


        public UpdateMarketoLeadsHandler( IMarketoLeadService marketoLeadService, IMediator mediator, IOptions<MarketoConfiguration> marketoOptions, IEventsService eventsService)
        {
            _marketoLeadService = marketoLeadService;
            _mediator = mediator;
            _eventsService = eventsService;
            _marketoConfiguration = marketoOptions.Value;
        }

        public async Task<UpdateMarketoLeadsResponse> Handle(UpdateMarketoLeadsRequest request, CancellationToken cancellationToken)
        {
            var pushResponse = new UpdateMarketoLeadsResponse();

            var candidateIds = _eventsService.GetKnownMarketoIds();

            // make sure if the candidate already exists and we have a marketo id, we use the same id
            request.Events.Where(w => candidateIds.ContainsKey(w.CandidateId)).ToList().ForEach(e => e.MarketoId = candidateIds[e.CandidateId]);

            var events = request.Events.Where(w => w.MarketoId != null && w.EventType == (EventType)_marketoConfiguration.CanDetailsUpdatedConf.EventTypeId).ToList();

            //make sure we only are pushing existing leads here, as we are only 
            pushResponse.Leads = await _marketoLeadService.SyncLeads(events);

            //add the activity to the log for the user so we can see when details have been updated
            await _mediator.Send(new EventsProcessedNotification(events,
                (EventType)_marketoConfiguration.CanDetailsUpdatedConf.EventTypeId,
                _marketoConfiguration.CanDetailsUpdatedConf.ApiName,
                _marketoConfiguration.CanDetailsUpdatedConf.ActivityTypeId,
                _marketoConfiguration.CanDetailsUpdatedConf.IdField));


            return pushResponse;

        }
    }
}
