using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Experiments.Application.Domain.Models.Events
{
   public enum EventType
    {
        CandidateApplicationStart = 1,
        CandidateApplicationSubmit = 2,
        CandidateDetailsChange = 3,
        CandidateFaaSavedSearchEmailSent = 4,
        CandidateFaaVacancyClosingEmailSent = 5,
        CandidateAccountDelete = 6


    }
}
