using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Vote
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<IEnumerable<Budget>> GetAllBudgetForVoteDetailIdAsync(int VoteDetailId);
        Task<IEnumerable<Budget>> GetAllBudgetForSabhaIDAsync(int SabhaID);
        Task<Budget> GetBudgetByIdAsync(int id);
    }
}
