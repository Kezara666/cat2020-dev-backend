using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Control
{
    public partial class SMSOutBox
    {
        public int ID { get; set; }
        public string? Module { get; set; }
        public string? Subject { get; set; }

        public string? SenderId { get; set; }
        public string Recipient { get; set; }
        public string SMSContent { get; set; }
        public SMSStatus SMSStatus { get; set; }
        public string Note { get; set; }
        public SMSSendAttempts SMSSendAttempts { get; set; }

        public int SabhaID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
