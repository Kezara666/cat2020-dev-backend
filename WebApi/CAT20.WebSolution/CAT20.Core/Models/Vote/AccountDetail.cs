using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.Vote
{
    public partial class AccountDetail
    {
        public AccountDetail()
        {
            accountBalDetail = new HashSet<AccountBalanceDetail>();
        }
        [Key]
        public int ID { get; set; }
        public string AccountNo { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? BankID { get; set; }
        public int? VoteId { get; set; }
        public int? Status { get; set; }
        public int? OfficeID { get; set; }
        public string? BankCode { get; set; }
        public string? BranchCode { get; set; }

        [Precision(20, 2)]
        public decimal RunningBalance { get; set; }
        [Precision(20, 2)]
        public decimal ExpenseHold { get; set; }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


        // public virtual BankCodekDetail? BankDetail { get; set; }

        public virtual ICollection<AccountBalanceDetail> accountBalDetail { get; set; }
        
      
    }
}