using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.User
{
    public partial class AuditLogUser 
    {
        public int? ID { get; set; }
        public Transaction Transaction { get; set; }
        public int SourceID { get; set; }
        public UserDetail User { get; set; }
        public int UserID { get; set; }
        public DateTime RecordDateTime { get; set; }
        public string Notes { get; set; }
    }
}
