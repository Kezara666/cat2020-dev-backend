using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterProjectGnDivisionRepository : Repository<WaterProjectGnDivision>, IWaterProjectGnDivisionRepository
    {
        public WaterProjectGnDivisionRepository(DbContext context) : base(context)
        {
        }



        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForProject(int waterProjectId)
        {
            // return waterprojectGndivisions for a water project

            return await waterBillingDbContext.WaterProjectGnDivisions
             .Include(m=>m.WaterProject)
            .Where(wpgd => wpgd.WaterProjectId == waterProjectId)
            .ToListAsync();

        }

        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForOffice(int officeid)
        {
            // return waterprojectGndivisions for office
            return await waterBillingDbContext.WaterProjectGnDivisions
             .Include(m => m.WaterProject)
            .Where(wpgd => wpgd.WaterProject.OfficeId == officeid)
            .ToListAsync();
        }

        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectsUnderGnDivision(int gnDivisionId)
        {
            // return all water projects for a gn division

            return await waterBillingDbContext.WaterProjectGnDivisions.Where(wpgd => wpgd.ExternalGnDivisionId == gnDivisionId).Include(wp => wp.WaterProject).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllGnDivisionIdsForProject(int waterProjectId)
        {

            //var gnDivisionIds = await waterBillingDbContext.WaterProjectGnDivisions.Where(wpgd => wpgd.WaterProjectId == waterProjectId).Select(wpgd=>wpgd.ExternalGnDivisionId).ToListAsync();


            // return (IEnumerable<int>)gnDivisionIds;

            var gnDivisionIds = await waterBillingDbContext.WaterProjectGnDivisions
          .Where(wpgd => wpgd.WaterProjectId == waterProjectId && wpgd.ExternalGnDivisionId != null)
          .Select(wpgd => wpgd.ExternalGnDivisionId.Value)
          .ToListAsync();

            return gnDivisionIds;
        }

        public async Task<bool> RemoveGnDivision(int waterProjectId, int externalGnDivisionId)
        {
            var entityToRemove = waterBillingDbContext.WaterProjectGnDivisions.FirstOrDefault(gnd => gnd.WaterProjectId == waterProjectId && gnd.ExternalGnDivisionId == externalGnDivisionId);

            if (entityToRemove != null)
            {
                waterBillingDbContext.WaterProjectGnDivisions.Remove(entityToRemove);
                await waterBillingDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public  bool IsExistGnDivision(int waterProjectId, int externalGnDivisionId)
        {
           return waterBillingDbContext.WaterProjectGnDivisions.Any(gnd => gnd.WaterProjectId == waterProjectId && gnd.ExternalGnDivisionId == externalGnDivisionId);
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
       
    }
}
