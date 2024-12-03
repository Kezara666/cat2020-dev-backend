using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class VoteBalanceResource
    {
        public int? Id { get; set; }
        public int? VoteDetailId { get; set; }
        public int? ClassificationId { get; set; }
        public int? SabhaId { get; set; }
        public int? Year { get; set; }
        public VoteBalanceStatus Status { get; set; }

        public decimal? EstimatedIncome { get; set; }

        public decimal? AllocationAmount { get; set; }

        public decimal? TakeHoldRate { get; set; }

        public decimal? TakeHoldAmount { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal TotalHold { get; set; }
        public decimal TotalCommitted { get; set; }
        public decimal TotalPending { get; set; }

        public VoteBalanceTransactionTypes TransactionType { get; set; }

        public VoteTransferFlag TransferFlag { get; set; }


        public decimal RunningBalance { get; set; }
        public decimal PreBalance { get; set; }

        public decimal ExchangedAmount { get; set; }


        public virtual VoteDetailResource? VoteDetail { get; set; }

        //public virtual ICollection<VoteBalanceLog>? VoteBalanceLogs { get; set; }
        //public virtual ICollection<VoteBalanceActionLog>? ActionLogs { get; set; }

        //// mandatory fields
        ////public int? RowStatus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }





        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }


        /*calculated props*/

        public decimal CreditDebitBalance { get; set; }=0;
        public decimal VoteBalance { get; set; } = 0; 




    }
}