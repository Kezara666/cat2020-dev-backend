using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface INonMeterFixChargeService
    {
        Task<IEnumerable<NonMeterFixCharge>> GetAllCharges();
        Task<IEnumerable<NonMeterFixCharge>> GetAllChargesForProject(int waterProjectId);

        Task<NonMeterFixCharge> GetById(int id);
        Task<NonMeterFixCharge> Create(NonMeterFixCharge obj);

        Task Update(NonMeterFixCharge objToBeUpdated, NonMeterFixCharge obj);

        Task Delete(NonMeterFixCharge obj);
    }
}
