using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiments.Application.Domain.Interfaces;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Helpers;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Mapping.Interfaces;
using SFA.DAS.Experiments.Application.Queries;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Handlers
{
    public class PushMarketoLeadsHandler : IRequestHandler<PushMarketoLeadsRequest, PushMarketoLeadsResponse>
    {
        private readonly IMarketoLeadService _marketoLeadService;
        private readonly IEventsService _eventsService;

        public PushMarketoLeadsHandler( IMarketoLeadService marketoLeadService, IEventsService eventsService)
        {
            _marketoLeadService = marketoLeadService;
            _eventsService = eventsService;
        }

        public async Task<PushMarketoLeadsResponse> Handle(PushMarketoLeadsRequest request, CancellationToken cancellationToken)
        {

            var pushResponse = new PushMarketoLeadsResponse();

            var candidateIds = _eventsService.GetKnownMarketoIds();

            // make sure if the candidate already exists and we have a marketo id, we use the same id
            request.Events.Where(w => candidateIds.ContainsKey(w.CandidateId)).ToList().ForEach(e => e.MarketoId = candidateIds[e.CandidateId]);

            //make sure we only are pushing new leads here and distinct values only, changes of details will be dealt with the event handler

            var events = request.Events.Where(w => w.MarketoId == null).DistinctBy(e => e.CandidateId);
               

            pushResponse.Leads = await _marketoLeadService.PushLeads(events.ToList());

            return pushResponse;

        }
    }
}
