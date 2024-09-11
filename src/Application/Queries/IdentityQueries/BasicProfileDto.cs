using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Queries.IdentityQueries
{
    public class BasicProfileDto
    {
        public string Id { get; set; }
        public string FullnameX { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public DateTime Date { get; set; }
        
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "Gender")]
        public GenderStatus Gender { get; set; }


        [Display(Name = "Role")]
        public string? Role { get; set; }

        public bool ResetPassword { get; set; }
        public string TempPass { get; set; }

        public bool SmsSent { get; set; }
        public bool EmailSent { get; set; }
        public string? State { get; set; }

    }
}
