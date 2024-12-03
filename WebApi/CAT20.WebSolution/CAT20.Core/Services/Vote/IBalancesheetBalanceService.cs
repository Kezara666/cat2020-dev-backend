using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IBalancesheetBalanceService
    {
        Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalances();
        Task<BalancesheetBalance> GetBalancesheetBalanceById(int id);
        Task<BalancesheetBalance> CreateBalancesheetBalance(BalancesheetBalance newBalancesheetBalance);
        Task UpdateBalancesheetBalance(BalancesheetBalance balancesheetBalanceToBeUpdated, BalancesheetBalance balancesheetBalance);
        Task DeleteBalancesheetBalance(BalancesheetBalance balancesheetBalance);

        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailId(int Id);
        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaId(int VoteDetailId, int SabhaId);
        Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForVoteDetailIdandYear(int VoteDetailId, int year);
        Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForSabhaId(int SabhaId);
       
    }
}

