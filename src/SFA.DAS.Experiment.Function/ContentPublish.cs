using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms;
using SFA.DAS.Experiment.Application.Cms.ContentPublish;

namespace SFA.DAS.Experiment.Function
{
    public class ContentPublish
    {
        private readonly IMediator _mediator;
        private readonly ContentfulOptions _contentfulOptions;

        public ContentPublish(IMediator mediator, ContentfulOptions contentfulOptions)
        {
            _mediator = mediator;
            _contentfulOptions = contentfulOptions;
        }

        [FunctionName("ContentPublish")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest request, ILogger log)
        {
            if(!request.Headers.ContainsKey("contentfulWebhookSecret") 
            || request.Headers["contentfulWebhookSecret"][0] != _contentfulOptions.WebhookSecret)
            {
                log.LogInformation("Call to ContentRefresher without the correct contentfulWebhookSecret. Returning 401 Unauthorized."); 
                return new UnauthorizedResult();
            }

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            var payload = JsonConvert.DeserializeObject<ContentPublishRequest>(requestBody);

            var contentPublishedResult = await _mediator.Send(payload);

            var objectResult = new ObjectResult(contentPublishedResult)
            {
                StatusCode = contentPublishedResult.Success ?  StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection() 
            };
            objectResult.ContentTypes.Add("application/json");
            return objectResult;
        }
    }
}