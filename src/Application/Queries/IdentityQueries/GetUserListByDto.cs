using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Queries.IdentityQueries
{

    public class GetUserListByDto : IRequest<IEnumerable<ProfileDto>> { }

    public class GetUserListByDtoHandler : IRequestHandler<GetUserListByDto, IEnumerable<ProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserListByDtoHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ProfileDto>> Handle(GetUserListByDto request, CancellationToken cancellationToken)
        {
            var UserDatas = _userManager.Users
                .Where(x => x.Email != "universaadmin@miycn.ng" && x.Email != "jinmcever@miycn.ng")
                .AsEnumerable();

            var UserDatasList = UserDatas.Select(x => new ProfileDto
            {
                Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName,
                Phone = x.PhoneNumber,
                Email = x.Email,
                Status = x.UserStatus,
                Id = x.Id,
                Category = x.Role
            });
            return UserDatasList;
        }
    }
}
