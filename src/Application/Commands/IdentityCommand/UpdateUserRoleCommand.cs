using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class UpdateUserRoleCommand : IRequest<string>
    {
        public UpdateUserRoleCommand(string userId, string roleId, string fullname)
        {
            UserId = userId;
            RoleId = roleId;
            Fullname = fullname;
        }

        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Fullname { get; set; }
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UpdateUserRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
            {
                throw new ArgumentException("Role not found");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var isUserInRole = await _userManager.IsInRoleAsync(user, role.Name);
            try
            {
                if (isUserInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                    user.Role = string.Join(",", (await _userManager.GetRolesAsync(user)).Where(r => r != role.Name));
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                    var roles = await _userManager.GetRolesAsync(user);
                    user.Role = string.Join(",", roles);
                }

                // Save the updated user
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Failed to update user");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                throw new InvalidOperationException("Failed to update user role", ex);
            }

            return user.Id;
        }

    }

}
