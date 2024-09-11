using Application.Commands.EmailCommand;
using Application.Commands.SmsCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Commands.TrainingParticipantCommand
{
    public sealed class UpdateTrainingParticipantStatusCommand : IRequest
    {
        public UpdateTrainingParticipantStatusCommand(long trainingId, long participantId, ParticipantTrainingStatus status, string userId, string report)
        {
            TrainingId = trainingId;
            ParticipantId = participantId;
            Status = status;
            UserId = userId;
            Report = report;
        }
        public long TrainingId { get; set; }
        public long ParticipantId { get; set; }
        public string UserId { get; set; }
        public string Report { get; set; }
        public ParticipantTrainingStatus Status { get; set; }
    }

    public class UpdateTrainingParticipantStatusCommandHandler : IRequestHandler<UpdateTrainingParticipantStatusCommand>
    {
        private readonly ITrainingParticipantRepository _repository;
        private readonly IMediator _mediator;

        public UpdateTrainingParticipantStatusCommandHandler(ITrainingParticipantRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UpdateTrainingParticipantStatusCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateParticipantInTrainingStatus(request.TrainingId, request.ParticipantId, request.Status, request.Report);
            var getuser = new GetUserByIdQuery(request.UserId);
            var user = await _mediator.Send(getuser);
            //try
            //{
               
            //    string messagedetails = $"Your Training has been disabled.<br>" +
            //        $"<h4>Reason</h4>" +
                    
            //        $"<p>{request.Report}</p>" +

            //        $"<h6>Thank You</h6>";

            //    //send email
            //    MessageDto msn = new MessageDto();
            //    msn.Email = user.Email;
            //    msn.Message = messagedetails;
            //    msn.Subject = "MIYCN TRAINING";
            //    msn.Name = user.FullnameX;
            //    SendMessageCommand emailcommand = new SendMessageCommand(msn);
            //    PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                 

            //}
            //catch (Exception e)
            //{

            //}
            //try
            //{
            //    SendSmsCommand smscommand = new SendSmsCommand(user.PhoneNumber, "Kindly check your email your training has been disabled");
            //    var smsresponse = await _mediator.Send(smscommand);
                
            //}
            //catch (Exception c) { }
        }
    }
}
