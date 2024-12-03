using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionStatusLogService
    {
        Task<WaterConnectionStatusLog> GetById(int id);
        Task<WaterConnectionStatusLog> Create(WaterConnectionStatusLog obj);

        Task<bool> Approve(WaterConnectionStatusLog objToBeUpdated, WaterConnectionStatusLog obj);

        Task<bool> Disapprove(WaterConnectionStatusLog objToBeUpdated, WaterConnectionStatusLog obj);
    }
}
