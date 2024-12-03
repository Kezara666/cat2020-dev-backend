using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IMeterReaderAssignRepository :IRepository<MeterReaderAssign>
    {
        Task<IEnumerable<MeterReaderAssign>> GetAllForMeterReader(int readerId);

        
    }
}
