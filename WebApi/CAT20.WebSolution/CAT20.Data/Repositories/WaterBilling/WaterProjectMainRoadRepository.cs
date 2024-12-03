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
    public class WaterProjectMainRoadRepository : Repository<WaterProjectMainRoad>, IWaterProjectMainRoadRepository
    {
        public WaterProjectMainRoadRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<WaterProjectMainRoad>> GetAllForSabha(int sabhaid)
        {
            return await waterBillingDbContext.WaterProjectMainRoads.Where(wp => wp.SabhaId == sabhaid).ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoadsForProject(int waterProjectId)
        {
          return  await  waterBillingDbContext.WaterProjects.Where(wp => wp.Id == waterProjectId).SelectMany(wp => wp.SubRoads).Select(sr => sr.MainRoad).Distinct().ToListAsync();
        }

        public async Task<bool> IsRelationshipsExist(int mainRoadId)
        {
            var mr = await waterBillingDbContext.WaterProjectMainRoads.Where(mr => mr.Id == mainRoadId).Include(mr => mr.SubRoads).FirstOrDefaultAsync();


            if ( mr.SubRoads.Count() > 0)
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
