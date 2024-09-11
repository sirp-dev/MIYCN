using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.DTO;
using XYZSMS.Services;

namespace Application.Queries.SmsQueries
{
    public sealed class GetMessageDetailsQuery : IRequest<MessageDetails>
    {
        public string Username { get; }
        public string Password { get; }
        public int MessageId { get; }

        public GetMessageDetailsQuery(string username, string password, int messageId)
        {
            Username = username;
            Password = password;
            MessageId = messageId;
        }
    }

    public class GetMessageDetailsQueryHandler : IRequestHandler<GetMessageDetailsQuery, MessageDetails>
    {
        private readonly ISmsService _smsClient;

        public GetMessageDetailsQueryHandler(ISmsService smsClient)
        {
            _smsClient = smsClient;
        }

        public async Task<MessageDetails> Handle(GetMessageDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _smsClient.GetMessageDetails(request.Username, request.Password, request.MessageId);
        }
    }
}
