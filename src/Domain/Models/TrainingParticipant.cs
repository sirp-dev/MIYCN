using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class TrainingParticipant
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public ParticipantTrainingStatus ParticipantTrainingStatus { get; set; }
        public long TrainingId { get; set; }
        public Training Training { get; set; }

        public string? Reasons { get; set; }
    }
}
