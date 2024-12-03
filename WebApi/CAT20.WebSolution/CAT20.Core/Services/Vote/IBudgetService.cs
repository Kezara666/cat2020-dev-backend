using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IBudgetService
    {
        Task<(bool, string?)> SaveBudget(Budget newBudget, HTokenClaim token);
        Task<(bool, string?)> DeleteBudget(Budget budget);
        Task<Budget> GetBudgetById(int id);
        Task<IEnumerable<Budget>> GetAllBudgetsByVoteDetailId(int VoteDetailId);
        Task<IEnumerable<Budget>> GetAllBudgetsBySabhaID(int SabhaID);

        Task<(bool, string?)> SaveBudgetList(List<Budget> newBudgetList, HTokenClaim token);
    }
}
