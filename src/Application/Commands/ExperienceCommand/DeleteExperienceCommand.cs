using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ExperienceCommand
{
    public sealed class DeleteExperienceCommand : IRequest
    {
        public DeleteExperienceCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand>
    {
        private readonly IExperienceRepository _repository;

        public DeleteExperienceCommandHandler(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
