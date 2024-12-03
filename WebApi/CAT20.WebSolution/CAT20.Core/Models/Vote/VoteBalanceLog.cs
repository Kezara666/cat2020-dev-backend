﻿using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class VoteBalanceLog
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? VoteBalanceId { get; set; }

        [Required]
        public int? VoteDetailId { get; set; }
        public int? SabhaId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }


        public string? Code { get; set; }
        public string? SubCode { get; set; }

        public string? Description { get; set; }    

        public VoteBalanceStatus Status { get; set; }
        public VoteBalanceTransactionTypes TransactionType { get; set; }

        public bool? IsCarryForward { get; set; }
        /***************/


        [Precision(20, 2)]
        public decimal? EstimatedIncome { get; set; }

        [Precision(20, 2)]
        public decimal? AllocationAmount { get; set; }


        [Precision(20, 2)]
        public decimal TakeHoldRate { get; set; }

        [Precision(20, 2)]
        public decimal TakeHoldAmount { get; set; }


        [Precision(20, 2)]
        public decimal? AllocationExchangeAmount { get; set; }

        [Precision(20, 2)]
        public decimal Debit { get; set; }

        [Precision(20, 2)]
        public decimal Credit { get; set; }

        [Precision(20, 2)]
        public decimal TotalCommitted { get; set; }
        [Precision(20, 2)]
        public decimal TotalHold { get; set; }
        [Precision(20, 2)]
        public decimal TotalPending { get; set; }

        /********/

        [Precision(20, 2)]
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

        
        /***************/

        public int? ModulePrimaryKey { get; set; }
        public AppCategory? AppCategory { get; set; }

        /***************/


        [JsonIgnore]
        public virtual VoteBalance? VoteBalance { get; set; }

        public int? OfficeId { get; set; }
        public int? SessionIdByOffice { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }

        [Precision(20, 2)]
        public decimal CustomVoteDebit { get; set; }

        [Precision(20, 2)]
        public decimal CustomVoteCredit { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}