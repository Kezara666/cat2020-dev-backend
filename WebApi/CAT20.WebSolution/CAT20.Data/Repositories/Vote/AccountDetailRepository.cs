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
    public class AccountDetailRepository : Repository<AccountDetail>, IAccountDetailRepository
    {
        public AccountDetailRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailAsync()
        {
            return await voteAccDbContext.AccountDetails
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<AccountDetail> GetWithAccountDetailByIdAsync(int id)
        {
            return await voteAccDbContext.AccountDetails
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByAccountDetailIdAsync(int Id)
        {
            return await voteAccDbContext.AccountDetails
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdAsync(int Id)
        {
            return await voteAccDbContext.AccountDetails.Where(m => m.BankID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<AccountDetail> GetAllWithAccountDetailByBankIdAsync1(int Id)
        {
            return await voteAccDbContext.AccountDetails.Where(m => m.BankID == Id && m.Status == 1)
                .FirstAsync();
        }
        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByOfficeIdAsync(int Id)
        {
            return await voteAccDbContext.AccountDetails
                .Include(m => m.accountBalDetail.Where(m => m.ID != null))
                .Where(m => m.OfficeID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdandOfficeIdAsync(int BankId, int OfficeId)
        {
            return await voteAccDbContext.AccountDetails.Where(m => m.BankID == OfficeId && m.OfficeID == OfficeId && m.Status==1)
                .ToListAsync();
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}