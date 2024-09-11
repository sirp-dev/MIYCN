using Application.Commands.EmailCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingParticipantCommand
{
    public sealed class AddTrainingParticipantCommand : IRequest
    {
        public AddTrainingParticipantCommand(TrainingParticipant trainingParticipant)
        {
            TrainingParticipant = trainingParticipant;
        }

        public TrainingParticipant TrainingParticipant { get; set; }


    }

    public class AddTrainingParticipantCommandHandler : IRequestHandler<AddTrainingParticipantCommand>
    {
        private readonly ITrainingParticipantRepository _trainingParticipantRepository;
        private readonly IMediator _mediator;
        private readonly ITrainingRepository _trainingRepository;
        public AddTrainingParticipantCommandHandler(ITrainingParticipantRepository trainingParticipantRepository, IMediator mediator, ITrainingRepository trainingRepository)
        {
            _trainingParticipantRepository = trainingParticipantRepository;
            _mediator = mediator;
            _trainingRepository = trainingRepository;
        }

        public async Task Handle(AddTrainingParticipantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var result = await _trainingParticipantRepository.AddParticipant(request.TrainingParticipant);
                if (result == true)
                {
                    SendTrainingNoticeCommand sendmailcommand = new SendTrainingNoticeCommand(request.TrainingParticipant.UserId,
                            request.TrainingParticipant.TrainingId, "as a PARTICIPANT", "");
                    await _mediator.Send(sendmailcommand);
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}
