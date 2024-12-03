using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoteBalancerRceivableIncome
    {
        public int? Id { get; set; }
        public int? VoteDetailId { get; set; }
        public int? VoteBalanceId { get; set; }
        public int? SabhaId { get; set; }
        public int? Year { get; set; }

        public int? Month { get; set; }
        public decimal? IncomeAmount { get; set; }

        public decimal CreditDebitRunningBalance { get; set; }

        public decimal ExchangedAmount { get; set; }


        // mandatory fields
        //public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
