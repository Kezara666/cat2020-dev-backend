using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.Core.Models.FinalAccount
{

    public class VoucherLine
    {
        [Key]
        public int? Id { get; set; }
        public int VoucherId { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        public int VoteBalanceId { get; set; }


        public string? CommentOrDescription { get; set; }
        public int? CommitmentLineId { get; set; }
        [NotMapped]
        public int? CommitmentLineVoteId { get; set; }

        [Precision(18, 2)]
        public decimal NetAmount { get; set; }
        [Precision(18, 2)]
        public decimal VAT { get; set; }
        [Precision(18, 2)]
        public decimal NBT { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        [Precision(18, 2)]
        public decimal SurchargeAmount { get; set; }

        public virtual ICollection<VoucherSubLine>? VoucherSubLines { get; set; }


        [JsonIgnore]
        public virtual Voucher? Voucher { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


        // report filed 

        [Precision(18, 2)]
        public decimal? RptBudgetAllocation { get; set; }


        [Precision(18, 2)]
        public decimal? RptExpenditure { get; set; }

        [Precision(18, 2)]
        public decimal? RptBalance { get; set; }




    }
}