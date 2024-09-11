using Application.Commands.DTO;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.IdentityCommand
{
    public class UpdateUserProfileCommand : IRequest<AppResponse>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderStatus Gender { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        // Add other properties as needed
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
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

            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            user.StateOrigin = request.State;
            user.Country = request.Country;
            // Update other properties as needed

            var result = await _userManager.UpdateAsync(user);

            return new AppResponse
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "User profile updated successfully." : "Failed to update user profile."
            };
        }
    }
}