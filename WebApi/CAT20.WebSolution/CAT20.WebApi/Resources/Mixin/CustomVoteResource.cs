using CAT20.Core.Models;
using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Mixin
{
    public class CustomVoteResource
    {
        public int Id { get; set; }
        public string CustomVoteName { get; set; }
        public sbyte IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int VoteAssignmentId { get; set; }
        public virtual VoteAssignmentBasicResource? voteAssignment { get; set; }
        public virtual ICollection<CustomVoteSubLevel1>? CustomVoteSubLevel1s { get; set; }
    }
}
