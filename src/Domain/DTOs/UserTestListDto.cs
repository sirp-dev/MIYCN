using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserTestListDto
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string PreTestScore {  get; set; } 
        public string PostTestScore {  get; set; }

        public bool PreTestTaken { get; set; }
        public bool PostTestTaken { get; set; }
    }
}
