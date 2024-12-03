using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterBillDocumentRepository:IRepository<WaterBillDocument>
    {
        Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForConnection(string applicationNo, int Id);
        Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForApplication(string applicationNo);
    }
}
