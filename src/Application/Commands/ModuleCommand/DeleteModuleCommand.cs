using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ModuleCommand
{
      public sealed class DeleteModuleCommand : IRequest
    {
        public DeleteModuleCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly IModuleRepository _repository;

        public DeleteModuleCommandHandler(IModuleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
