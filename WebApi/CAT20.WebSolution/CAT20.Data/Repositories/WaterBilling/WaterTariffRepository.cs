using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterTariffRepository : Repository<WaterTariff>, IWaterTariffRepository
    {
        public WaterTariffRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WaterTariff>> GetAllTariffsForWaterProject(int waterProjectId)
        {
            return await waterBillingDbContext.WaterTariffs.Where(wt => wt.WaterProjectId == waterProjectId).Include(wt => wt.WaterProjectNature).ToListAsync();
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
