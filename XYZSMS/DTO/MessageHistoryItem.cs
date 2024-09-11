using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZSMS.DTO
{
    public class MessageHistoryItem
    {
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public DateTime DeliveredDate { get; set; }
        public int Status { get; set; }
        public object Recipients { get; set; } // Assuming this is not important for now
        public int RecipientsCount { get; set; }
        public int UnitsUsed { get; set; }
        public object Scheduleddate { get; set; } // Assuming this is not important for now
    }
}
