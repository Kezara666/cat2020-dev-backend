using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    internal class MeterReaderAssignRepository : Repository<MeterReaderAssign>, IMeterReaderAssignRepository
    {
        public MeterReaderAssignRepository(DbContext context) : base(context)
        {
        }



        public async Task<IEnumerable<MeterReaderAssign>> GetAllForMeterReader(int readerId)
        {
            return await waterBillingDbContext.MeterReaderAssigns.Where(mra => mra.MeterReaderId == readerId).ToListAsync();
        }
       
        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
