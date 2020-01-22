using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SFA.DAS.Experiment.Function
{
    public class ApplicationEventsImporter
    {
        private readonly ConnectionStrings _connectionStrings;

        public ApplicationEventsImporter(IOptions<ConnectionStrings> connectionStrings)
        {
            // Connection strings passed through here just to test DI / Config wiring is working.  Obvs can be taken out if / when not needed.
            _connectionStrings = connectionStrings.Value;
        }

        [FunctionName("ApplicationEventsImporter")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}