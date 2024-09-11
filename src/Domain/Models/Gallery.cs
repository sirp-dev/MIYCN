using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Gallery
    {

        public long Id { get; set; }
        public string? Title { get; set; }
        public string? ImageDescription { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageKey { get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
