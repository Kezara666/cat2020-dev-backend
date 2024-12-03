using CAT20.Core.Models;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Mixin
{
    public class VoteAssignmentDetailsMixinCustomResource
    {
        public int Id { get; set; }
        public string customVoteName { get; set; }
        public int VoteAssignmentId { get; set; }
        public string? VoteCode { get; set; }
        public string? voteCodewithCustomName { get; set; }
        public sbyte IsActive { get; set; }
        public VoteDetail? VoteDetail { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Code { get; set; }
        public int Depth { get; set; }
        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }
        //public virtual VoteAssignmentResource? voteAssignment { get; set; }
        //public virtual ICollection<CustomVoteSubLevel1>? CustomVoteSubLevel1s { get; set; }
    }
}
