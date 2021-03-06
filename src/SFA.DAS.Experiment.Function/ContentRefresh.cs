using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SFA.DAS.Experiment.Application.Cms.ContentRefresh;

namespace SFA.DAS.Experiment.Function
{
    public class ContentRefresh
    {
        private readonly IMediator _mediator;

        public ContentRefresh(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("ContentRefresher")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest request, ILogger log)
        {
            var refreshResult = await _mediator.Send(new ContentRefreshRequest());

            var objectResult = new ObjectResult(refreshResult)
            {
                StatusCode = refreshResult.Success ?  StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection() 
            };
            objectResult.ContentTypes.Add("application/json");
            return objectResult;
        }

        [FunctionName("ContentRefresherCron")]
        public async Task RunCron([TimerTrigger("0 0 */1 * * *")]TimerInfo timer, ILogger log)
        {
            await _mediator.Send(new ContentRefreshRequest());
        }
    }
}