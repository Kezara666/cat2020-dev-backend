using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterProjectMainRoadService
    {
        Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoads();
        Task<WaterProjectMainRoad> GetById(int id);
        Task<IEnumerable<WaterProjectMainRoad>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoadsForProject(int waterProjectId);
        Task<WaterProjectMainRoad> Create(WaterProjectMainRoad obj);

        Task Update(WaterProjectMainRoad objToBeUpdated, WaterProjectMainRoad obj);

        Task<(bool,string)> Delete( int id);



    }
}
