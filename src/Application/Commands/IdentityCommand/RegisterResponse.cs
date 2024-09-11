using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class RegisterResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool Success { get; set; } 
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
