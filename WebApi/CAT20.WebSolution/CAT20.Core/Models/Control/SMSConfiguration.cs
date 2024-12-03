using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Control
{
    public partial class SMSConfiguration
    {
        public int ID { get; set; }
        public string Provider { get; set; }
        public string SenderId { get; set; }
        public string ApiEndPoint { get; set; }
        public string? Username { get; set; }
        public string ApiToken { get; set; }
        public int SabhaId { get; set; }
        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
