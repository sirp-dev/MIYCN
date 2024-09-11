using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Setting
    {
        public long Id { get; set; }

        public string? CertificateAttendanceTitle { get; set; }
        public string? TrainingTitle { get; set; }
        public string? CourseTitle { get; set; }
        public string? Address { get; set; }
        public string? Date { get; set; }

        public bool LeftOffSignature { get; set; }
        public string? LeftSignatureUrl { get; set; }
        public string? LeftSignatureKey { get; set; }
        public string? LeftTitleName { get; set; }
        public string? LeftPosition { get; set; }
        public string? LeftOccupation { get; set; }

        public bool RightOffSignature { get; set; }
        public string? RightSignatureUrl { get; set; }
        public string? RightSignatureKey { get; set; }
        public string? RightTitleName { get; set; }
        public string? RightPosition { get; set; }
        public string? RightOccupation { get; set; }


        public long TrainingId { get; set; }
        public Training Training { get; set; }

    }
}
