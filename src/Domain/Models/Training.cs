using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class Training
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? Ward { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Training Status")]
        public TrainingStatus TrainingStatus { get; set; }
        [Display(Name = "Dialy Start Time")]
        public string DialyStartTime { get; set; }
        [Display(Name = "Dialy End Time")]
        public string DialyEndTime { get; set; }

        public long? TrainingCategoryId  { get; set; }
        public TrainingCategory TrainingCategory { get; set; }

        public ICollection<Sponsor> Sponsors { get; set; }
        public ICollection<TrainingFacilitator> TrainingFacilitators { get; set; }
        public ICollection<TrainingParticipant> TrainingParticipants { get; set; }
        public ICollection<DialyActivity> DialyActivities { get; set; }
        public ICollection<TestCategory> TestCategory { get; set; }
        [Display(Name = "Enable Post Test")]
        public bool EnablePostTest { get; set; }
        [Display(Name = "Post Test Instruction")]
        public string? PostTestInstruction { get; set; }
        [Display(Name = "Enable Pre Test")]
        public bool EnablePreTest { get; set; }
        [Display(Name = "Pre Test Instruction")]
        public string? PreTestInstruction { get; set; }


        [Display(Name = "Evaluation Instruction")]
        public string? EvaluationInstruction { get; set; }


        [Display(Name = "SignIn Start Time")]
        public TimeSpan SignInStartTime { get; set; }
        [Display(Name = "SignIn Stop Time")]
        public TimeSpan SignInStopTime { get; set; }




        [Display(Name = "SignOut Start Time")]
        public TimeSpan SignOutStartTime { get; set; }
        [Display(Name = "SignOut Stop Time")]
        public TimeSpan SignOutStopTime { get; set; }


        [Display(Name = "Post Test Start Time")]
        public TimeSpan PostTestStartTime { get; set; }

        //
        [Display(Name = "Certificate Use Right Side Physical Signature")]
        public bool CertificateUseRightSidePhysicalSignature { get; set; }
        [Display(Name = "Certificate Right Side Signature Url")]

        public string? CertificateRightSideSignatureUrl { get; set; }
        [Display(Name = "Certificate Right Side Signature Key")]

        public string? CertificateRightSideSignatureKey { get; set; }
        [Display(Name = "Certificate Right Side Name")]

        public string? CertificateRightSideName { get; set; }
        [Display(Name = "Certificate Right Side Office Position")]

        public string? CertificateRightSideOfficePosition { get; set; }
        [Display(Name = "Certificate Right Side Office Title")]

        public string? CertificateRightSideOfficeTitle { get; set; }

        //
        [Display(Name = "Certificate Use Left Side Physical Signature")]
        public bool CertificateUseLeftSidePhysicalSignature { get; set; }
        [Display(Name = "Certificate Left Side Signature Url")]

        public string? CertificateLeftSideSignatureUrl { get; set; }
        [Display(Name = "Certificate Left Side Signature Key")]

        public string? CertificateLeftSideSignatureKey { get; set; }
        [Display(Name = "Certificate Left Side Name")]

        public string? CertificateLeftSideName { get; set; }
        [Display(Name = "Certificate Left Side Office Position")]

        public string? CertificateLeftSideOfficePosition { get; set; }

        [Display(Name = "Certificate Left Side Office Title")]
        public string? CertificateLeftSideOfficeTitle { get; set; }
        //

        [Display(Name = "Certificate Course Title")]
        public string? CertificateCourseTitle { get; set; }

        [Display(Name = "Certificate Title")]
        public string? CertificateTitle { get; set; }


        [Display(Name = "Certificate Address")]
        public string? CertificateAddress { get; set; }


        [Display(Name = "Certificate Date")]
        public string? CertificateDate { get; set; }
    }
}
