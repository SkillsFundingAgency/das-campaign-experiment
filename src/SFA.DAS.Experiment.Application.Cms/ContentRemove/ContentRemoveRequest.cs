using MediatR;

namespace SFA.DAS.Experiment.Application.Cms.ContentRemove
{
    public class ContentRemoveRequest : IRequest<ContentRemoveResponse>
    {
        public string EntryId { get; set; }
        public string EntryType { get; set; }
    }
}

