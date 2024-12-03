using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Mixin
{
    public class VoteAssignmentRepository : Repository<VoteAssignment>, IVoteAssignmentRepository
    {
        public VoteAssignmentRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<VoteAssignment>> GetAllForSabhaId(int sabhaid)
        {
            return
                await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.IsActive == 1 && m.SabhaId == sabhaid)
                .ToListAsync();
        }

        public async Task<VoteAssignment> GetById(int id)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.Id == id && m.IsActive == 1)
                .FirstOrDefaultAsync(); 
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        public async Task<IEnumerable<VoteAssignment>> GetAllForOfficeId(int officeId)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m=> m.VoteAssignmentDetails)
                .Where(m => m.OfficeId == officeId && m.IsActive == 1).ToListAsync();
        }

        public async Task<IEnumerable<VoteAssignment>> GetAllForOfficeIdAndAccountDetailId(int officeId, int accountdetailid)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.OfficeId == officeId && m.IsActive == 1 && m.BankAccountId== accountdetailid).ToListAsync();
        }

        public async Task<bool> HasVoteAssignmentsForAccountDetailId(int accountDetailId)
        {
            return await mixinDbContext.VoteAssignment
                .AnyAsync(m => m.BankAccountId == accountDetailId);
        }


        public async Task<IEnumerable<VoteAssignment>> GetAllForVoteId(int Id)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.VoteId == Id && m.IsActive == 1).ToListAsync();
        }

        public async Task<bool> HasVoteAssignmentsForVoteId(int voteDetailId)
        {
            return await mixinDbContext.VoteAssignment
                .AnyAsync(m => m.VoteId == voteDetailId && m.IsActive == 1);
        }

        public async Task<int> GetAssignedBankAccountForSubOffice(int OfficeId)
        {
            try
            {
                var bankaccount = await mixinDbContext.VoteAssignment
                    .Where(m => m.OfficeId == OfficeId && m.IsActive == 1).FirstAsync();

                return bankaccount.BankAccountId;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        public async Task<VoteAssignment> GetByVoteId(int voteId ,HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.VoteId == voteId && m.IsActive == 1 && m.SabhaId ==token.sabhaId )
                .FirstOrDefaultAsync();
        }
        public async Task<VoteAssignment> GetByVoteIdAndOffice(int voteId, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.VoteId == voteId && m.IsActive == 1 && m.SabhaId == token.sabhaId && m.OfficeId ==token.officeId)
                .FirstOrDefaultAsync();
        }


        public async Task<int> GetAccountIdByVoteId(int voteId, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.VoteId == voteId && m.IsActive == 1 && m.SabhaId == token.sabhaId)
                .Select(m => m.BankAccountId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetAccountIdByVoteIdByOffice(int voteId, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignment
                .Include(m => m.VoteAssignmentDetails)
                .Where(m => m.VoteId == voteId && m.IsActive == 1 && m.SabhaId == token.sabhaId && m.OfficeId == token.officeId)
                .Select(m => m.BankAccountId)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> HasAssigned(int voteId)
        {
            return await mixinDbContext.VoteAssignment
                .AnyAsync(m => m.VoteId == voteId  && m.IsActive == 1);
        }

        public async Task<IEnumerable<int>> GetAssignedVoteIds( int bankAccountId)
        {
            return await mixinDbContext.VoteAssignment
                .Where(m => m.BankAccountId == bankAccountId && m.IsActive == 1)
                .Select(m=>m.VoteId)
                .ToListAsync();
        }
    }
}