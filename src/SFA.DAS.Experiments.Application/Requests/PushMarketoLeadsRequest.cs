using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Experiments.Application.Domain.Models;

namespace SFA.DAS.Experiments.Application.Queries
{
    public class PushMarketoLeadsRequest : IRequest<PushMarketoLeadsResponse>
    {
        public PushMarketoLeadsRequest(List<EventData> events)
        {
            Events = events;
        }

        public List<EventData> Events { get; set; }

    }
}
