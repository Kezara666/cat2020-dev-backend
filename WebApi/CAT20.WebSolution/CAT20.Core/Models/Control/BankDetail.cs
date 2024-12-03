using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Models.Control
{
    public partial class BankDetail
    {
        public BankDetail()
        {
            bankBranch = new HashSet<BankBranch>();
        }
        public int ID { get; set; }
        public string Description { get; set; }
        public int BankCode { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BankBranch>? bankBranch { get; set; }
    }
}