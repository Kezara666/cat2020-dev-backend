using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount.logs
{
    public class VoucherLineLog
    {
        public int? Id { get; set; }
        public int VoucherLogId { get; set; }
        public int? VoucherLineId { get; set; }
        public int VoteBalanceId { get; set; }
        public string? CommentOrDescription { get; set; }
        public int CommitmentLineId { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        [Precision(18, 2)]
        public decimal NetAmount { get; set; }
        [Precision(18, 2)]
        public decimal VAT { get; set; }
        [Precision(18, 2)]
        public decimal NBT { get; set; }

        [Precision(18, 2)]
        public decimal SurchargeAmount { get; set; }

        [JsonIgnore]
        public virtual VoucherLog? VoucherLog { get; set; }

        // report filed 

        [Precision(18, 2)]
        public decimal? RptBudgetAllocation { get; set; }


        [Precision(18, 2)]
        public decimal? RptExpenditure { get; set; }

        [Precision(18, 2)]
        public decimal? RptBalance { get; set; }
    }
}
