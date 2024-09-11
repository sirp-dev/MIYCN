using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.Services;

namespace Application.Commands.SmsCommand
{
    public sealed class CheckBalanceCommand : IRequest<string>
    {
        public string Username { get; }
        public string Password { get; }

        public CheckBalanceCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class CheckBalanceCommandHandler : IRequestHandler<CheckBalanceCommand, string>
    {
        private readonly ISmsService _smsClient;

        public CheckBalanceCommandHandler(ISmsService smsClient)
        {
            _smsClient = smsClient;
        }

        public async Task<string> Handle(CheckBalanceCommand request, CancellationToken cancellationToken)
        {
            return await _smsClient.CheckBalance(request.Username, request.Password);
        }
    }
}
