using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProfileCategoryCommand
{
     public sealed class UpdateProfileCategoryCommand : IRequest
    {
        public UpdateProfileCategoryCommand(ProfileCategory data)
        {
            ProfileCategory = data;
        }

        public ProfileCategory ProfileCategory { get; set; }


    }

    public class UpdateProfileCategoryCommandHandler : IRequestHandler<UpdateProfileCategoryCommand>
    {
        private readonly IProfileCategoryRepository _repository;

        public UpdateProfileCategoryCommandHandler(IProfileCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProfileCategoryCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.ProfileCategory);
        }
    }
}
