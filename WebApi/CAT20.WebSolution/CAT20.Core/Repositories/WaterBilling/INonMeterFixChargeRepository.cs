using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface INonMeterFixChargeRepository : IRepository<NonMeterFixCharge>
    {
        Task<IEnumerable<NonMeterFixCharge>> GetAllChargesForProject(int waterProjectId);
    }
}
