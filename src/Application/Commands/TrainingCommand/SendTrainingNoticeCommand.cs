using Application.Commands.EmailCommand;
using Application.Commands.SmsCommand;
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

namespace Application.Commands.TrainingCommand
{
    public sealed class SendTrainingNoticeCommand : IRequest
    {
        public SendTrainingNoticeCommand(string? userId, long trainingId, string? title, string? position)
        {
            UserId = userId;
            TrainingId = trainingId;
            Title = title;
            Position = position;
        }

        public string? UserId { get; set; }
        public long TrainingId { get; set; }
        public string? Title { get; set; }
        public string? Position { get; set; }

    }

    public class SendTrainingNoticeCommandHandler : IRequestHandler<SendTrainingNoticeCommand>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMediator _mediator;

        public SendTrainingNoticeCommandHandler(ITrainingRepository trainingRepository, IMediator mediator)
        {
            _trainingRepository = trainingRepository;
            _mediator = mediator;
        }

        public async Task Handle(SendTrainingNoticeCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var getTraining = await _trainingRepository.GetByIdAsync(request.TrainingId);

                string messagedetails = $"<h4>{getTraining.Title}</h4> <br><br>" +
                    $"You have been selected to attend the MIYCN Training {request.Title} at <b>{getTraining.Address} from {getTraining.StartDate.ToString("dd MMM, yyyy")} to {getTraining.EndDate.ToString("dd MMM, yyyy")}. </b>" +
                    $"To participate, kindly complete your registration with the login details provided below:\r\n<br><br>";


                GetUserByIdQuery getUser = new GetUserByIdQuery(request.UserId);
                var userdata = await _mediator.Send(getUser);



                try
                {



                    string baseUrl = "https://miycnportal.ng/"; // replace with your application base URL
                    string relativeUrl = "/Identity/Account/Login";
                    Uri baseUri = new Uri(baseUrl);
                    Uri loginUri = new Uri(baseUri, relativeUrl);

                    // Now you can use the loginUri to create the link
                    string linkcomplete = loginUri.AbsoluteUri;

                    string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                    string messagedetails2 = $"Login Email: {userdata.Email} <br>Temporal Password: {userdata.TempPass}<br>" +
                        $"" + datavalue + "or copy the link before and paste in your browser to continue<br><br>" +
                        linkcomplete + "<br>" + 

                        $"<h6>Instruction: Kindly login with the temporal password, reset your password and update your profile.</h6>" +
                        $"<h4>Kindly note that this training is organized by the Nutrition department of the federal ministry of health and social welfare</h4>"
                        
                        ;

                    //send email
                    MessageDto msn = new MessageDto();
                    msn.Email = userdata.Email;
                    msn.Message = messagedetails + messagedetails2;
                    msn.Subject = "MIYCN TRAINING";
                    msn.Name = userdata.FullnameX;
                    SendMessageCommand emailcommand = new SendMessageCommand(msn);
                    PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                }
                catch (Exception e)
                {

                }
                try
                {
                    SendSmsCommand smscommand = new SendSmsCommand(userdata.PhoneNumber, "Kindly check your email to complete your registration.");
                    var smsresponse = await _mediator.Send(smscommand);

                }
                catch (Exception c) { }

            }
            catch (Exception ex) { }


        }
    }
}
