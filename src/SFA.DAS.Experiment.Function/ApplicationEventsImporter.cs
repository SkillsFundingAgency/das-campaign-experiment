using System;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using SFA.DAS.Experiments.Application.Handlers;

namespace SFA.DAS.Experiment.Function
{
    public class ApplicationEventsImporter
    {
        private readonly IMediator _mediator;

        public ApplicationEventsImporter(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("ApplicationEventsImporter")]
        public void Run([TimerTrigger("* */3 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"ApplicationEventsImporter Timer trigger function executed at: {DateTime.Now}");

            try
            {
                _mediator.Send(new ProcessEventsCommand());
                log.LogInformation($"Events processesing completed at {DateTime.Now}");

            }
            catch (Exception e)
            {
                log.LogError(e,"Unable to process Event data");
                throw;
            }
            
        }
    }
}