using Application.Commands.EmailCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Application.Commands.TrainingFacilitatorCommand
{
    public sealed class AddTrainingFacilitatorCommand : IRequest
    {
        public AddTrainingFacilitatorCommand(TrainingFacilitator trainingFacilitator)
        {
            TrainingFacilitator = trainingFacilitator;
        }

        public TrainingFacilitator TrainingFacilitator { get; set; }


    }

    public class AddTrainingFacilitatorCommandHandler : IRequestHandler<AddTrainingFacilitatorCommand>
    {
        private readonly ITrainingFacilitatorRepository _trainingFacilitatorRepository;
        private readonly IMediator _mediator;
        private readonly ITrainingRepository _trainingRepository;
        public AddTrainingFacilitatorCommandHandler(ITrainingFacilitatorRepository trainingFacilitatorRepository, IMediator mediator, ITrainingRepository trainingRepository)
        {
            _trainingFacilitatorRepository = trainingFacilitatorRepository;
            _mediator = mediator;
            _trainingRepository = trainingRepository;
        }

        public async Task Handle(AddTrainingFacilitatorCommand request, CancellationToken cancellationToken)
        {
            var check = await _trainingFacilitatorRepository.FacilitatorInTraining(request.TrainingFacilitator.TrainingId, request.TrainingFacilitator.UserId);
            if (check == null)
            {
                try
                {
                    var result = await _trainingFacilitatorRepository.AddFacilitator(request.TrainingFacilitator);
                    if (result == true)
                    {

                        string titlex = "";

                        if(!request.TrainingFacilitator.Position.Contains("Admin") || !request.TrainingFacilitator.Position.Contains("Staff"))
                        {
                            titlex = "as a FACILITATOR";
                        }
                        else
                        {
                            titlex = "as a STAFF";
                        }

                         
                        SendTrainingNoticeCommand sendmailcommand = new SendTrainingNoticeCommand(request.TrainingFacilitator.UserId,
                            request.TrainingFacilitator.TrainingId, titlex, request.TrainingFacilitator.Position);
                        await _mediator.Send(sendmailcommand);

                    }
                }
                catch (Exception ex) { }
            }



        }
    }
}
