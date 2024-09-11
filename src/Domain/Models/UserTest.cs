using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserTest
    {
        public long Id { get; set; }
        public long TrainingTestId { get; set; }
        public TrainingTest TrainingTest { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public long TrainingId { get; set; }
        public Training Training { get; set; }

        public int Answer {  get; set; }
        public bool Submitted {  get; set; }
        public DateTime Date { get; set; }

    }
}
