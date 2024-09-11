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
    public class FullProfileDto
    {
        public string Id { get; set; }
        public string FullnameX { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
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
        [Display(Name = "Registration Date")]

        public DateTime Date { get; set; }
        public string? Religion { get; set; }

        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "Gender")]
        public GenderStatus Gender { get; set; }


        [Display(Name = "Role")]
        public string? Role { get; set; }

        [Display(Name = "Facilitator Role")]
        public string? FacilitatorRole { get; set; }
        [Display(Name = "Describe Facilitator Role")]
        public string? DescribeFacilitatorRole { get; set; }

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

        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Bank Name")]
        public string? BankName { get; set; }

        [Display(Name = "Account Name")]
        public string? BankAccount { get; set; }

        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }
        public string ProfileLink { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string? Logs { get; set; }
        public string? LastLogin { get; set; }
        // Navigation properties
        public IList<ProfileCategory> ProfileCategories { get; set; }


        public bool UpdateProfile { get; set; }
        public bool UpdateExperience { get; set; }
        public bool UpdateEducation { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
