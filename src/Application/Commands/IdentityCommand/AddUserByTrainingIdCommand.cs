using Application.Commands.TrainingFacilitatorCommand;
using Application.Commands.TrainingParticipantCommand;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public sealed class AddUserByTrainingIdCommand : IRequest<RegisterResponseDto>
    {
        public AddUserByTrainingIdCommand(RegisterDto registerDto, string role, long trainingId, string? existingUserId)
        {
            RegisterDto = registerDto;
            Role = role;
            TrainingId = trainingId;
            ExistingUserId = existingUserId;
        }

        public RegisterDto RegisterDto { get; set; }
        public string Role { get; set; }
        public long TrainingId { get; set; }
        public string? ExistingUserId { get; set; }

    }

    public class AddUserByTrainingIdCommandHandler : IRequestHandler<AddUserByTrainingIdCommand, RegisterResponseDto>
    {
        private readonly IMediator _mediator;

        public AddUserByTrainingIdCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RegisterResponseDto> Handle(AddUserByTrainingIdCommand request, CancellationToken cancellationToken)
        {
            RegisterResponseDto regResponse = new RegisterResponseDto();
            if (request.ExistingUserId == null)
            {
                AddUserCommand regCommand = new AddUserCommand(request.RegisterDto, request.Role);
                regResponse = await _mediator.Send(regCommand);

                if (regResponse.Role != null && regResponse.Success == true) { 
                if (regResponse.Role.ToLower() == "participant" && regResponse.Success == true)
                {
                    TrainingParticipant tmodel = new TrainingParticipant();
                    tmodel.TrainingId = request.TrainingId;
                    tmodel.UserId = regResponse.UserId;
                    try
                    {
                        AddTrainingParticipantCommand tcomand = new AddTrainingParticipantCommand(tmodel);
                        await _mediator.Send(tcomand);
                        regResponse.AddedToTraining = true;
                        regResponse.TrainingId = tmodel.TrainingId;
                        regResponse.Success = true; 
                        regResponse.Role = "Participant";
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (regResponse.Role.ToLower() == "facilitator")
                {
                    TrainingFacilitator fmodel = new TrainingFacilitator();
                    fmodel.TrainingId = request.TrainingId;
                    fmodel.UserId = regResponse.UserId;
                    fmodel.Position = request.RegisterDto.Position;
                    try
                    {
                        AddTrainingFacilitatorCommand tcomand = new AddTrainingFacilitatorCommand(fmodel);
                        await _mediator.Send(tcomand);
                        regResponse.AddedToTraining = true;
                        regResponse.TrainingId = fmodel.TrainingId;
                        regResponse.Role = "Facilitator";
                        regResponse.Success = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                }
            }
            else
            {
                TrainingParticipant tmodel = new TrainingParticipant();
                tmodel.TrainingId = request.TrainingId;
                tmodel.UserId = request.ExistingUserId;
                try
                {

                    AddTrainingParticipantCommand tcomand = new AddTrainingParticipantCommand(tmodel);
                    await _mediator.Send(tcomand);

                    //


                    regResponse.AddedToTraining = true;
                    regResponse.TrainingId = tmodel.TrainingId;
                    regResponse.Success = true;
                    regResponse.UserId = tmodel.UserId;
                    regResponse.Role = "Participant";
                    //id = response.UserId, rx = response.Role, txid = response.TrainingId, area="User"
                }
                catch (Exception ex)
                {

                }
            }
            return regResponse;

        }
    }
}
