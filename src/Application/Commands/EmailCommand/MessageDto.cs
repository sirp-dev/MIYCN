using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailCommand
{
    public class MessageDto
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
}
