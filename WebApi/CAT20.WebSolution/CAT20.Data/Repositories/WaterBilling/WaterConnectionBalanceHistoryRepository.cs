using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterConnectionBalanceHistoryRepository : Repository<WaterConnectionBalanceHistory>, IWaterConnectionBalanceHistoryRepository
    {
        public WaterConnectionBalanceHistoryRepository(DbContext context) : base(context)
        {
        }
    }
}
