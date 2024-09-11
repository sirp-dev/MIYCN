using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IdentityQueries
{
     
    public class GetUserPermissionListQuery : IRequest<UserRolesDto>
    {
        public GetUserPermissionListQuery(string userId)
        {
            UserId = userId; 
        }

        public string UserId { get; set; }
        public string Fullname { get; set; }
    }
    public class GetUserPermissionListQueryHandler : IRequestHandler<GetUserPermissionListQuery, UserRolesDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public GetUserPermissionListQueryHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserRolesDto> Handle(GetUserPermissionListQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }
            //
            try
            {
                AppRole xRole = new AppRole("AdminEditor");
                var checkrole = await _roleManager.FindByNameAsync("AdminEditor");
                if (checkrole == null)
                {
                    await _roleManager.CreateAsync(xRole);

                }
            }
            catch(Exception c)
            {

            }
            var roles = await _roleManager.Roles
                .Where(x => x.Name != "mSuperAdmin")
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            var userInfo = await _userManager.FindByIdAsync(request.UserId);
            if (userInfo == null)
            {
                throw new ArgumentException("User not found");
            }

            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var remainingRoles = roles.Except(userRoles).ToList();

            // Uncomment and modify this block if you need to update the user profile with roles information
            // var profile = await _userManager.FindByIdAsync(request.UserId);
            // var rolesList = string.Join(", ", userRoles);
            // profile. // Assign rolesList to the appropriate property
            // await _userManager.UpdateAsync(profile);AdminEditor



            return new UserRolesDto
            {
                Roles = roles,
                UserRoles = userRoles.ToList(),
                RemainingUserRoles = remainingRoles,
                UserInfo = userInfo,
                Fullname = userInfo.FullnameX,
                Id = userInfo.Id
            };
        }
    }

}
