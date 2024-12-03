using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopRentalVoteAssignRepository: Repository<ShopRentalVoteAssign>, IShopRentalVoteAssignRepository
    {
        public ShopRentalVoteAssignRepository(DbContext context) : base(context)
        {
        }

        
        public async Task<IEnumerable<ShopRentalVoteAssign>> GetAllForOffice(int oficeId)
        {
            return
                await shopRentalDbContext.ShopRentalVoteAssign
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Include(x => x.Shop)
                .Where(m => m.OfficeId == oficeId && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShopRentalVoteAssign>> GetAllForSabha(int sabhaid)
        {
            return
                await shopRentalDbContext.ShopRentalVoteAssign
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Include(x => x.Shop)
                .Where(m => m.SabhaId == sabhaid && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        //public async Task<ShopRentalVoteAssign> GetByPropertyIdAsync(int propertyId)
        //{
        //    return
        //        await shopRentalDbContext.ShopRentalVoteAssign
        //        .Include(x => x.Property)
        //        .Where(m => m.PropertyId == propertyId && m.Status == 1)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<ShopRentalVoteAssign> GetByShopId(int shopId)
        {
            return
            await shopRentalDbContext.ShopRentalVoteAssign
                 .Include(p => p.Property)
                 .ThenInclude(f => f.Floor)
                 .ThenInclude(r => r.RentalPlace)
                 .Where(m => m.ShopId == shopId && m.Status == 1)
                 .FirstOrDefaultAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
