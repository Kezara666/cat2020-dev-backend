using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class VoucherLineResources
    {
        public int? Id { get; set; }
        public int VoucherId { get; set; }

        public string? CommentOrDescription { get; set; }
        public int? CommitmentLineId { get; set; }
        public int? DepositLineId { get; set; }
        public int? SubImprestLineId { get; set; }
        public int VoteId { get; set; }
        public int VoteBalanceId { get; set; }

        public string VoteCode { get; set; }

        public decimal NetAmount { get; set; }
        public decimal VAT { get; set; }
        public decimal NBT { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<VoucherSubLine>? VoucherSubLines { get; set; }

        //public Enums.PaymentStatus PaymentStatus { get; set; }

        //public virtual Voucher? Voucher { get; set; }



        //// report filed 

        //[Precision(18, 2)]
        //public decimal? RptBudgetAllocation { get; set; }


        //[Precision(18, 2)]
        //public decimal? RptExpenditure { get; set; }

        //[Precision(18, 2)]
        //public decimal? RptBalance { get; set; }
    }
}
