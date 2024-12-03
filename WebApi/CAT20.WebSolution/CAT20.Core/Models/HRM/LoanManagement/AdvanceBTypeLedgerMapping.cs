using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.LoanManagement
{
    public partial class AdvanceBTypeLedgerMapping
    {
        public int Id { get; set; }
        public int  AdvanceBTypeId      { get; set; }
        public string LedgerCode { get; set; }
        public int LedgerId { get; set; }
        public string Prefix { get; set; }
        public int LastIndex { get; set; }
        public int SabhaId { get; set; }

        // Mandatory fields
        public int StatusId { get; set; }

        // Navigation properties
        public AdvanceBTypeData? AdvanceBTypeData { get; set; }



    }
}
