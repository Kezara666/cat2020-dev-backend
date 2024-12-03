using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Mixin
{
    public partial class CustomVoteSubLevel2
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int CustomVoteSubLevel1Id { get; set; }
        //09.12.2024
        public string Code { get; set; }
        public int? ProgrammeID { get; set; }
        //End
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual CustomVoteSubLevel1? CustomVoteSubLevel1 { get; set; }
        //public virtual List<SecondSubLevelBalance>? CustomVoteSubLevel2Balance { get; set; }
    }
}