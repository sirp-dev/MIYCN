using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.DTOs
{
    public class ParticipantInTrainingDTo
    {
        public long ParticipantTrainingId { get; set; }
        public string Id { get; set; }
        [Display(Name = "Unique ID")]
        public string? UniqueId { get; set; }
        public string? AccountToken { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public DateTime Date { get; set; }
        public string? Religion { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "Gender")]
        public GenderStatus Gender { get; set; }
        public ParticipantTrainingStatus ParticipantTrainingStatus { get; set; }

        [Display(Name = "Role")]
        public string? Role { get; set; }



        [Display(Name = "Current State")]
        public string? CurrentState { get; set; }
        [Display(Name = "Current LGA")]
        public string? CurrentLga { get; set; }
        [Display(Name = "Current Address")]
        public string? Address { get; set; }

        [Display(Name = "Place Of Work")]
        public string? PlaceOfWork { get; set; }

        [Display(Name = "State of Origin")]
        public string? StateOrigin { get; set; }
        [Display(Name = "LGA of Origin")]
        public string? LgaOrigin { get; set; }
        [Display(Name = "Country")]
        public string? Country { get; set; }


        [Display(Name = "Passport Photograph")]
        public string? PassportFilePathUrl { get; set; }
        public string? PassportFilePathKey { get; set; }


        [Display(Name = "Valid IDCard")]
        public string? IdCardUrl { get; set; }
        public string? IdCardKey { get; set; }


        [Display(Name = "Bank Name")]
        public string? BankName { get; set; }

        [Display(Name = "Bank Account")]
        public string? BankAccount { get; set; }

        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }


        [Display(Name = "FullName")]
        public string FullnameX
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        public string? Logs { get; set; }

        public long TrainingId { get; set; }
        public string? Title { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }

        public bool SmsSent { get; set; }
    }
}
