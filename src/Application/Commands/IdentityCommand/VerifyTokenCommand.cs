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
    public class VerifyTokenCommand : IRequest<AppResponse>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class VerifyTokenCommandHandler : IRequestHandler<VerifyTokenCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public VerifyTokenCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
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

            var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.Token);

            return new AppResponse
            {
                Success = result,
                Message = result ? "Token is valid." : "Token is not valid or expired."
            };
        }
    }
}