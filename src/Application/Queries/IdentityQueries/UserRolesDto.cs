using Amazon;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IdentityQueries
{
    public class UserRolesDto
    {
        public IList<string> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public IList<string> RemainingUserRoles { get; set; }
        public AppUser UserInfo { get; set; }
        public string Fullname { get; set; }
        public string Id { get; set; }
    }
}
