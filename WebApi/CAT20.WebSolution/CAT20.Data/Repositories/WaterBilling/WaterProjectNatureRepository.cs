using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterProjectNatureRepository : Repository<WaterProjectNature>, IWaterProjectNatureRepository
    {
        public WaterProjectNatureRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WaterProjectNature>> GetAllForSabha(int sabhaid)
        {
            return await waterBillingDbContext.WaterProjectNatures.Where(wp => wp.SabhaId == sabhaid).ToListAsync();
        }

        public async Task<bool> IsRelationshipsExist(int natureId)
        {
            var nature = await waterBillingDbContext.WaterProjectNatures.Where(n => n.Id == natureId).Include(n => n.NonMeterFixCharges).Include(n => n.WaterTariffs).Include(n => n.WaterProjects).FirstOrDefaultAsync();


            if (nature.NonMeterFixCharges.Count() > 0 || nature.WaterTariffs.Count() > 0 || nature.WaterProjects.Count() > 0)
            {

                return false;
            }

            return true;
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
