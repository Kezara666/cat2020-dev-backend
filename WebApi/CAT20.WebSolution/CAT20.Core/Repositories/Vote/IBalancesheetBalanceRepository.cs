using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IBalancesheetBalanceRepository : IRepository<BalancesheetBalance>
    {
        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceAsync();
        Task<BalancesheetBalance> GetWithBalancesheetBalanceByIdAsync(int id);
        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByBalancesheetBalanceIdAsync(int Id);
        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdAsync(int Id);
        Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaIdAsync(int VoteDetailId, int SabhaId);
        Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForVoteDetailIdandYearAsync(int VoteDetailId, int year);
        Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForSabhaIdAsync(int SabhaId);
    }
}
