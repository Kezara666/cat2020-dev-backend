using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.LoanManagement
{
    public partial class AdvanceBTypeData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Interest { get; set; }
        public int? MaxInstalment { get; set; }
        public bool HasInterest { get; set; }
        public int AccountSystemVersionId { get; set; }

        // Mandatory fields
        public int StatusId { get; set; }

        // Navigation property
        public ICollection<AdvanceBTypeLedgerMapping>? AdvanceBTypeLedgerMapping { get; set; }
    }
}
