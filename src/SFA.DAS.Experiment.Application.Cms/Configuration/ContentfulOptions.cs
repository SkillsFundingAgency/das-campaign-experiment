namespace SFA.DAS.Experiment.Application.Cms
{
    public class ContentfulOptions
    {
        public string DeliveryApiKey { get; set; }
        public string PreviewApiKey { get; set; }
        public string SpaceId { get; set; }
        public bool UsePreviewApi { get; set; }
        public int MaxNumberOfRateLimitRetries { get; set; }
        public string WebhookSecret { get; set; }
    }
}