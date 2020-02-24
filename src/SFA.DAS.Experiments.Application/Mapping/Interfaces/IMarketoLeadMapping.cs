using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IdentityModel.Client;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Mapping.Interfaces
{
    public interface IMarketoLeadMapping
    {
        NewLead Map(EventData data);

        Lead MapLead(EventData data);
    }
}
