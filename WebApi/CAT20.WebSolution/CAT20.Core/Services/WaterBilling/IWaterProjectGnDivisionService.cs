using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterProjectGnDivisionService
    {
       

        Task<WaterProjectGnDivision> Create(WaterProjectGnDivision obj);

        Task Update(WaterProjectGnDivision objToBeUpdated, WaterProjectGnDivision obj);

        Task Delete(WaterProjectGnDivision obj);

        Task<bool> RemoveGnDivision(int waterProjectId, int externalGnDivisionId);


        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisions();

        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForProject(int waterProjectId);
        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForOffice(int officeid);
        Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectsUnderGnDivision(int gnDivisionId);

        Task<IEnumerable<GnDivisions>> GetAllGnDivisionForProject(int waterProjectId);
    }
}
