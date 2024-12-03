using CAT20.Core.Models.Enums;
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
    public class ShopRentalProcessRepository : Repository<ShopRentalProcess>, IShopRentalProcessRepository
    {
        public ShopRentalProcessRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<ShopRentalProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo)
        {
            try
            {

                var query = shopRentalDbContext.ShopRentalProcess
                    .Where(a => a.ShabaId == sabhaId)
                    .OrderByDescending(a => a.Id); ;

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var shopList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: shopList);

            }
            catch (Exception ex)
            {
                return (totalCount: 0, list: null);
            }
        }

        public async Task<bool> IsCompetedProcess(int SbahaId, DateOnly date, ShopRentalProcessType processType)
        {
            return await shopRentalDbContext.ShopRentalProcess.AnyAsync(p => p.ShabaId == SbahaId &&  p.Date == date && p.ProcessType== processType);
        }

        public async Task<bool> IsCompetedMonthendProcess(int SbahaId, DateOnly date)
        {
            return await shopRentalDbContext.ShopRentalProcess.AnyAsync(p => p.ShabaId == SbahaId && p.Date == date && p.ProcessType == ShopRentalProcessType.MonthendProcess);
        }

        public async Task<bool> IsCompetedFineProcess(int SbahaId, DateOnly date)
        {
            return await shopRentalDbContext.ShopRentalProcess.AnyAsync(p => p.ShabaId == SbahaId && p.Date == date && p.ProcessType == ShopRentalProcessType.FineProcess);
        }

        public async Task<ShopRentalProcess> getLastDailyFineRateShopRentalprocess(int SbahaId)
        {
            return await shopRentalDbContext.ShopRentalProcess
                   .Include(s => s.ShopRentalProcessConfigaration)
                .Where(s => s.ShabaId == SbahaId && s.ProcessType == ShopRentalProcessType.FineProcess && s.ShopRentalProcessConfigaration.FineRateTypeId ==1)
             
                .OrderByDescending(s=> s.ProceedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<ShopRentalProcess> getLastDailyFineFixedAmountShopRentalprocess(int SbahaId)
        {
            return await shopRentalDbContext.ShopRentalProcess
                   .Include(s => s.ShopRentalProcessConfigaration)
                .Where(s => s.ShabaId == SbahaId && s.ProcessType == ShopRentalProcessType.FineProcess && s.ShopRentalProcessConfigaration.FineRateTypeId == 4)

                .OrderByDescending(s => s.ProceedDate)
                .FirstOrDefaultAsync();
        }


        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
