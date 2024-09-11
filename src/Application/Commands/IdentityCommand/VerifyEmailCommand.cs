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
    public class VerifyEmailCommand : IRequest<VerifyEmailResponse>
    {
        public string UserId { get; set; }
    }

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public VerifyEmailCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<VerifyEmailResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new VerifyEmailResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            if (user.EmailConfirmed)
            {
                return new VerifyEmailResponse
                {
                    Success = true,
                    Message = "Email is already confirmed."
                };
            }

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            return new VerifyEmailResponse
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "Email confirmed successfully." : "Failed to confirm email."
            };
        }
    }
}