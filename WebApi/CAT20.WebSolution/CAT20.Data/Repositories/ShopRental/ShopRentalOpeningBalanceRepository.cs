using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopRentalOpeningBalanceRepository : Repository<OpeningBalance>, IShopRentalOpeningBalanceRepository

    {
        public ShopRentalOpeningBalanceRepository(DbContext context) : base(context)
        {
        }


        public async Task<OpeningBalance> GetOpeningBalanceByShopId(int shopId)
        {
            return await shopRentalDbContext.OpeningBalances.
                Include(s => s.Shop)
               .Include(p => p.Property)
               .ThenInclude(f => f.Floor)
               .ThenInclude(r => r.RentalPlace)
               .Where(m => m.ShopId == shopId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OpeningBalance>> GetOpeningBalancesByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.OpeningBalances.Where(obl => shopIds.Contains(obl.ShopId)).ToListAsync();

        }

        public async Task<IEnumerable<OpeningBalance>> GetAprovalPendingOpeningBalancesByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.OpeningBalances
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(obl => shopIds.Contains(obl.ShopId) && obl.ApproveStatus == 0).OrderByDescending(obl => obl.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<OpeningBalance>> GetAprovalRejectedOpeningBalancesByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.OpeningBalances
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(obl => shopIds.Contains(obl.ShopId) && obl.ApproveStatus == 2).OrderByDescending(obl => obl.ApprovedAt).ToListAsync();
        }

        public async Task<IEnumerable<OpeningBalance>> GetAprovalApprovedOpeningBalancesByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.OpeningBalances
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(obl => shopIds.Contains(obl.ShopId) && obl.ApproveStatus == 1).OrderByDescending(obl => obl.ApprovedAt).ToListAsync();
        }

        public async Task<IEnumerable<OpeningBalance>> GetAllByOpeningBalanceIds(List<int?> openingBalanceIds)
        {
            return await shopRentalDbContext.OpeningBalances.Where(obl => openingBalanceIds.Contains(obl.Id)).ToListAsync();
        }

        public async Task<IEnumerable<OpeningBalance>> GetAllByPropertyIdShopId(int propertyId, int shopId)
        {
            return
            await shopRentalDbContext.OpeningBalances
            .Where(m => m.PropertyId == propertyId && m.ShopId == shopId)
            .ToListAsync();
        }

        //---------------------------------------------------------------------------------


        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
