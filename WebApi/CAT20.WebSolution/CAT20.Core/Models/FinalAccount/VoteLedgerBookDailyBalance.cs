using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoteLedgerBookDailyBalance
    {
        public int Id { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }

        public int VoteBalanceId { get; set; }
        public int VoteDetailId { get; set; }


        [Precision(20, 2)]
        public decimal Credit { get; set; }
        [Precision(20, 2)]
        public decimal Debit { get; set; }


        [Precision(20, 2)]
        public decimal TotalDailyCredit { get; set; }
        [Precision(20, 2)]
        public decimal TotalDailyDebit { get; set; }
        [Precision(20, 2)]
        public decimal RunningTotal { get; set; }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionDate { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
