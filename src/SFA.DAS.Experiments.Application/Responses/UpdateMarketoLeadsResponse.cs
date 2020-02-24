using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Queries
{
    public class UpdateMarketoLeadsResponse
    {
        public IEnumerable<Lead> Leads { get; set; }
    }
}
