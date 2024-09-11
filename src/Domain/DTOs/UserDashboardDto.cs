using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserDashboardDto
    {
        public int Trainings { get; set; }
        public int ActiveTraining { get; set; }
        public int CompletedTraining { get; set; }
        public int CancelledTraining { get; set; }
        public int UpcomingTraining { get; set; }
        public int Certificates { get; set; }
        public int TotalAttendanceInTrainings { get; set; }
        public int Certificate { get; set; }

        public int PostShared { get; set; }
    }
}
