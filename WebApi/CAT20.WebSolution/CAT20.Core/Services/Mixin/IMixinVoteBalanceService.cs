using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinVoteBalanceService
    {
        Task<VoteBalance> CreateNewVoteBalance(int VoteDetailId, Session session, HTokenClaim token);
    }
}
