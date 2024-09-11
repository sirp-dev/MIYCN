using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IdentityQueries
{

    public class GetUserListByRoleListDto : IRequest<IEnumerable<ProfileDto>>
    {
        public GetUserListByRoleListDto(IEnumerable<string> roles)
        {
            Roles = roles.ToList();
        }

        public List<string> Roles { get; set; }
    }


    public class GetUserListByRoleListDtoHandler : IRequestHandler<GetUserListByRoleListDto, IEnumerable<ProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserListByRoleListDtoHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ProfileDto>> Handle(GetUserListByRoleListDto request, CancellationToken cancellationToken)
        {


            var UserDatas = _userManager.Users
        .Where(x => x.Email != "universaadmin@miycn.ng" && x.Email != "jinmcever@miycn.ng")
        .AsEnumerable()
        .Where(x => x.Role != null && request.Roles.Any(role => x.Role.Split(',').Contains(role)))
        //.Where(x => request.Roles.Any(role => x.Role.Split(',').Contains(role)))
        .ToList();


            //var UserDatas = _userManager.Users
            //    .Where(x => x.Email != "universaadmin@miycn.ng" && x.Email != "jinmcever@miycn.ng")
            //    .Where(x => request.Roles.Contains(x.Role))
            //    .AsEnumerable();

            var UserDatasList = UserDatas.Select(x => new ProfileDto
            {
                Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName,
                Phone = x.PhoneNumber,
                Email = x.Email,
                Status = x.UserStatus,
                Id = x.Id,
                Category = x.Role
            });

            return await Task.FromResult(UserDatasList);
        }
    }

}
