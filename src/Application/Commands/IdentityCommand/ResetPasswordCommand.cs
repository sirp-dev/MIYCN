using Application.Commands.DTO;
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
    public class ResetPasswordCommand : IRequest<AppResponse>
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new AppResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return new AppResponse
                {
                    Success = false,
                    Message = "Unable to Validate."
                };
            }

            var setPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!setPasswordResult.Succeeded)
            {
                return new AppResponse
                {
                    Success = false,
                    Message = "Unable to Validate."
                };
            }

            return new AppResponse
            {
                Success = true,
                Message = "Password reset successfully."
            };
        }
    }
}