using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserTestCommand
{
      public sealed class DeleteUserTestCommand : IRequest
    {
        public DeleteUserTestCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteUserTestCommandHandler : IRequestHandler<DeleteUserTestCommand>
    {
        private readonly IUserTestRepository _repository;

        public DeleteUserTestCommandHandler(IUserTestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteUserTestCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
