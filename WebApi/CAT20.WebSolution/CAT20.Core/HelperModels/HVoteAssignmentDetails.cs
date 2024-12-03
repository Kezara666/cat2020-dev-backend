using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HVoteAssignmentDetails
    {
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

        public HVoteAssignment? voteAssignment { get; set; }
        public virtual ICollection<HVoteAssignmentDetails>? Children { get; set; } = new List<HVoteAssignmentDetails>();

    }
}
