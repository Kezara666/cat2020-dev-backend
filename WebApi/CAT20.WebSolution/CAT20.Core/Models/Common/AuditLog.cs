using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Common
{
   public class AuditLog
    {
        public int ID { get; set; }
        public DateTime dateTime { get; set; }
        public int User { get; set; }
        public string Note { get; set; }
        public string TransactionName{ get; set; }
        public int TransactionID{ get; set; }


    }
}
