using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DialyUserEvaluation
    {
        public long Id { get; set; }
        public long DialyEvaluationQuestionId { get; set; }
        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public long DialyActivityId { get; set; }
        public DialyActivity DialyActivity { get; set; }

        public string? Answer { get; set; }
        public bool Submitted { get; set; }
        public DateTime Date { get; set; }
    }
}
