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
      public sealed class AddProfileCategoryCommand : IRequest
    {
        public AddProfileCategoryCommand(ProfileCategory profileCategory)
        {
            ProfileCategory = profileCategory;
        }

        public ProfileCategory ProfileCategory { get; set; }


    }

    public class AddProfileCategoryCommandHandler : IRequestHandler<AddProfileCategoryCommand>
    {
        private readonly IProfileCategoryRepository _profileCategoryRepository;

        public AddProfileCategoryCommandHandler(IProfileCategoryRepository profileCategoryRepository)
        {
            _profileCategoryRepository = profileCategoryRepository;
        }

        public async Task Handle(AddProfileCategoryCommand request, CancellationToken cancellationToken)
        {

            await _profileCategoryRepository.AddAsync(request.ProfileCategory);


        }
    }
}
