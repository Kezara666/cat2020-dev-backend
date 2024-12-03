using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionNatureLogService
    {
        //Task<IEnumerable<WaterConnectionNatureLog>> GetAllNaturesForConnection(string applicationNo, int Id);
        Task<WaterConnectionNatureLog> GetById(int id);
        Task<WaterConnectionNatureLog> Create(WaterConnectionNatureLog obj);
        Task<bool> Approve(WaterConnectionNatureLog objToBeUpdated, WaterConnectionNatureLog obj);
        //Task<bool> Disapprove(WaterConnectionNatureLog obj);
        Task<bool> Disapprove(WaterConnectionNatureLog objToBeUpdated, WaterConnectionNatureLog obj);
    }
}
