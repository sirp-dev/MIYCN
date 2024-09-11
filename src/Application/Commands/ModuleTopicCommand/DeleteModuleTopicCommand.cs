using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ModuleTopicCommand
{
    public sealed class DeleteModuleTopicCommand : IRequest
    {
        public DeleteModuleTopicCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteModuleTopicCommandHandler : IRequestHandler<DeleteModuleTopicCommand>
    {
        private readonly IModuleTopicRepository _repository;

        public DeleteModuleTopicCommandHandler(IModuleTopicRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteModuleTopicCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
