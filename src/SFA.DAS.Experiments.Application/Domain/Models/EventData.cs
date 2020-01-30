using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;
using Refit;
using SFA.DAS.Experiments.Application.Domain.Models.Events;

namespace SFA.DAS.Experiments.Application.Domain.Models
{
    public class EventData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public EventType EventType { get; set; }
        public string CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateSurname { get; set; }
        [Required]
        public string CandidateEmailAddress { get; set; }
        public string ApplicationId { get; set; }
        public string VacancyId { get; set; }
        public string VacancyTitle { get; set; }
        public string VacancyReference { get; set; }
        public DateTime? VacancyCloseDate { get; set; }
        public bool Processed { get; set; }
        public int? MarketoId { get; set; }

        [NotMapped]
        public bool DomainInvalid { get; set; }
        public bool IsValidEmail()
        {
            var strIn = CandidateEmailAddress;
            if (string.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (DomainInvalid)
            {
                return false;
            }

            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(strIn,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }



        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                DomainInvalid = true;
            }

            return match.Groups[1].Value + domainName;
        }
    }
}
