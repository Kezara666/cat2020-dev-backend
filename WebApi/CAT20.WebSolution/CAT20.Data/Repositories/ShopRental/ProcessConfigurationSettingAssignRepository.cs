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
    public class ProcessConfigurationSettingAssignRepository : Repository<ProcessConfigurationSettingAssign>, IProcessConfigurationSettingAssignRepository
    {
        public ProcessConfigurationSettingAssignRepository(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProcessConfigurationSettingAssign>> GetAllForSabha(int sabhaid)
        {
            var processConfigurationSettingAssignment = await shopRentalDbContext.ProcessConfigurationSettingAssign
                .Include(prc => prc.ShopRentalProcessConfigaration)
                .Include(s => s.Shop)
                .Where(m => m.SabhaId == sabhaid && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            // Add a log to check if the data is retrieved
            Console.WriteLine("Retrieved Data Count: " + processConfigurationSettingAssignment.Count());

            return processConfigurationSettingAssignment;
        }


        public async Task<ProcessConfigurationSettingAssign> GetByShopId(int shopId)
        {
            return
                await shopRentalDbContext.ProcessConfigurationSettingAssign
                    .Include(prc => prc.ShopRentalProcessConfigaration)
                    .Include(s => s.Shop)
                    .Where(m => m.ShopId == shopId && m.Status == 1)
                    .FirstOrDefaultAsync();
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }
    }
}
