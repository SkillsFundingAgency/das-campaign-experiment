using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.Experiment.Application.Cms;
using SFA.DAS.Experiment.Application.Cms.ContentRemove;

namespace SFA.DAS.Experiment.Function
{
    public class ContentRemove
    {
        private readonly IMediator _mediator;
        private readonly ContentfulOptions _contentfulOptions;

        public ContentRemove(IMediator mediator, IOptions<ContentfulOptions> contentfulOptions)
        {
            _mediator = mediator;
            _contentfulOptions = contentfulOptions.Value;
        }

        [FunctionName("ContentRemove")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest request, ILogger log)
        {
            if(!request.Headers.ContainsKey("contentfulWebhookSecret") 
            || request.Headers["contentfulWebhookSecret"][0] != _contentfulOptions.WebhookSecret)
            {
                log.LogInformation("Call to ContentRemove without the correct contentfulWebhookSecret. Returning 401 Unauthorized."); 
                return new UnauthorizedResult();
            }

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            var payload = JsonConvert.DeserializeObject<ContentRemoveRequest>(requestBody);

            var contentRemovedResult = await _mediator.Send(payload);

            var objectResult = new ObjectResult(contentRemovedResult)
            {
                StatusCode = contentRemovedResult.Success ?  StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection() 
            };
            objectResult.ContentTypes.Add("application/json");
            return objectResult;
        }
    }
}