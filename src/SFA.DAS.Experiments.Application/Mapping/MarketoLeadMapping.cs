using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IdentityModel.Client;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Models.Marketo;

namespace SFA.DAS.Experiments.Application.Mapping.Interfaces
{
    public class MarketoLeadMapping : IMarketoLeadMapping
    {
        public NewLead Map(EventData data)
        {
            var newLead = new NewLead();

            if (data != null)
            {
                newLead.Email = data.CandidateEmailAddress;
                newLead.FirstName = data.CandidateFirstName;
                newLead.LastName = data.CandidateSurname;
            }

            if (data.MarketoId != null)
            {
                newLead.Id = data.MarketoId.Value;
            }

            return newLead;
        }

        public Lead MapLead(EventData data)
        {
            var newLead = new Lead();

            if (data != null)
            {
                newLead.FirstName = data.CandidateFirstName;
                newLead.LastName = data.CandidateSurname;
                newLead.Email = data.CandidateEmailAddress;
            }

            if (data.MarketoId != null)
            {
                newLead.Id = data.MarketoId.Value;
            }

            return newLead;
        }
    }
}
