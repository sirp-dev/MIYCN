using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZSMS.DTO
{
    public class MessageDetails
    {
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public string Recipients { get; set; }
        public int RecipientsCount { get; set; }
        public string MessageContent { get; set; }
        public string Response { get; set; }
        public object SummaryReport { get; set; } // Assuming this is not important for now
        public int UnitsUsed { get; set; }
        public object Scheduleddate { get; set; } // Assuming this is not important for now
        public DateTime DeliveredDate { get; set; }
        public int Status { get; set; }
        public string Username { get; set; }
        public object Response_status { get; set; }
        public object Response_error_code { get; set; }
        public object Response_cost { get; set; }
        public object Response_data { get; set; }
        public object Response_msg { get; set; }
        public int Response_length { get; set; }
        public int Response_page { get; set; }
        public object Response_balance { get; set; }
        public object Response_BalanceResponse { get; set; }
    }
}
