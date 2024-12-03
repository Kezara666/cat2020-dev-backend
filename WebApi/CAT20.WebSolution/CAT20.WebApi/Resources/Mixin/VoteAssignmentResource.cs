using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Vote;

namespace CAT20.WebApi.Resources.Mixin
{
    public class VoteAssignmentResource
    {
        public VoteAssignmentResource()
        {
            VoteAssignmentDetails = new HashSet<VoteAssignmentDetailsResource>();
        }
        public int Id { get; set; }
        public sbyte IsActive { get; set; }
        public virtual VoteDetailResource? voteDetail { get; set; }
        public int VoteId { get; set; }
        public virtual OfficeResource? office { get; set; }
        public int OfficeId { get; set; }
        public virtual AccountDetailResource? accountDetail { get; set; }
        public int BankAccountId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int SabhaId { get; set; }

        public virtual ICollection<VoteAssignmentDetailsResource> VoteAssignmentDetails { get; set; }
    }
}
