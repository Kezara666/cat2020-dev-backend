using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterProjectNatureService
    {
        Task<IEnumerable<WaterProjectNature>> GetAllNatures();

        Task<WaterProjectNature> GetById(int id);

        Task<IEnumerable<WaterProjectNature>> GetAllForSabha(int sabhaid);
        Task<WaterProjectNature> Create(WaterProjectNature obj);
        Task Update(WaterProjectNature objToBeUpdated, WaterProjectNature obj);

        Task<bool> Delete(WaterProjectNature obj);
    }
}
