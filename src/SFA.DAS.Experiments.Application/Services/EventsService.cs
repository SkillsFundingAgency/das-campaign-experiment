﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Domain.Models.Events;

namespace SFA.DAS.Experiments.Application.Services
{
    public class EventsService : IEventsService
    {
        private readonly ExperimentsContext _experimentsContext;
        private readonly ILogger<EventsService> _logger;

        public EventsService(ExperimentsContext experimentsContext, ILogger<EventsService> logger)
        {
            _experimentsContext = experimentsContext;
            _logger = logger;
        }

        public IList<EventData> GetUnprocessed()
        {
            _logger.LogDebug("Getting all unprocessed events in experiment database");

            _experimentsContext.ChangeTracker.DetectChanges();

          return _experimentsContext.Events.Where(w => w.Processed == false && (
              w.EventType == EventType.CandidateApplicationStart || 
              w.EventType == EventType.CandidateApplicationSubmit 
              )).OrderBy(e => e.EventDate).ToList();
        }

        public void UpdateAll(List<EventData> events)
        {

           _experimentsContext.UpdateRange(events);
           _experimentsContext.SaveChanges();
        }
    }
}
