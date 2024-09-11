using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class RegisterResponseDto
    {
        public string UserId { get; set; }
        public string Role {  get; set; }
        public bool Success { get; set; }
        public bool AddedToRole { get; set; }
        public bool EmailSent { get; set; }
        public bool SmsSent { get; set; }
        public string Message { get; set; }
        public bool AddedToTraining { get; set; }
        public long TrainingId { get; set; }
    }
}
