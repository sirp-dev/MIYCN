using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Sponsor
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoKey { get; set; }
        public string? Website { get; set; }


        public long TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
