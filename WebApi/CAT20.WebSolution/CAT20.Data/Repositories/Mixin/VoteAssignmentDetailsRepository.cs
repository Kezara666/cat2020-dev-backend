using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Mixin
{
    public class VoteAssignmentDetailsRepository : Repository<VoteAssignmentDetails>, IVoteAssignmentDetailsRepository
    {
        public VoteAssignmentDetailsRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<VoteAssignmentDetails>> GetAll()
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m=> m.IsActive == 1 && m.voteAssignment.IsActive == 1)
                .ToListAsync();
        }

        public async Task<VoteAssignmentDetails> GetById(int id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                .Include(m=> m.voteAssignment)
                .Where(m => m.Id == id && m.voteAssignment.IsActive == 1 && m.IsActive == 1)
                .FirstOrDefaultAsync(); 
        }

        public async Task<VoteAssignmentDetails> GetVoteAssignmentDetails(int id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                .Where(m => m.Id == id && m.voteAssignment.IsActive == 1 && m.IsActive == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<VoteAssignmentDetails> GetByAssignmentId(int assignmentId)
        {
            return await mixinDbContext.VoteAssignmentDetails
                //.Include(m => m.voteAssignment)
                .Where(m => m.VoteAssignmentId == assignmentId  && m.IsActive == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllVoteAssignmentDetailsForVoteAssignmentId(int id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.VoteAssignmentId == id && m.voteAssignment.IsActive==1 && m.IsActive == 1).ToListAsync();
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        private VoteAccDbContext voteaccDbContext
        {
            get { return voteaccDbContext as VoteAccDbContext; }
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllForOfficeId(int Id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.voteAssignment.OfficeId == Id && m.voteAssignment.IsActive == 1 && m.IsActive == 1).ToListAsync();
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllForSabhaId(int Id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.voteAssignment.OfficeId == Id && m.voteAssignment.IsActive == 1 && m.IsActive == 1).ToListAsync();
        }

        public async Task<VoteAssignmentDetails> GetForCrossOrder(int Id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.Id == Id && m.IsActive == 1 && m.voteAssignment.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<VoteAssignmentDetails> GetWithVoteAssignmentById(int Id)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.Id == Id && m.IsActive == 1 && m.voteAssignment.IsActive == 1).FirstOrDefaultAsync();
        }


        public async  Task<IEnumerable<VoteAssignmentDetails>> GetCustomVoteWithZeroLevelsForVoteAssignmentId(int assignmentId)
        {
            return await mixinDbContext.VoteAssignmentDetails
                  .Include(m => m.voteAssignment)
                    .Where(m => m.VoteAssignmentId == assignmentId && m.IsActive == 1 && m.IsSubLevel ==false )
                    .ToListAsync();
        }


        public async Task<VoteAssignmentDetails> GetCustomVoteWithSubLevels(int Id)
        {
                     return await mixinDbContext.VoteAssignmentDetails
                    .Include(m => m.voteAssignment)
                      .Where(m => m.Id == Id && m.IsActive == 1)
                      .FirstOrDefaultAsync();
        }

        public ICollection<VoteAssignmentDetails> GetChildren(int? parentId)
        {
            // Fetch categories with the given ParentId
           return mixinDbContext.VoteAssignmentDetails
                .Where(c => c.ParentId == parentId && c.IsActive==1 && c.IsSubLevel==true)
                .ToList();

           
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithZeroLevelsForVoteId(int voteId, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignmentDetails
                .Include(m => m.voteAssignment)
               .Where(m => m.voteAssignment!.VoteId ==voteId && m.voteAssignment.IsActive == 1 && m.voteAssignment.OfficeId == token.officeId && m.IsActive == 1 && m.IsSubLevel==false && m.ParentId==null).ToListAsync();
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithSubLevelsForOfficeAndBankAccountId(int bankaccountid, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignmentDetails
                 .Include(m => m.voteAssignment)
                .Where(m => m.voteAssignment.BankAccountId == bankaccountid && m.voteAssignment.IsActive == 1 && m.IsActive == 1 && m.voteAssignment.OfficeId==token.officeId).ToListAsync();
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithZeroLevelsForAccountId(int accountid, HTokenClaim token)
        {
            return await mixinDbContext.VoteAssignmentDetails
                .Include(m => m.voteAssignment)
               .Where(m => m.voteAssignment!.BankAccountId == accountid && m.voteAssignment.IsActive == 1 && m.voteAssignment.OfficeId == token.officeId && m.IsActive == 1 && m.IsSubLevel == false && m.ParentId == null).ToListAsync();
        }
    }
}