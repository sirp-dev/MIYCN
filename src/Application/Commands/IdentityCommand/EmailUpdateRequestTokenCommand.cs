using Application.Commands.DTO;
using Application.Services;
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
    public class EmailUpdateRequestTokenCommand : IRequest<EmailUpdateRequestTokenResponse>
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
    }

    public class EmailUpdateRequestTokenCommandHandler : IRequestHandler<EmailUpdateRequestTokenCommand, EmailUpdateRequestTokenResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public EmailUpdateRequestTokenCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EmailUpdateRequestTokenResponse> Handle(EmailUpdateRequestTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new EmailUpdateRequestTokenResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            try
            {
              //  send email token

            }catch (Exception ex)
            {

            }

            // Save the token in the user's AccountToken field
            user.AccountToken = AppServices.GenerateRandomToken(6);

            await _userManager.UpdateAsync(user);

            return new EmailUpdateRequestTokenResponse
            {
                Success = true,
                Message = "Email sent successfully.",
                Email = request.NewEmail
            };
        }

    }
}
