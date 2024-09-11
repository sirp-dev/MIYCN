using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Experience
    {
        public long Id { get; set; }
        public string? Title { get; set; }    
        public string? Description { get; set; }
        public string? Year { get; set; }
    }
}
