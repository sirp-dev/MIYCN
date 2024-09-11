using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.TrainingFacilitatorCommand
{
    // internal class UpdateTrainingFacilitatorStatusCommand
    public sealed class UpdateTrainingFacilitatorStatusCommand : IRequest
    {
        public UpdateTrainingFacilitatorStatusCommand(long trainingId, long facilitatorId, FacilitatorTrainingStatus status, string userId, string report)
        {
            TrainingId = trainingId;
            FacilitatorId = facilitatorId;
            Status = status;
            UserId = userId;
            Report = report;
        }
        public long TrainingId { get; set; }
        public long FacilitatorId { get; set; }
        public string UserId { get; set; }
        public string Report { get; set; }
        public FacilitatorTrainingStatus Status { get; set; }
    }

    public class UpdateTrainingFacilitatorStatusCommandHandler : IRequestHandler<UpdateTrainingFacilitatorStatusCommand>
    {
        private readonly ITrainingFacilitatorRepository _repository;
        private readonly IMediator _mediator;

        public UpdateTrainingFacilitatorStatusCommandHandler(ITrainingFacilitatorRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UpdateTrainingFacilitatorStatusCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateFacilitatorInTrainingStatus(request.TrainingId, request.FacilitatorId, request.Status, request.Report);
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
