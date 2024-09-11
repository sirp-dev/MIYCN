using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.Services;

namespace Application.Commands.SmsCommand
{
    public sealed class ComposeMessageCommand : IRequest<string>
    {
        public string Username { get; }
        public string Password { get; }
        public string Recipients { get; }
        public string SenderId { get; }
        public string SmsMessage { get; }
        public string SmsSendOption { get; }

        public ComposeMessageCommand(string username, string password, string recipients, string senderId, string smsMessage, string smsSendOption)
        {
            Username = username;
            Password = password;
            Recipients = recipients;
            SenderId = senderId;
            SmsMessage = smsMessage;
            SmsSendOption = smsSendOption;
        }
    }

    public class ComposeMessageCommandHandler : IRequestHandler<ComposeMessageCommand, string>
    {
        private readonly ISmsService _smsClient;

        public ComposeMessageCommandHandler(ISmsService smsClient)
        {
            _smsClient = smsClient;
        }

        public async Task<string> Handle(ComposeMessageCommand request, CancellationToken cancellationToken)
        {
            return await _smsClient.ComposeMessage(request.Username, request.Password, request.Recipients, request.SenderId, request.SmsMessage, request.SmsSendOption);
        }
    }
}
