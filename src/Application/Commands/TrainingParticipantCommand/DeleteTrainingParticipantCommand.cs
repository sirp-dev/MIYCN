using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingParticipantCommand
{
     public sealed class DeleteTrainingParticipantCommand : IRequest
    {
        public DeleteTrainingParticipantCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingParticipantCommandHandler : IRequestHandler<DeleteTrainingParticipantCommand>
    {
        private readonly ITrainingParticipantRepository _repository;

        public DeleteTrainingParticipantCommandHandler(ITrainingParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingParticipantCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
