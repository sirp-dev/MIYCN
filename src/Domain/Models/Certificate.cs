using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class Certificate
    {
        public long Id { get; set; }
        [Display(Name = "User")]
        public string? UserId { get; set; }
        [Display(Name = "User")]
        public AppUser User { get; set; }
        [Display(Name = "FullName")]
        public string? FullName { get; set; }
        [Display(Name = "Passport")]
        public string? PassportUrl { get; set; }
        [Display(Name = "Passport Key")]
        public string? PassportKey { get; set; }
        [Display(Name = "Certificate")]

        public string? CerificateNumber { get; set; }
        [Display(Name = "Type of Certificate")]
        public CertificateType CertificateType { get; set; }
        [Display(Name = "Issuer Date")]

        public DateTime IssuerDate { get; set; }

        [Display(Name = "Training")]
        public long? TrainingId { get; set; }
        [Display(Name = "Training")]
        public Training Training { get; set; }

        public CertificateStatus CertificateStatus { get; set; }
    }
}
