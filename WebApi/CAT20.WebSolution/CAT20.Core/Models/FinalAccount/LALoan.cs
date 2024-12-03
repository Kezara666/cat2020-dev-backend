using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class LALoan
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? LoanTypeVote { get; set; }

        [Required]
        public int? CustomVoteId { get; set; }

        public int? BankBranchId { get; set; }

        [Precision(18,2)]
        public decimal LoanAmount { get; set; }
        [Precision(18, 2)]
        public decimal InterestRate { get; set; }
        [Precision(18, 2)]
        public decimal Installment { get; set; }

        [Required]
        public DateTime BorrowingDate { get; set; }
        [Required]
        public int? Duration { get; set; }

        [Precision(18, 2)]
        public decimal Balance  { get; set; }

        [Required]
        public string? LoanPurpose  { get; set; }
        public string? Mortgage { get; set; }


        /*mandatory filed*/

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
