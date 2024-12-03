using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterTariffService
    {
        Task<IEnumerable<WaterTariff>> GetAllTariffs();
        Task<IEnumerable<WaterTariff>> GetAllTariffsForWaterProject(int waterProjectId);

        Task<WaterTariff> GetById(int id);
        Task<WaterTariff> Create(WaterTariff obj);

        Task Update(WaterTariff objToBeUpdated, WaterTariff obj);

        Task Delete(WaterTariff obj);
    }
}
