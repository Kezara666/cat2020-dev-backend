using CAT20.Core.Models.Control;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Mixin
{
    public class MixinOrderLineLogRepository : Repository<MixinOrderLineLog>, IMixinOrderLineLogRepository
    {
        public MixinOrderLineLogRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<MixinOrderLineLog>> GetAllForOrderId(int mixinOrderId)
        {
            return
                await mixinDbContext.MixinOrderLineLogs
                .Where(m => m.MixinOrderId == mixinOrderId).ToListAsync();
        }

        public async Task<MixinOrderLineLog> GetById(int id)
        {
            return await mixinDbContext.MixinOrderLineLogs
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        public async Task<IEnumerable<MixinOrderLineLog>> GetAllForOfficeDate(int officeid, DateTime orderdate)
        {
            return await mixinDbContext.MixinOrderLineLogs
                .Include(t => t.VoteAssignmentDetails)
                .Include(t => t.VoteAssignmentDetails.voteAssignment)
                .Where(m => m.VoteAssignmentDetails.voteAssignment.OfficeId == officeid &&
            (m.CreatedAt).ToShortDateString() == orderdate.ToShortDateString()
            ).ToListAsync();
        }

        public async Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal)
        {
            var result = await mixinDbContext.MixinOrderLineLogs
               .Where(m => m.MixinVoteAssignmentDetailId == voteid && m.VoteOrBal == voteorbal)
               .OrderByDescending(m => m.CreatedAt)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentDetailId(int Id)
        {
            return await mixinDbContext.MixinOrderLineLogs.Where(m => m.MixinVoteAssignmentDetailId == Id).ToListAsync();
        }
        public async Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentId(int Id)
        {
            return await mixinDbContext.MixinOrderLineLogs
                .Include(t => t.VoteAssignmentDetails)
                .Include(t => t.VoteAssignmentDetails.voteAssignment)
                .Where(m => m.VoteAssignmentDetails.voteAssignment.Id == Id).ToListAsync();
        }

        public async Task<bool> IsRelationShipExist(int voteAssignmentDetailId)
        {
            return await mixinDbContext.MixinOrderLineLogs.AnyAsync(m => m.MixinVoteAssignmentDetailId == voteAssignmentDetailId);
        }
    }
}