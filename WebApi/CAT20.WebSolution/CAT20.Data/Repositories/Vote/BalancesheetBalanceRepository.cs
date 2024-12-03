using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class BalancesheetBalanceRepository : Repository<BalancesheetBalance>, IBalancesheetBalanceRepository
    {
        public BalancesheetBalanceRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceAsync()
        {
            return await voteAccDbContext.BalancesheetBalances
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<BalancesheetBalance> GetWithBalancesheetBalanceByIdAsync(int id)
        {
            return await voteAccDbContext.BalancesheetBalances
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByBalancesheetBalanceIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetBalances
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetBalances.Where(m => m.VoteDetailID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaIdAsync(int VoteDetailId, int SabhaId)
        {
            return await voteAccDbContext.BalancesheetBalances.Where(m => m.VoteDetailID == VoteDetailId && m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForVoteDetailIdandYearAsync(int VoteDetailId, int year)
        {
            return await voteAccDbContext.BalancesheetBalances.Where(m => m.VoteDetailID == VoteDetailId && m.Year == year && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetBalances.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}