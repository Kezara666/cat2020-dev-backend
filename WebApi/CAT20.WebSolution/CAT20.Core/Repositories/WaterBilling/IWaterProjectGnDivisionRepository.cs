using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterProjectGnDivisionRepository : IRepository<WaterProjectGnDivision>
    {
        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForProject(int waterProjectId);
        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForOffice(int officeid);
        Task<bool> RemoveGnDivision(int waterProjectId, int externalGnDivisionId);
        bool IsExistGnDivision(int waterProjectId, int externalGnDivisionId);

        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectsUnderGnDivision(int gnDivisionId);

        Task<IEnumerable<int>> GetAllGnDivisionIdsForProject(int waterProjectId);
    }
}
