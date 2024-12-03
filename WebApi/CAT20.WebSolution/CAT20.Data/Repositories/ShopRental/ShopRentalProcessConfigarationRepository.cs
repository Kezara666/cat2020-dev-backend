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
    public class ShopRentalProcessConfigarationRepository : Repository<ShopRentalProcessConfigaration>, IShopRentalProcessConfigarationRepository
    {
        public ShopRentalProcessConfigarationRepository(DbContext context) : base(context)
        {
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

        public async Task<ShopRentalProcessConfigaration> GetById(int id)
        {
            return await shopRentalDbContext.ShopRentalProcessConfigaration
                    .Where(s => s.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShopRentalProcessConfigaration>> GetAllForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.ShopRentalProcessConfigaration
                .Include(s => s.ShopRentalProcess)
                .Where(p => p.SabhaId == sabhaId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

       


        public async Task<ShopRentalProcessConfigaration> GetByProcessConfigId(int processConfigId)
        {
            try {
                //need to get processConfig details which have this processConfigAssignId
                return await shopRentalDbContext.ShopRentalProcessConfigaration
                        .Include(fr => fr.FineRateType)
                        .Include(fc => fc.FineCalType)
                        .Include(rp => rp.RentalPaymentDateType)
                        .Include(fc => fc.FineChargingMethod)
                        .Include(s => s.ShopRentalProcess)
                        .Where(p => p.Id == processConfigId)
                        .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
