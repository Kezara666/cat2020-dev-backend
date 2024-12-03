using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM
{
    public  class HRMSequenceNumber
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int SabhaId { get; set; }
        public int ModuleType { get; set; }
        public string Prefix { get; set; }
        public int LastIndex { get; set; }
    }

    /*module types
     Advance B = 2
     
     */
}
