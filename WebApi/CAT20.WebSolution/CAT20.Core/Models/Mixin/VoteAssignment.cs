using CAT20.Core.Models.User;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Models.Mixin
{
    public partial class VoteAssignment
    {
        public VoteAssignment()
        {
            VoteAssignmentDetails = new HashSet<VoteAssignmentDetails>();
        }

        public int Id { get; set; }
        public sbyte IsActive { get; set; }
        public virtual VoteDetail? voteDetail { get; set; }
        public int VoteId { get; set; }
        public virtual Office? office { get; set; }
        public int OfficeId { get; set; }
        public virtual AccountDetail? accountDetail { get; set; }
        public int BankAccountId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int SabhaId { get; set; }

        public virtual ICollection<VoteAssignmentDetails> VoteAssignmentDetails { get; set; }
    }
}