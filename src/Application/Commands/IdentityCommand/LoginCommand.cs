using Application.Commands.DTO;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                // Handle invalid login
                throw new Exception("Invalid login attempt");
            }

            // Generate JWT token
            var token = AppServices.GenerateJwtToken(user, Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));

            return new LoginResponse
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                UserStatus = user.UserStatus,
                Verified = user.EmailConfirmed,
                Success = true
            };
        }

      
    }

}
