using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Models.Control
{
    public partial class BankBranch
    {
        public int ID { get; set; }
        public int BankCode { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string? BranchAddress { get; set; }
        public string? TelNo1 { get; set; }
        public string? TelNo2 { get; set; }
        public string? TelNo3 { get; set; }
        public string? TelNo4 { get; set; }
        public string? FaxNo { get; set; }
        public string? District { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BankDetail Bank { get; set; }

        public virtual ICollection<Agents>? Agents { get; set; }    
    }
}