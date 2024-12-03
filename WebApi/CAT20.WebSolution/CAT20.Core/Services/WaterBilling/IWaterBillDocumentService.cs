using CAT20.Core.Models.WaterBilling;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterBillDocumentService
    {
        Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForConnection(string applicationNo,int Id);
        Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForApplication(string applicationNo);
        Task<WaterBillDocument> GetById(string applicationNo);
        Task<WaterBillDocument> Create(WaterBillDocument obj, object environment, string _uploadsFolder);
        Task Update(WaterBillDocument objToBeUpdated, WaterBillDocument obj);
    }
}
