using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;

namespace CAT20.WebApi.Resources.Vote.Save
{ 
    public partial class SaveVoteAllocationResource
    {
        //public int? ID { get; set; }
        //public int? VoteDetailID { get; set; }
        //public double? AllocationAmount { get; set; }
        //public double? IncomeAmount { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public int? Year { get; set; }
        //public int? Status { get; set; }
        //public int? SabhaID { get; set; }
        //public decimal TotalPaid { get; set; }
        //public decimal TotalHold { get; set; }
        //[JsonIgnore]
        //public virtual VoteDetail? VoteDetail { get; set; }


        public int? Id { get; set; }
        public int? VoteDetailId { get; set; }
        public int? SabhaId { get; set; }
        public int? Year { get; set; }
        public VoteBalanceStatus Status { get; set; }

        public decimal? EstimatedIncome { get; set; }
        public decimal? IncomeAmount { get; set; }

        public decimal DepositedAmount { get; set; }
        public decimal ReleasedAmount { get; set; }
        public decimal DepositBalanceAmount { get; set; }

        public decimal? AllocationAmount { get; set; }
        public decimal TotalPaid { get; set; }

        public decimal TotalHold { get; set; }
        public decimal TotalCommitted { get; set; }
        public decimal TotalPending { get; set; }

        public VoteBalanceTransactionTypes TransactionType { get; set; }
        public decimal ExchangedAmount { get; set; }

        //public virtual VoteDetail VoteDetail { get; set; }

        //public virtual ICollection<VoteBalanceLog>? VoteBalanceLogs { get; set; }
        //public virtual ICollection<VoteBalanceActionLog>? ActionLogs { get; set; }

        //// mandatory fields
        ////public int? RowStatus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }


        public bool IsAllocationOrEstimateIncomeGreaterThanZero
        {
            get { return (AllocationAmount > 0 || EstimatedIncome > 0); }
        }
    }
}