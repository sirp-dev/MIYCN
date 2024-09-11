using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TimeTable
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        public string? Activity {  get; set; }

        public long? ModuleTopicId {  get; set; }
        public ModuleTopic ModuleTopic {  get; set; }

        public string? FacilitatorId { get; set; }
        public AppUser Facilitator { get; set; }

        public bool Publish {  get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }

    }
}
