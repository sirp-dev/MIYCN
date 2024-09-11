using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class TrainingTest
    {
        public long Id { get; set; }
        public TrainingTestType TrainingTestType { get; set; }
        public string Question { get; set; }
        public bool Publish { get; set; }
        public int SortOrder { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
