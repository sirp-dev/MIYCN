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
      public sealed class AddProfileCategoryListCommand : IRequest
    {
        public AddProfileCategoryListCommand(ProfileCategoryList profileCategoryList)
        {
            ProfileCategoryList = profileCategoryList;
        }

        public ProfileCategoryList ProfileCategoryList { get; set; }


    }

    public class AddProfileCategoryListCommandHandler : IRequestHandler<AddProfileCategoryListCommand>
    {
        private readonly IProfileCategoryListRepository _profileCategoryListRepository;

        public AddProfileCategoryListCommandHandler(IProfileCategoryListRepository profileCategoryListRepository)
        {
            _profileCategoryListRepository = profileCategoryListRepository;
        }

        public async Task Handle(AddProfileCategoryListCommand request, CancellationToken cancellationToken)
        {

            await _profileCategoryListRepository.AddAsync(request.ProfileCategoryList);


        }
    }
}
