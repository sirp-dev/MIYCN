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
    public sealed class GetMessageHistoryQuery : IRequest<List<MessageHistoryItem>>
    {
        public string Username { get; }
        public string Password { get; }

        public GetMessageHistoryQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class GetMessageHistoryQueryHandler : IRequestHandler<GetMessageHistoryQuery, List<MessageHistoryItem>>
    {
        private readonly ISmsService _smsClient;

        public GetMessageHistoryQueryHandler(ISmsService smsClient)
        {
            _smsClient = smsClient;
        }

        public async Task<List<MessageHistoryItem>> Handle(GetMessageHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _smsClient.GetMessageHistory(request.Username, request.Password);
        }
    }
}
