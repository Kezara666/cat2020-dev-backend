using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Mixin
{
    public partial class CustomVoteSubLevel1
    {
        public CustomVoteSubLevel1()
        {
            CustomVoteSubLevel2s = new HashSet<CustomVoteSubLevel2>();
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public int CustomVoteId { get; set; }
        public int? Status { get; set; }
        //09.12.2024
        public string Code { get; set; }
        public int? ProgrammeID { get; set; }
        //End
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual VoteAssignmentDetails? CustomVote { get; set; }
        public virtual ICollection<CustomVoteSubLevel2>? CustomVoteSubLevel2s { get; set; }
        //public virtual List<FirstSubLevelBalance>? CustomVoteSubLevel1Balance { get; set; }
    }
}