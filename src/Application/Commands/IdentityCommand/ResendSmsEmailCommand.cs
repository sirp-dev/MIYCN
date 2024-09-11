using Application.Commands.EmailCommand;
using Application.Commands.SmsCommand;
using Application.Queries.IdentityQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public sealed class ResendSmsEmailCommand : IRequest<bool>
    {
        public ResendSmsEmailCommand(string id, bool sendSms, bool sendEmail)
        {
            Id = id;
            SendSms = sendSms;
            SendEmail = sendEmail;
        }

        public string Id { get; set; }
        public bool SendSms { get; set; }
        public bool SendEmail { get; set; }
    }


    public class ResendSmsEmailCommandHandler : IRequestHandler<ResendSmsEmailCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;


        public ResendSmsEmailCommandHandler(
            UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }


        public async Task<bool> Handle(ResendSmsEmailCommand request, CancellationToken cancellationToken)
        {

            var UserDatas = await _userManager.FindByIdAsync(request.Id);
            if (request.SendEmail == true)
            {
                try
                {
                    string baseUrl = "https://miycnportal.ng/"; // replace with your application base URL
                    string relativeUrl = "/Identity/Account/Login";
                    Uri baseUri = new Uri(baseUrl);
                    Uri loginUri = new Uri(baseUri, relativeUrl);

                    // Now you can use the loginUri to create the link
                    string linkcomplete = loginUri.AbsoluteUri;

                    string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                    string messagedetails = $"Login Email: {UserDatas.Email} <br>Temporal Password: {UserDatas.TempPass}<br>" +
                        $"" + datavalue + "or copy the link before and paste in your browser to continue<br><br>" +
                        linkcomplete + "<br>" +
                        "<h4>INSTRUCTION</h4>" +

                        $"<h6>Kindly login, reset your password and update your profile.</h6>";

                    //send email
                    MessageDto msn = new MessageDto();
                    msn.Email = UserDatas.Email;
                    msn.Message = messagedetails;
                    msn.Subject = "MIYCN REGISTRATION";
                    msn.Name = UserDatas.FullnameX;
                    SendMessageCommand emailcommand = new SendMessageCommand(msn);
                    PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                    if (responseemail.Status == PostmarkStatus.Success)
                    {
                        UserDatas.EmailSent = true;
                        await _userManager.UpdateAsync(UserDatas);
                        return true;
                    }

                }
                catch (Exception e)
                {

                }

            }
            if (request.SendSms == true)
            {
                try
                {

                    string smsm = $"Email: {UserDatas.Email} \nPasswd: {UserDatas.TempPass} \nLink: www.miycnportal.ng";
                    //SendSmsCommand smscommand = new SendSmsCommand(UserDatas.PhoneNumber, "Kindly check your email to complete your registration.");
                    SendSmsCommand smscommand = new SendSmsCommand(UserDatas.PhoneNumber, smsm);
                    var smsresponse = await _mediator.Send(smscommand);
                    UserDatas.SmsSent = true;
                    await _userManager.UpdateAsync(UserDatas);
                    return true;
                }
                catch (Exception c) { }
            }
            return false;
        }
    }
}