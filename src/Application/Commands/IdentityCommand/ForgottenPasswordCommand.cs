using Application.Commands.DTO;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class ForgottenPasswordCommand : IRequest<ForgottenPasswordResponse>
    {
        public string Email { get; set; }
    }

    public class ForgottenPasswordCommandHandler : IRequestHandler<ForgottenPasswordCommand, ForgottenPasswordResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public ForgottenPasswordCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ForgottenPasswordResponse> Handle(ForgottenPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ForgottenPasswordResponse
                {
                    Success = false,
                    Message = "User not found with the provided email address."
                };
            }

            // Generate password reset token
            // Generate 6-digit random token
            var token = AppServices.GenerateRandomToken(6);

            // Save the token in the user's accountToken field
            user.AccountToken = token;
            await _userManager.UpdateAsync(user);

            // Construct password reset callback URL
            
            // Send password reset email
            try
            {
                // Call your email sending service
                // For example:
                // await _emailService.SendPasswordResetEmail(user.Email, callbackUrl);

                // For demonstration purposes, log the email and token
                Console.WriteLine($"Password reset email sent to: {user.Email}");
                Console.WriteLine($"Token: {token}");

                return new ForgottenPasswordResponse
                {
                    Success = true,
                    Message = "Password reset email sent successfully."
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error sending password reset email: {ex.Message}");
                return new ForgottenPasswordResponse
                {
                    Success = false,
                    Message = "Failed to send password reset email. Please try again later."
                };
            }
        }
    }
}