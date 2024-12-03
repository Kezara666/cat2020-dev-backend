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
    public class WaterBillDocumentRepository : Repository<WaterBillDocument>, IWaterBillDocumentRepository
    {
        public WaterBillDocumentRepository(DbContext context) : base(context)
        {
        }

        public Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForApplication(string applicationNo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForConnection(string applicationNo, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
