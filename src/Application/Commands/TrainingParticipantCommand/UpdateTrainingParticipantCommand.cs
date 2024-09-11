using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingParticipantCommand
{
     public sealed class UpdateTrainingParticipantCommand : IRequest
    {
        public UpdateTrainingParticipantCommand(TrainingParticipant movie)
        {
            TrainingParticipant = movie;
        }

        public TrainingParticipant TrainingParticipant { get; set; }


    }

    public class UpdateTrainingParticipantCommandHandler : IRequestHandler<UpdateTrainingParticipantCommand>
    {
        private readonly ITrainingParticipantRepository _repository;

        public UpdateTrainingParticipantCommandHandler(ITrainingParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTrainingParticipantCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TrainingParticipant);
        }
    }
}
