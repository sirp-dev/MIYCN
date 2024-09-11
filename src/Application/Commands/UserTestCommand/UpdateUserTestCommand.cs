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
     public sealed class UpdateUserTestCommand : IRequest
    {
        public UpdateUserTestCommand(UserTest movie)
        {
            UserTest = movie;
        }

        public UserTest UserTest { get; set; }


    }

    public class UpdateUserTestCommandHandler : IRequestHandler<UpdateUserTestCommand>
    {
        private readonly IUserTestRepository _repository;

        public UpdateUserTestCommandHandler(IUserTestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserTestCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.UserTest);
        }
    }
}
