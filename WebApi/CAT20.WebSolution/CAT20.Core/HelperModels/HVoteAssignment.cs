using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HVoteAssignment
    {
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
    }
}
