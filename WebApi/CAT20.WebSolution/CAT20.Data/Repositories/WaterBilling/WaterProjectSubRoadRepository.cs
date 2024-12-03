using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterProjectSubRoadRepository : Repository<WaterProjectSubRoad>, IWaterProjectSubRoadRepository
    {
        public WaterProjectSubRoadRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoad(int mainRoadId)
        {
            return await waterBillingDbContext.WaterProjectSubRoads
                .Include(m => m.WaterProject)
                .Include(m => m.MainRoad)
                .Where(sr => sr.MainRoadId == mainRoadId)
                 .OrderBy(sr => sr.WaterProjectId).ThenBy(sr => sr.MainRoad.Name).ThenBy(sr => sr.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForOffice(int officeId)
        {
            return await waterBillingDbContext.WaterProjectSubRoads
                 .Include(m => m.WaterProject)
                 .Include(m => m.MainRoad)
                  .OrderBy(sr => sr.WaterProjectId).ThenBy(sr => sr.MainRoad.Name).ThenBy(sr => sr.Name)
                .Where(sr => sr.WaterProject.OfficeId == officeId).ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForSabha(int sabhaId)
        {
            return await waterBillingDbContext.WaterProjectSubRoads
            .Include(m => m.WaterProject)
            .Include(m => m.MainRoad)
            .OrderBy(sr => sr.WaterProjectId).ThenBy(sr => sr.MainRoad.Name).ThenBy(sr => sr.Name)
                .Where(sr => sr.MainRoad.SabhaId == sabhaId).ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsNotAssignedReader(int waterProjectId)
        {
            var subroadIdsToExclue = await waterBillingDbContext.MeterReaderAssigns.Select(mra => mra.SubRoadId).ToListAsync();
            return await waterBillingDbContext.WaterProjectSubRoads.Where(sr => !subroadIdsToExclue.Contains(sr.Id) && sr.WaterProjectId == waterProjectId)
            .ToListAsync();

        }

        public async Task<bool> IsRelationshipsExist(int subRoadID)
        {
            var sr = await waterBillingDbContext.WaterProjectSubRoads.Where(sr => sr.Id == subRoadID).Include(sr => sr.ApplicationForConnections).Include(sr => sr.WaterConnections).Include(sr => sr.MeterConnectInfos).FirstOrDefaultAsync();


            if (sr.ApplicationForConnections.Count() > 0 || sr.WaterConnections.Count() > 0 || sr.WaterConnections.Count() > 0)
            {

                return false;
            }

            return true;
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForListOfIds(List<int> idList)
        {
            return await waterBillingDbContext.WaterProjectSubRoads.Include(sr => sr.WaterProject).Include(sr => sr.MainRoad).Where(sr => idList.Contains(sr.Id.Value)).OrderBy(sr => sr.WaterProject).ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoadForWaterProject(int waterProjectId, int mainRoadId)
        {
            return await waterBillingDbContext.WaterProjectSubRoads
                .Include(m => m.WaterProject)
                .Include(m => m.MainRoad)
                .Where(sr => sr.MainRoadId == mainRoadId && sr.WaterProjectId == waterProjectId)
                 .OrderBy(sr => sr.Name)
                .ToListAsync();
        }

        public async Task<WaterProjectSubRoad> GetByIdWithMainRoad(int id)
        {
            return await waterBillingDbContext.WaterProjectSubRoads.AsNoTracking().Include(sr => sr.MainRoad).FirstOrDefaultAsync(sr => sr.Id == id);
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}

