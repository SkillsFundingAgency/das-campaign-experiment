using System;
using System.Threading.Tasks;
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

        [FunctionName("ApplicationEventsImporter"), Disable]
        public async Task Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"ApplicationEventsImporter Timer trigger function executed at: {DateTime.Now}");

            try
            {
               await _mediator.Send(new ProcessEventsCommand());
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