using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.TradeTax
{
    public partial class TradeTaxVoteAssignment
    {
        public int ID { get; set; }
        public int ActiveStatus { get; set; }
        public int SabhaID { get; set; }
        public int TaxTypeID { get; set; }
        public int VoteAssignmentDetailID { get; set; }
        //public int BankAccountID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}