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
    public class InternalJournalTransfers
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

        /***************/

        [Precision(20, 2)]
        public decimal Debit { get; set; }

        [Precision(20, 2)]
        public decimal Credit { get; set; }
        
        /********/
        public int? ModulePrimaryKey { get; set; }
        public AppCategory? AppCategory { get; set; }

        /***************/

        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }

        public DateTime? SystemActionAt { get; set; }


    }
}
