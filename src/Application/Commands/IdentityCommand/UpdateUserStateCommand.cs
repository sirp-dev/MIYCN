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
     public class UpdateUserStateCommand : IRequest<AppResponse>
    {
        public UpdateUserStateCommand(string state, string userId)
        {
            State = state;
            UserId = userId;
        }

        public string UserId { get; set; }
        public string State { get; set; }
        
    }

    public class UpdateUserStateCommandHandler : IRequestHandler<UpdateUserStateCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserStateCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse> Handle(UpdateUserStateCommand request, CancellationToken cancellationToken)
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
             
            user.AssignedState = request.State; 

            var result = await _userManager.UpdateAsync(user);

            return new AppResponse
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "User profile updated successfully." : "Failed to update user profile."
            };
        }
    }
}
