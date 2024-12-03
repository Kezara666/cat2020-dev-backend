using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterConnectionNatureLogRepository:IRepository<WaterConnectionNatureLog>
    {
        Task<IEnumerable<WaterConnectionNatureLog>> GetAllNaturesForConnection(string applicationNo, int Id);
        Task<IEnumerable<WaterConnectionNatureLog>> GetAllNaturesByWCId(int wcId);
        Task<WaterConnectionNatureLog> GetFirstNatureByWCId(int wcId);
      
    }
}
