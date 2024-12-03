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
    public class NonMeterFixChargeRepository : Repository<NonMeterFixCharge>, INonMeterFixChargeRepository
    {
        public NonMeterFixChargeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<NonMeterFixCharge>> GetAllChargesForProject(int waterProjectId)
        {
            return await waterBillingDbContext.NonMeterFixCharges.Where(nmfx => nmfx.WaterProjectId == waterProjectId).Include(nmfx=>nmfx.WaterProjectNature).ToListAsync();
        }
        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
