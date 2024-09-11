using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.DTOs
{
    public class TrainingByUserDto
    {
        public string UserId { get; set; }
        public long TrainingId { get; set; }
        public string Type { get; set; } // Participant or Facilitator
        public string? FacilitatorPosition { get; set; }
        public string? TrainingTitle { get; set; }
        public DateTime TrainingDate { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserState { get; set; }
        public string? UserLGA { get; set; }
        public string? PlaceOfWork { get; set; }
        public string? TrainingAddress { get; set; }
        public string? TrainingState { get; set; }
        public string? TrainingLGA { get; set; }
        public TrainingStatus TrainingStatus { get; set; }


        [Display(Name = "SignInStop Start Time")]
        public TimeSpan SignInStartTime { get; set; }
        [Display(Name = "SignIn Stop Time")]
        public TimeSpan SignInStopTime { get; set; }




        [Display(Name = "SignOutStop Start Time")]
        public TimeSpan SignOutStartTime { get; set; }
        [Display(Name = "SignOut Stop Time")]
        public TimeSpan SignOutStopTime { get; set; }
    }
}
