using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }

        public ICollection<Training> Training { get; set; }
    }
}
