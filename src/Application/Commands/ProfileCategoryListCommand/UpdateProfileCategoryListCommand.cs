using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProfileCategoryListCommand
{
     public sealed class UpdateProfileCategoryListCommand : IRequest
    {
        public UpdateProfileCategoryListCommand(ProfileCategoryList data)
        {
            ProfileCategoryList = data;
        }

        public ProfileCategoryList ProfileCategoryList { get; set; }


    }

    public class UpdateProfileCategoryListCommandHandler : IRequestHandler<UpdateProfileCategoryListCommand>
    {
        private readonly IProfileCategoryListRepository _repository;

        public UpdateProfileCategoryListCommandHandler(IProfileCategoryListRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProfileCategoryListCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.ProfileCategoryList);
        }
    }
}
