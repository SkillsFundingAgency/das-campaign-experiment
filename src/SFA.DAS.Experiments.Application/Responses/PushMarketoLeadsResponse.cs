using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Queries
{
    public class PushMarketoLeadsResponse
    {
        public IEnumerable<NewLead> Leads { get; set; }
    }
}
