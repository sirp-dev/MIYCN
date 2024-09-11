using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingFacilitatorCommand
{
     public sealed class DeleteTrainingFacilitatorCommand : IRequest
    {
        public DeleteTrainingFacilitatorCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingFacilitatorCommandHandler : IRequestHandler<DeleteTrainingFacilitatorCommand>
    {
        private readonly ITrainingFacilitatorRepository _repository;

        public DeleteTrainingFacilitatorCommandHandler(ITrainingFacilitatorRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingFacilitatorCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
