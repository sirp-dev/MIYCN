using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ParticipantCertificateDto
    {
        public string UserId { get; set; }
        public long ParticipantId { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Phone {  get; set; }
        public string PretestScore { get; set; }
        public string PostestScore { get; set; }
        public string SignInAttendancePresent { get; set; }
        public string SignInAttendanceAbsent { get; set; }
        public string SignOutAttendancePresent { get; set; }
        public string SignOutAttendanceAbsent { get; set; }

        public bool AddedToCertificate { get; set; }
    }
}
