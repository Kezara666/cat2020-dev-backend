using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class FinalAccountSequenceNumber
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int SabhaId { get; set; }
        public FinalAccountModuleType ModuleType { get; set; }
        public string Prefix { get; set; }
        public int LastIndex { get; set; }
    }
}
