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
    public class AccountBalanceDetailRepository : Repository<AccountBalanceDetail>, IAccountBalanceDetailRepository
    {
        public AccountBalanceDetailRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailAsync()
        {
            return await voteAccDbContext.AccountBalanceDetails
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<AccountBalanceDetail> GetWithAccountBalanceDetailByIdAsync(int id)
        {
            return await voteAccDbContext.AccountBalanceDetails
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountBalanceDetailIdAsync(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdAsync(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailBySabhaIdAsync(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.SabhaID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountIdAsync(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.AccountDetailID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<AccountBalanceDetail> GetAllWithAccountBalanceDetailByAccountIdAsync1(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.AccountDetailID == Id && m.Status == 1)
                .FirstAsync();
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdSabhaIdAsync(int AccountDetailId, int SabhaId)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.AccountDetailID == AccountDetailId && m.SabhaID == SabhaId && m.Status==1)
                .ToListAsync();
        }


        public async Task<IEnumerable<AccountBalanceDetail>> GetAllAccountBalanceDetailsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.AccountBalanceDetails.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}