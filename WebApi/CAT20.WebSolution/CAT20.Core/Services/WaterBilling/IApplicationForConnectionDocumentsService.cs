using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IApplicationForConnectionDocumentsService
    {
      
        Task<IEnumerable<ApplicationForConnectionDocument>> GetAllDocumentsForApplication(string applicationNo);
        Task<ApplicationForConnectionDocument> GetById(int id);
       
        Task<ApplicationForConnectionDocument> Create(ApplicationForConnectionDocument obj, object environment, string _uploadsFolder);
        Task Update(ApplicationForConnectionDocument objToBeUpdated, ApplicationForConnectionDocument obj);

        Task Delete(ApplicationForConnectionDocument obj, object environment, string _uploadsFolder);

       
    }
}
