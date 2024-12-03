using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Control
{
    public partial class EmailOutBox
    {
        public int ID { get; set; }
        public int? CreatedByID { get; set; }
        public string Recipient { get; set; }
        public string Bc { get; set; }
        public string Cc { get; set; }
        public bool IsBodyHtml { get; set; }
        public string MailContent { get; set; }
        public string Subject { get; set; }
        public EmailStatus EmailStatus { get; set; }
        public string Note { get; set; }
        public EmailSendAttempts EmailSendAttempts { get; set; }
        public string Attachment { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
