using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class RegisterDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string PlaceOfWork { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Position { get; set; }
    }
}
