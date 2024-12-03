using CAT20.Core.DTO.Vote.Save;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveLALoanResource
    {
        public int? Id { get; set; }
        public int? LoanTypeVote { get; set; }
        public int? BankBranchId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Installment { get; set; }

        public DateTime BorrowingDate { get; set; }
        public int? Duration { get; set; }

        public decimal Balance { get; set; }

        public string? LoanPurpose { get; set; }
        public string? Mortgage { get; set; }


        ///*mandatory filed*/

        //[Required]
        //public int? OfficeId { get; set; }
        //[Required]
        //public int? SabhaId { get; set; }
        //public int Status { get; set; }
        //[Required]
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //[Required]
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }

        public int? CustomVoteId { get; set; }
    }
}
