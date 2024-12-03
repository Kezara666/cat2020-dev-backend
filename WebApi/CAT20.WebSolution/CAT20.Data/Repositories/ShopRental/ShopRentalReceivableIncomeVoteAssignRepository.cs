using CAT20.Common.Envelop;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopRentalReceivableIncomeVoteAssignRepository : Repository<ShopRentalRecievableIncomeVoteAssign>, IShopRentalReceivableIncomeVoteAssignRepository
    {
        public ShopRentalReceivableIncomeVoteAssignRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForOffice(int oficeId)
        {
            return
                await shopRentalDbContext.ShopRentalRecievableIncomeVoteAssign
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Include(x => x.Shop)
                .Where(m => m.OfficeId == oficeId && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForSabha(int sabhaid)
        {
            return
                await shopRentalDbContext.ShopRentalRecievableIncomeVoteAssign
                .Include(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Include(x => x.Shop)
                .Where(m => m.SabhaId == sabhaid && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<ShopRentalRecievableIncomeVoteAssign> GetByShopId(int shopId)
        {
            return
            await shopRentalDbContext.ShopRentalRecievableIncomeVoteAssign
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
