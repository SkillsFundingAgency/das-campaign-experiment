using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            // if(!request.Headers.ContainsKey("contentfulWebhookSecret") 
            // || request.Headers["contentfulWebhookSecret"][0] != _contentfulOptions.WebhookSecret)
            // {
            //     log.LogInformation("Call to ContentRefresher without the correct contentfulWebhookSecret. Returning 401 Unauthorized."); 
            //     return new UnauthorizedResult();
            // }

            var refreshResult = await _mediator.Send(new ContentRefreshRequest());

            var objectResult = new ObjectResult(refreshResult)
            {
                StatusCode = StatusCodes.Status200OK,
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection() 
            };
            objectResult.ContentTypes.Add("application/json");
            return objectResult;
            //return objectResult  OkResultOn();
        }
    }
}