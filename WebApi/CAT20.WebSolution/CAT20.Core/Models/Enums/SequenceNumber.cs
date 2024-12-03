using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public class SequenceNumber 
    {
        public int? ID { get; set; }
        public string Type { get; set; }
        public string Prefix { get; set; }
        public int LastNumber { get; set; }
    }
}
