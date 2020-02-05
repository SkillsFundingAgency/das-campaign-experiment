using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.Experiments.Application.Domain.Interfaces;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Helpers;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Mapping.Interfaces;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Services.Marketo
{
    public class MarketoLeadService : IMarketoLeadService
    {
        private readonly IMarketoLeadClient _marketoLeadClient;
        private readonly IMarketoLeadMapping _marketoLeadMapping;
        private readonly MarketoConfiguration _marketoOptions;
        private readonly ILogger<MarketoLeadService> _log;

        public MarketoLeadService(IMarketoLeadClient marketoLeadClient, IMarketoLeadMapping marketoLeadMapping, ILogger<MarketoLeadService> log, IOptions<MarketoConfiguration> marketoOptions)
        {
            _marketoLeadClient = marketoLeadClient;
            _marketoLeadMapping = marketoLeadMapping;
            _log = log;
            _marketoOptions = marketoOptions.Value;
        }

        public async Task<IEnumerable<NewLead>> PushLeads(IList<EventData> events)
        {
            var Leads = events.GroupBy(g => new { g.CandidateFirstName, g.CandidateSurname, g.CandidateEmailAddress })
                .Select(g => _marketoLeadMapping.Map(g.First()))
                .ToList();

            var splitList = Leads.SplitList().ToList();

            foreach (var leadsList in splitList)
            {
                var leadsRequest = new PushLeadToMarketoRequest();

                leadsRequest.ProgramName = _marketoOptions.ExpProgConfiguration.ProgramName;
                leadsRequest.Input = leadsList.ToList();

                var response = await _marketoLeadClient.PushLead(leadsRequest);

                if (response.Errors != null)
                {
                    _log.LogError("Marketo Api Push lead Errors: \n" + String.Join("\n", response.Errors.Select(s => s.Message)));
                }


                for (int i = 0; i < leadsList.Count; i++)
                {
                    leadsList[i].Id = response.Result[i].Id;
                }
            }

            return splitList.SelectMany(s => s);
        }
    }
}
