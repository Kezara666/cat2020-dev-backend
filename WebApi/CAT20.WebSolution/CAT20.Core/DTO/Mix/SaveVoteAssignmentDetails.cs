using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Mix
{
    public class SaveVoteAssignmentDetails
    {
        public int? Id { get; set; }
        public string CustomVoteName { get; set; }
        public sbyte IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int VoteAssignmentId { get; set; }
        //09.12.2024
        public string Code { get; set; }
        public int Depth { get; set; }


        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
