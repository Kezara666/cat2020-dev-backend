using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface ICustomVoteBalanceService
    {
        Task CreateNewCustomVoteBalance(int customVoteId, Session session, HTokenClaim token);
        Task UpdateCustomVoteBalance(int customVoteId,CashBookTransactionType cbTransaction, VoteBalanceTransactionTypes voteTransaction, Decimal amount, Session session, HTokenClaim token);
        Task UpdateCustomVoteBalanceForOpenBalances(int customVoteId, FAMainTransactionMethod transactionMethod, VoteBalanceTransactionTypes voteTransaction, Decimal amount, Session session, HTokenClaim token);

       
    }
}
