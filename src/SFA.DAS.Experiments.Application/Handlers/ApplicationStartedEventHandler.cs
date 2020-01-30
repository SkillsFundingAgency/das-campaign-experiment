using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiments.Application.Domain.Models.Events;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Queries;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Models.Marketo;
using SFA.DAS.Experiments.Application.Helpers;
using Attribute = Marketo.Api.Client.Model.Attribute;

namespace SFA.DAS.Experiments.Application.Handlers
{
   public class ApplicationStartedEventHandler : INotificationHandler<EventsProcessedNotification>
   {
       private readonly IMediator _mediator;
       private readonly ILogger<ApplicationStartedEventHandler> _log;
       private readonly IMarketoActivityClient _activityService;
       private readonly IEventsService _eventsService;

       public ApplicationStartedEventHandler(IMediator mediator, ILogger<ApplicationStartedEventHandler> log, IMarketoActivityClient activityService, IEventsService eventsService)
       {
           _mediator = mediator;
           _log = log;
           _activityService = activityService;
           _eventsService = eventsService;
       }


       public async Task Handle(EventsProcessedNotification notification, CancellationToken cancellationToken)
       {
           var startedEvents = notification.Events.Where(w => w.EventType == EventType.CandidateApplicationStart && w.Processed == false).ToList().SplitList();


           foreach (var eventList in startedEvents)
           {
                var activities = new CustomActivityRequest();

                activities.Input = eventList.Select(s => new CustomActivity()
                {
                    LeadId = Convert.ToInt64(s.MarketoId),
                    ActivityDate = s.EventDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                    ActivityTypeId = 100002,
                    PrimaryAttributeValue = s.ApplicationId,
                    ApiName = "ESFA_candidate_Application_Started",
                    Attributes = new List<Attribute>()
                    {
                        new Attribute()
                        {
                            ApiName = "ESFA_candidateID",
                            Value = s.CandidateId
                        },
                        new Attribute()
                        {
                            ApiName = "ESFA_vacancyReference",
                            Value = s.VacancyReference
                        },
                        new Attribute()
                        {
                            ApiName = "ESFA_vacancyID",
                            Value = s.VacancyId
                        },
                        new Attribute()
                        {
                            ApiName = "ESFA_vacancyTitle",
                            Value = s.VacancyTitle
                        },
                        new Attribute()
                        {
                            ApiName = "ESFA_vacancyCloseDate",
                            Value = s.VacancyCloseDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture)
                        }
                    }


                }).ToList();

                var response = await _activityService.AddExternal(activities);

                if(response.Success == false)
                {
                    _log.LogError($"Unable to add activities, errors: {String.Join("\n",response.Errors)}");
                }

                var successfulUpdates = notification.Events.Where(p => response.Result.Where(w => w.Status != null).All(p2 => p2.Id == p.MarketoId));
                _eventsService.UpdateAll(successfulUpdates.ToList());

                var unsuccessfulUpdates = response.Result.Where(p => p.Status == null);

                unsuccessfulUpdates.ToList().ForEach(s => _log.LogError($"Unable to add activity for Marketo Id: {s.Id}. Reason: {s.Reason}"));

            }


       }
   }
}
