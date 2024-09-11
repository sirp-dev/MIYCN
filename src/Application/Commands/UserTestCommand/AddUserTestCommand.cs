using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserTestCommand
{
     public sealed class AddUserTestCommand : IRequest
    {
        public AddUserTestCommand(UserTest userTest)
        {
            UserTest = userTest;
        }

        public UserTest UserTest { get; set; }


    }

    public class AddUserTestCommandHandler : IRequestHandler<AddUserTestCommand>
    {
        private readonly IUserTestRepository _UserTestRepository;

        public AddUserTestCommandHandler(IUserTestRepository UserTestRepository)
        {
            _UserTestRepository = UserTestRepository;
        }

        public async Task Handle(AddUserTestCommand request, CancellationToken cancellationToken)
        {

            await _UserTestRepository.AddAsync(request.UserTest);


        }
    }
}
