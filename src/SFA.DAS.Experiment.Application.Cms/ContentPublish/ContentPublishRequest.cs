using MediatR;

namespace SFA.DAS.Experiment.Application.Cms.ContentPublish
{
    public class ContentPublishRequest : IRequest<ContentPublishResponse>
    {
        public string EntryId { get; set; }
        public string EntryType { get; set; }
    }
}

