using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    internal class SpecialLedgerAccountsRepository : Repository<SpecialLedgerAccounts>, ISpecialLedgerAccountsRepository
    {
        public SpecialLedgerAccountsRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<SpecialLedgerAccountTypes>> GetSpecialLedgerAccountTypes()
        {
            return await voteAccDbContext.SpecialLedgerAccountTypes
               .ToListAsync();

        }

        public async Task<IEnumerable< SpecialLedgerAccounts>> GetSpecialLedgerAccountsForSabaha(int sabahId)
        {
            return await voteAccDbContext.SpecialLedgerAccounts
                .Include(a => a.Type)
                .Where(a => a.SabhaId == sabahId && a.Status==1)
                .ToListAsync();
        }   

        public async Task<SpecialLedgerAccounts> GetAccumulatedFundLedgerAccount(int sabahId)
        {
            return await voteAccDbContext.SpecialLedgerAccounts
                .Where(a => a.SabhaId == sabahId && a.TypeId == 4)
                .FirstOrDefaultAsync();
        }

        public async Task<SpecialLedgerAccounts> GetStampLedgerAccount(int sabahId)
        {
            return await voteAccDbContext.SpecialLedgerAccounts
                .Where(a => a.SabhaId == sabahId && a.TypeId == 3)
                .FirstOrDefaultAsync();
        }

        public async Task<SpecialLedgerAccounts> GetOtherReceiptsLedgerAccount(int sabahId)
        {
              return await voteAccDbContext.SpecialLedgerAccounts
                    .Where(a => a.SabhaId == sabahId && a.TypeId == 5)
                    .FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
