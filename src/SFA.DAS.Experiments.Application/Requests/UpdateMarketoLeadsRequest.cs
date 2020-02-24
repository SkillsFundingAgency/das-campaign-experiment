using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Experiments.Application.Domain.Models;

namespace SFA.DAS.Experiments.Application.Queries
{
    public class UpdateMarketoLeadsRequest : IRequest<UpdateMarketoLeadsResponse>
    {
        public UpdateMarketoLeadsRequest(List<EventData> events)
        {
            Events = events;
        }

        public List<EventData> Events { get; set; }

    }
}
