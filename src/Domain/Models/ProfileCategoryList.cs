using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class ProfileCategoryList
    {
        public long Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public long ProfileCategoryId { get; set; }
        public ProfileCategory ProfileCategory { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Employer { get; set; }
        public string? Honours { get; set; }
        public string? Qualification { get; set; }
        [Display(Name = "Field Of Study")]
        public string? FieldOfStudy { get; set; }
        public string? City { get; set; }
        public string? Percentage { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Start Date")]
        public string? StartDate { get; set; }
        [Display(Name = "End Date")]
        public string? EndDate { get; set; }
        public bool Currently { get; set; }


        public string? PrivacyTitle { get; set; }
        public bool Authorize { get; set; }
        public string? TokenKey { get; set; }
    }

}
