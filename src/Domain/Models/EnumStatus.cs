using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EnumStatus
    {
        public enum UserStatus
        {
            [Description("Pending")]
            Pending = 0,

            [Description("Active")]
            Active = 2,
            [Description("Suspended")]
            Suspended = 3,

            [Description("Leave")]
            Leave = 4,
            [Description("Deleted")]
            Deleted = 6,
        }
        public enum EvaluationAnswerType
        {
            Options = 0,
            Typing = 2
        }
        public enum CertificateStatus
        {
            Preview = 0,
            Active = 2,
            Expired = 1,
        }
        public enum CertificateType
        {
            Nill = 0,
            Facilitator = 2,
            Participant = 1,
        }
        public enum TrainingTestType
        {
            PreTest = 0,
            PostTest = 2,
        }
        public enum GenderStatus
        {
            Nill = 0,
            Female = 1,
            Male = 2,
        }

        public enum ParticipantTrainingStatus
        {
            Active = 0,
            Disabled = 1,
        }
        public enum FacilitatorTrainingStatus
        {
            Active = 0,
            Disabled = 1,
        }
        public enum AttendanceSignInStatus
        {

            Present = 3,
            Absent = 2,
            Null = 0
        }

        public enum AttendanceSignOutStatus
        {

            Present = 3,
            Absent = 2,
            Null = 0
        }
        public enum TestStatus
        {
            Nill = 0,
            Present = 1,
            Absent = 2,
            Excused = 3,
        }
        public enum TrainingStatus
        {
            Nill = 0,
            Active = 1,
            Completed = 2,
            Upcoming = 3,
            Cancelled = 3,
        }
    }
}
