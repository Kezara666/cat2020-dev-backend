using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Repositories.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Vote
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(DbContext context) : base(context) { }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
        public async Task<IEnumerable<Budget>> GetAllBudgetForVoteDetailIdAsync(int voteDetailId)
        {
            return await voteAccDbContext.Budget.Where(b => b.VoteDetailId == voteDetailId && b.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetForSabhaIDAsync(int sabhaID)
        {
            return await voteAccDbContext.Budget.Where(b => b.SabhaID == sabhaID && b.Status == 1).ToListAsync();
        }

        public async Task<Budget> GetBudgetByIdAsync(int id)
        {
            return voteAccDbContext.Budget.Where(b => b.Id == id && b.Status == 1).FirstOrDefault();
        }
    }
}
