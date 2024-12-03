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
    public class MixinOrderLineRepository : Repository<MixinOrderLine>, IMixinOrderLineRepository
    {
        public MixinOrderLineRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<MixinOrderLine>> GetAllForOrderId(int mixinOrderId)
        {
            return
                await mixinDbContext.MixinOrderLines
                .Where(m => m.MixinOrderId == mixinOrderId).ToListAsync();
        }

        public async Task<MixinOrderLine> GetById(int id)
        {
            return await mixinDbContext.MixinOrderLines
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForOfficeDate(int officeid, DateTime orderdate)
        {
            return await mixinDbContext.MixinOrderLines
                .Include(t => t.VoteAssignmentDetails)
                .Include(t => t.VoteAssignmentDetails.voteAssignment)
                .Where(m => m.VoteAssignmentDetails.voteAssignment.OfficeId == officeid &&
            (m.CreatedAt).ToShortDateString() == orderdate.ToShortDateString()
            ).ToListAsync();
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal)
        {
            var result = await mixinDbContext.MixinOrderLines
               .Where(m => m.MixinVoteAssignmentDetailId == voteid && m.VoteOrBal == voteorbal)
               .OrderByDescending(m => m.CreatedAt)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentDetailId(int Id)
        {
            return await mixinDbContext.MixinOrderLines.Where(m => m.MixinVoteAssignmentDetailId == Id).ToListAsync();
        }
        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentId(int Id)
        {
            return await mixinDbContext.MixinOrderLines
                .Include(t => t.VoteAssignmentDetails)
                .Include(t => t.VoteAssignmentDetails.voteAssignment)
                .Where(m => m.VoteAssignmentDetails.voteAssignment.Id == Id).ToListAsync();
        }

        public async Task<bool> IsRelationShipExist(int voteAssignmentDetailId)
        {
            return await mixinDbContext.MixinOrderLines.AnyAsync(m => m.MixinVoteAssignmentDetailId == voteAssignmentDetailId);
        }
    }
}