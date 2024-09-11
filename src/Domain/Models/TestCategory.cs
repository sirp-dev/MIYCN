using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TestCategory
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public ICollection<TestSheet> TestSheets { get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
