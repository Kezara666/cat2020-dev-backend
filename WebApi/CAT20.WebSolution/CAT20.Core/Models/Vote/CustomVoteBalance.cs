﻿using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Models.Vote
{
    public class CustomVoteBalance
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? CustomVoteDetailId { get; set; }
        public int? CustomVoteDetailIdParentId { get; set; }
        public int? ClassificationId { get; set; }
        public int Depth { get; set; }

        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }
        public int? SabhaId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }

        [NotMapped]
        public string? Code { get; set; }
        [NotMapped]
        public string? SubCode { get; set; }

        [NotMapped]
        public string? Description { get; set; }

        public CustomVoteBalanceStatus Status { get; set; }
        public VoteBalanceTransactionTypes TransactionType { get; set; }
        public bool? IsCarryForward { get; set; }

        /***************/

        [Precision(20, 2)]
        public decimal? EstimatedIncome { get; set; }

        [Precision(20, 2)]
        public decimal? AllocationAmount { get; set; }
        [Precision(20, 2)]
        [NotMapped]
        public decimal? AllocationExchangeAmount { get; set; }


        [Precision(20, 2)]
        public decimal TakeHoldRate { get; set; }

        [Precision(20, 2)]
        public decimal TakeHoldAmount { get; set; }

        [Precision(20, 2)]
        public decimal Debit { get; set; }

        [Precision(20, 2)]
        public decimal Credit { get; set; }


        [Precision(20, 2)]
        public decimal ChildrenDebit { get; set; }

        [Precision(20, 2)]
        public decimal ChildrenCredit { get; set; }

        [Precision(20, 2)]
        public decimal TotalCommitted { get; set; }
        [Precision(20, 2)]
        public decimal TotalHold { get; set; }
        [Precision(20, 2)]
        public decimal TotalPending { get; set; }

        /********/
        [Precision(20, 2)]
        [NotMapped]
        public decimal ExchangedAmount { get; set; }

        [Precision(20, 2)]
        public decimal CreditDebitRunningBalance { get; set; }

        [Precision(20, 2)]
        public decimal RunningBalance { get; set; }



        [Precision(20, 2)]
        public decimal CarryForwardDebit { get; set; }

        [Precision(20, 2)]
        public decimal CarryForwardCredit { get; set; }

        [Precision(20, 2)]
        public decimal CreditDebitCarryForwardRunningBalance { get; set; }

        [Precision(20, 2)]
        public decimal SurchargeDebit { get; set; }

        [Precision(20, 2)]
        public decimal SurchargeCredit { get; set; }

        [Precision(20, 2)]
        public decimal CreditDebitSurchargeRunningBalance { get; set; }

        public VoteTransferFlag TransferFlag { get; set; }




        [JsonIgnore]
        public virtual VoteAssignmentDetails? VoteAssignmentDetails { get; set; }

        public virtual ICollection<CustomVoteBalanceLog>? CustomVoteBalanceLog { get; set; }
        public virtual ICollection<CustomVoteBalanceActionLog>? CustomVoteBalanceActionLog { get; set; }

        // mandatory fields
        //public int? RowStatus { get; set; }

        public int? SessionIdByOffice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? OfficeId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }





        [ConcurrencyCheck]
        [Timestamp]
        public byte[]? RowVersion { get; set; }

    }
}