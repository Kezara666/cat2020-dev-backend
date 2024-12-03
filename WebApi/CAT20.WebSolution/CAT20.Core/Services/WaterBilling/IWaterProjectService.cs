using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterProjectService
    {
        Task<IEnumerable<WaterProject>> GetAllProjects();
        Task<WaterProject> GetById(int id);
        Task<WaterProject> GetWaterProjectById(int id);

     

        Task<IEnumerable<WaterProject>> GetAllForOffice(int officeid);
        Task<IEnumerable<WaterProject>> GetAllForSabha(int sabhaid);

        Task<WaterProject> Create(WaterProject obj);
        

        Task<WaterProject> AddNature(int waterProjectId, int natureId);
        Task<WaterProject> RemoveNature(int waterProjectId, int natureId);

        Task<WaterProject> GetAllNaturesForProject(int id);
        

        Task Update(WaterProject objToBeUpdated, WaterProject obj);

        Task<bool> Delete(WaterProject obj);
    }
}
