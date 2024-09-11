using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class DialyEvaluationQuestion
    {
        public long Id { get; set; }

        public string? Question { get; set; }
        public int SortOrder { get; set; }
        public bool IsModuleTopic { get; set; }
        public EvaluationAnswerType EvaluationAnswerType { get; set; }


        public long DialyActivityId { get; set; }
        public DialyActivity DialyActivity { get; set; }

    }
}
