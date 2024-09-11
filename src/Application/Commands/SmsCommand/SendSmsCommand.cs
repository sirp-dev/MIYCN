using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.Services;

namespace Application.Commands.SmsCommand
{
      public sealed class SendSmsCommand : IRequest<string>
    {
        
        public string Recipients { get; } 
        public string SmsMessage { get; } 

        public SendSmsCommand(string recipients, string smsMessage )
        {
           
            Recipients = recipients; 
            SmsMessage = smsMessage; 
        }
    }

    public class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, string>
    {
        private readonly ISmsService _smsClient;

        public SendSmsCommandHandler(ISmsService smsClient)
        {
            _smsClient = smsClient;
        }

        public async Task<string> Handle(SendSmsCommand request, CancellationToken cancellationToken)
        {

            return await _smsClient.ComposeMessage("", "", request.Recipients, "MIYCN", request.SmsMessage, "SendNow");

        }
    }
}
