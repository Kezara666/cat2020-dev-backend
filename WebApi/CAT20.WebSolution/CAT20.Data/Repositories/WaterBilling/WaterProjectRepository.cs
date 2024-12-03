using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterProjectRepository : Repository<WaterProject>, IWaterProjectRepository
    {

        public WaterProjectRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<WaterProject>> GetAllForOffice(int officeId)
        {

            return await waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(n => n.Natures).ToListAsync();

        }

        public async Task<IEnumerable<WaterProject>> GetAllByOfficeIds(List<int> officeIds)
        {

            return await waterBillingDbContext.WaterProjects.Where(wp => officeIds.Contains(wp.OfficeId)).ToListAsync();
        }

        public async Task<WaterProject> GetWaterProjectById(int id)
        {


            //var projects = await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == id).Include(wp => wp.WaterProjectGnDivisions).Include(wp => wp.SubRoads).Include(wp => wp.Natures).FirstOrDefaultAsync();
            //var projects = await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == id).Include(wp => wp.WaterProjectGnDivisions).FirstOrDefaultAsync();
            var projects = await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == id).FirstOrDefaultAsync();
            return projects;
        }

        public async Task<WaterProject> AddNature(int waterProjectId, int natureId)
        {
            var waterProject = waterBillingDbContext.WaterProjects.Include(m => m.Natures).FirstOrDefault(m => m.Id == waterProjectId);
            var nature = waterBillingDbContext.WaterProjectNatures.FirstOrDefault(m => m.Id == natureId);

            if (waterProject != null && nature != null)
            {


                waterProject.Natures.Add(nature);
                await waterBillingDbContext.SaveChangesAsync();

            }
            return waterProject;
        }


        public async Task<WaterProject> RemoveNature(int waterProjectId, int natureId)
        {

            //var nature = await waterBillingDbContext.WaterProjectNatures.Where(n => n.Id == natureId).Include(n => n.NonMeterFixCharges).Include(n => n.WaterTariffs).Include(n => n.WaterProjects).FirstOrDefaultAsync();
            var nofApplication = await waterBillingDbContext.ApplicationForConnections
                                        .Where(w=>w.RequestedNatureId == natureId)
                                        .Where(w=>w.SubRoad!.WaterProjectId == waterProjectId)
                                        .CountAsync();
            var nofWaterConnection = await waterBillingDbContext
                                           .WaterConnections
                                           .Where(w=>w.ActiveNatureId == natureId)
                                           .Where(w => w.SubRoad!.WaterProjectId == waterProjectId)
                                           .CountAsync();

            if (nofApplication > 0 || nofWaterConnection > 0)
            {

                return null;
            }
            else
            {

                var waterProject = await waterBillingDbContext.WaterProjects.Include(m => m.Natures).FirstOrDefaultAsync(m => m.Id == waterProjectId);
                var nature = waterBillingDbContext.WaterProjectNatures.FirstOrDefault(m => m.Id == natureId);


                if (waterProject != null && nature != null)
                {

                    waterProject.Natures.Remove(nature);
                    waterBillingDbContext.SaveChanges();


                }
                return waterProject;
            }
        }


        public async Task<WaterProject> GetAllNaturesForProject(int id)
        {
            var v = await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == id).Include(wp => wp.Natures).FirstOrDefaultAsync();

            //return await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == id).FirstOrDefaultAsync();
            return v;
        }

        public async Task<bool> IsRelationshipsExist(int waterProjectId)
        {

            var wp = await waterBillingDbContext.WaterProjects.Where(wp => wp.Id == waterProjectId).Include(wp => wp.Natures).Include(wp => wp.WaterProjectGnDivisions).Include(wp => wp.SubRoads).FirstOrDefaultAsync();


            if (wp.Natures.Count() > 0 || wp.SubRoads.Count() > 0 || wp.WaterProjectGnDivisions.Count() > 0)
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
