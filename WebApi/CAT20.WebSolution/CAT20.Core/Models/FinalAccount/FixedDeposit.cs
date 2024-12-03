using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class FixedDeposit
    {
        [Key]
        public int? Id {  get; set; }
        [Required]
        public int? DepositTypeVote { get; set; }

        [Required]
        public int? CustomVoteId { get; set; }

        [Required]
        public int? BankBranchId { get; set; }
        [Required]
        public string? Reference { get; set; }

        [Precision(18, 2)]
        public decimal? InterestRate { get; set; }

        [Precision(18,2)]
        public decimal? FDAmount { get; set; }

        [Required]
        public DateTime DepositDate { get; set; }

        [Required]
        public int? Duration { get; set; }
        public DateTime RenewableDate { get; set; }

        [Required]
        public int? OfficeId { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }

    }
}
