using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterProjectRepository : IRepository<WaterProject>
    {
        Task<IEnumerable<WaterProject>> GetAllForOffice(int officeid);
        Task<IEnumerable<WaterProject>> GetAllByOfficeIds(List<int> officeIds);

        Task<WaterProject> AddNature(int waterProjectId,int natureId);
        Task<WaterProject> RemoveNature(int waterProjectId,int natureId);
        Task<WaterProject> GetAllNaturesForProject(int id);
        Task<WaterProject> GetWaterProjectById(int id);

        Task<bool> IsRelationshipsExist(int waterProjectId);
    }
}
