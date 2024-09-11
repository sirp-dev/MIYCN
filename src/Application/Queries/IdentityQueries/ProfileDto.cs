using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Queries.IdentityQueries
{
    public class ProfileDto
    {
        public string Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }
        public GenderStatus Gender { get; set; }
        public string Roles { get; set; }
        public string Category { get; set; }
    }
}
