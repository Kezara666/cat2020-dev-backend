using CAT20.Core.Models.ShopRental;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Mixin
{
    public partial class VoteAssignmentDetails
    {
        //public VoteAssignmentDetails()
        //{
        //    CustomVoteSubLevel1s = new HashSet<CustomVoteSubLevel1>();
        //}

        public int Id { get; set; }
        public string CustomVoteName { get; set; }
        public sbyte IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int VoteAssignmentId { get; set; }
        //09.12.2024
        public string? Code { get; set; }
        public int Depth { get; set; }

        
        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual VoteAssignment? voteAssignment { get; set; }
        //public virtual ICollection<CustomVoteSubLevel1>? CustomVoteSubLevel1s { get; set; }
    }
}
