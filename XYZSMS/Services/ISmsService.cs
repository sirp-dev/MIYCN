using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.DTO;

namespace XYZSMS.Services
{
    public interface ISmsService
    {
        Task<string> CheckBalance(string username, string password);
        Task<List<MessageHistoryItem>> GetMessageHistory(string username, string password);
        Task<MessageDetails> GetMessageDetails(string username, string password, int messageId);
        Task<string> ComposeMessage(string username, string password, string recipients, string senderId, string smsMessage, string smsSendOption);
    }
}
