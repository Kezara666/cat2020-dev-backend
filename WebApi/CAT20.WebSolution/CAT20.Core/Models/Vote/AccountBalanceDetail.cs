using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class AccountBalanceDetail
    {
        [Key]
        public int ID { get; set; }
        public int AccountDetailID { get; set; }
        public int? Year { get; set; }
        public decimal BalanceAmount { get; set; }

        public DateTime? EnteredDate { get; set; }
        public int Status { get; set; }
        public int SabhaID { get; set; }

        public virtual AccountDetail accountDetail { get; set; }
        
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
