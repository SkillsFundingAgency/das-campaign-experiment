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
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Handlers
{
    public class PushMarketoLeadsHandler : IRequestHandler<PushMarketoLeadsRequest, PushMarketoLeadsResponse>
    {
        private readonly IMarketoLeadService _marketoLeadService;

        public PushMarketoLeadsHandler( IMarketoLeadService marketoLeadService)
        {
            _marketoLeadService = marketoLeadService;
        }

        public async Task<PushMarketoLeadsResponse> Handle(PushMarketoLeadsRequest request, CancellationToken cancellationToken)
        {

            var pushResponse = new PushMarketoLeadsResponse();

            pushResponse.Leads = await _marketoLeadService.PushLeads(request.Events);

            return pushResponse;

        }
    }
}
