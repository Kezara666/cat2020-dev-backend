using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class OpeningBalanceInformationRepository : Repository<OpeningBalanceInformation>, IOpeningBalanceInformationRepository
    {
        public OpeningBalanceInformationRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OpeningBalanceInformation>> GetOpeningBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return await waterBillingDbContext.OpeningBalanceInformations.Where(obl => wcKeyIds.Contains(obl.WaterConnectionId)).ToListAsync();

        }

        //--------------- [Start - Define new api to filter openBalance data w.r.t. isProcessed] --------
        public async Task<OpeningBalanceInformation> GetNotProcessedOpenBalancesByWaterConnectionId(int waterConnectionId)
        {
            return await waterBillingDbContext.OpeningBalanceInformations
                .Where(m => m.WaterConnectionId == waterConnectionId && m.IsProcessed == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OpeningBalanceInformation>> GetAllNotProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return await waterBillingDbContext.OpeningBalanceInformations.Where(obl => wcKeyIds.Contains(obl.WaterConnectionId) && obl.IsProcessed == false).ToListAsync();
        }

        public async Task<IEnumerable<OpeningBalanceInformation>> GetAllProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return await waterBillingDbContext.OpeningBalanceInformations.Where(obl => wcKeyIds.Contains(obl.WaterConnectionId) && obl.IsProcessed == true).ToListAsync();
        }

        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsNotProcessedOpenBalanceForOfficeId(int officeId)
        {
            //var openBalids = waterBillingDbContext.WaterConnections
            //    .Include(w => w.OpeningBalanceInformation).Where(w => w.OfficeId == officeId)
            //    .Select(w => w.OpeningBalanceInformation!.Id)
            //    .ToList();




            var filterwcIds = waterBillingDbContext.OpeningBalanceInformations
                .Join(waterBillingDbContext.Balances,
                a => a.BalanceIdForCurrentBalance,
                 b => b.Id,
                 (a, b) => new { OpeningBalanceInformation = a, WaterConnectionBalance = b }
                 ).Where(b => b.WaterConnectionBalance.IsProcessed == false)
                 .Select(a => a.OpeningBalanceInformation.WaterConnectionId).ToList();




            //wcs.Where(w => filterwcIds.Contains(w.Id!.Value));

            var wcs = await waterBillingDbContext.WaterConnections
                .AsNoTracking()
                .Include(w => w.MeterConnectInfo)
                .Include(w => w.SubRoad).ThenInclude(s => s.WaterProject)
                .Include(w => w.SubRoad!.MainRoad)
                .Include(w => w.OpeningBalanceInformation).Where(w => filterwcIds.Contains(w.Id!.Value) && w.OfficeId == officeId).ToListAsync();

            return wcs;
        }

        public async Task<WaterConnection> GetWaterConnectionsNotProcessedOpenBalance(int wcId)
        {
            var x = waterBillingDbContext.WaterConnections.Include(w => w.OpeningBalanceInformation).Where(w => w.Id == wcId).FirstOrDefault();

            var bal = waterBillingDbContext.Balances.Where(b => b.Id == x.OpeningBalanceInformation!.BalanceIdForCurrentBalance!.Value).FirstOrDefault();
            if (bal != null)
            {
                x.OpeningBalanceInformation!.IsProcessed = bal!.IsProcessed;
                //return null;
            }
            return x;

        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
