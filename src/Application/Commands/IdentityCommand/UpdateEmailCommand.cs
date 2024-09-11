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
    public class UpdateEmailCommand : IRequest<AppResponse>
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
    }

    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateEmailCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
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

            var existingUser = await _userManager.FindByEmailAsync(request.NewEmail);
            if (existingUser != null)
            {
                return new AppResponse
                {
                    Success = false,
                    Message = "Email address already exists."
                };
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);

            return new AppResponse
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "Email address updated successfully." : "Failed to update email address."
            };
        }
    }
}
