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
using Attribute = Marketo.Api.Client.Model.Attribute;

namespace SFA.DAS.Experiments.Application.Services.Marketo
{
    public class MarketoActivityService : IMarketoActivityService
    {
        private readonly IMarketoActivityClient _activityClient;
        private readonly IMarketoLeadMapping _marketoLeadMapping;
        private readonly IOptions<MarketoConfiguration> _marketoOptions;
        private readonly ILogger<MarketoLeadService> _log;

        public MarketoActivityService(IMarketoActivityClient activityClient)
        {
            _activityClient = activityClient;
        }

        public async Task<IList<EventData>> AddActivities(IList<EventData> allEvents,
            IEnumerable<IList<EventData>> startedEvents, int activityTypeId, string apiName, string fieldId)
        {
            IList<EventData> successfulUpdates = new List<EventData>();


            foreach (var eventList in startedEvents)
            {
                var activities = new CustomActivityRequest();

                activities.Input = eventList.Select(s => Map(s, apiName, activityTypeId, fieldId)).ToList();

                var response = await _activityClient.AddExternal(activities);

                if (response.Success == false)
                {
                    _log.LogError($"Unable to add activities, errors: {String.Join("\n", response.Errors)}");
                }

                successfulUpdates.Concat(eventList.Where(p =>
                    response.Result.Where(w => w.Status != null).All(p2 => p.Processed = true)));

                var unsuccessfulUpdates = response.Result.Where(p => p.Status == null);

                unsuccessfulUpdates.ToList().ForEach(s =>
                    _log.LogError($"Unable to add activity for Activity {apiName} and Marketo Lead Id: {s.Id}. Reason: {s.Reason}"));
            }

            return successfulUpdates;
        }

        private CustomActivity Map(EventData eventData, string apiName, int activityTypeId, string fieldId)
        {

            var attributes = new List<Attribute>();


            if (String.IsNullOrWhiteSpace(eventData.CandidateId) == false)
            {
                attributes.Add(new Attribute()
                {
                    ApiName = "ESFA_candidateID",
                    Value = eventData.CandidateId
                });
            }

            if (String.IsNullOrWhiteSpace(eventData.CandidateId) == false)
            {
                attributes.Add(new Attribute()
                {
                    ApiName = "ESFA_vacancyReference",
                    Value = eventData.VacancyReference
                });
            }

            if (String.IsNullOrWhiteSpace(eventData.CandidateId) == false)
            {
                attributes.Add(new Attribute()
                {
                    ApiName = "ESFA_vacancyID",
                    Value = eventData.VacancyId
                });
            }

            if (String.IsNullOrWhiteSpace(eventData.CandidateId) == false)
            {
                attributes.Add(new Attribute()
                {
                    ApiName = "ESFA_vacancyTitle",
                    Value = eventData.VacancyTitle
                });
            }

            if (String.IsNullOrWhiteSpace(eventData.CandidateId) == false)
            {
                attributes.Add(new Attribute()
                {
                    ApiName = "ESFA_vacancyCloseDate",
                    Value = eventData.VacancyCloseDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture)
                });
            }


            var activity = new CustomActivity()
            {
                LeadId = Convert.ToInt64(eventData.MarketoId),
                ActivityDate = eventData.EventDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                ActivityTypeId = activityTypeId,
                PrimaryAttributeValue = GetPropValue(eventData,fieldId),
                ApiName = apiName,
                Attributes = attributes


            };
            return activity;
        }

        private static string GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
        }

    }
}
