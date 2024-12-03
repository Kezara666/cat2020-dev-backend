using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterProjectMainRoadRepository : IRepository<WaterProjectMainRoad>
    {
        Task<IEnumerable<WaterProjectMainRoad>> GetAllForSabha(int sabhaid);

        Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoadsForProject(int waterProjectId);

        Task<bool> IsRelationshipsExist(int mainRoadId);
    }
}
