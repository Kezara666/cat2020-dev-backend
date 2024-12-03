using CAT20.Core.Models.Control;
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
    public class MixinCancelOrdersRepository : Repository<MixinCancelOrder>, IMixinCancelOrderRepository
    {
        public MixinCancelOrdersRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<MixinCancelOrder>> GetAll()
        {
            return
                await mixinDbContext.MixinCancelOrders
                .ToListAsync();
        }

        public async Task<MixinCancelOrder> GetById(int id)
        {
            return await mixinDbContext.MixinCancelOrders
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<MixinCancelOrder> GetByOrderId(int id)
        {
            var result = await mixinDbContext.MixinCancelOrders
                  .Where(m => m.MixinOrderId == id)
                  .FirstOrDefaultAsync();
            return result;
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        //public async Task<IEnumerable<MixinOrder>> GetAllForOfficeId(int officeId)
        //{
        //    return await mixinDbContext.MixinOrders.Where(m => m.OfficeId == officeId && m.IsActive == 1).ToListAsync();
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForVoteId(int Id)
        //{
        //    return await mixinDbContext.MixinOrders.Where(m => m.MixinVoteAssignmentId == Id).ToListAsync();
        //}
    }
}