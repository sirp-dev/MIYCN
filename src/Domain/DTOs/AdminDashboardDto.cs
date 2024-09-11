using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AdminDashboardDto
    {
        public int Trainings { get; set; }
        public int Facilitator { get; set; }
        public int Participants { get; set; }
        public int Certificates { get; set; }
        public int Staff { get; set; }
        public int StateCordinator { get; set; }

        public int Backtowork { get; set; }
        public int Sponsors { get; set; }
    }
}
