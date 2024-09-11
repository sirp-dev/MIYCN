using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class TestSheet
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public TestStatus TestStatus { get; set; }

        public long TestCategoryId { get; set; }
        public TestCategory TestCategory { get; set; }
    }
}
