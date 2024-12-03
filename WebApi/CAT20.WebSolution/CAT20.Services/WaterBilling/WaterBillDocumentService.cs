using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using Microsoft.Extensions.Options;
using System;

namespace CAT20.Services.WaterBilling
{
    public class WaterBillDocumentService : IWaterBillDocumentService
    {
       


        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterBillDocumentService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }
        public async Task<WaterBillDocument> Create(WaterBillDocument obj, object environment, string _uploadsFolder)
        {
            try
            {


                if (obj.File == null || obj.File.Length == 0)
                {
                    return obj;
                }

                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString().Substring(0,12) + "_" + obj.File.FileName;

                string filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await obj.File.CopyToAsync(stream);
                }

                obj.Uri = filePath;


                await _wb_unitOfWork.WaterBillDocuments.AddAsync(obj);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return obj;
        }

        public Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForApplication(string applicationNo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WaterBillDocument>> GetAllDocumentsForConnection(string applicationNo, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<WaterBillDocument> GetById(string applicationNo)
        {
            throw new NotImplementedException();
        }

        public Task Update(WaterBillDocument objToBeUpdated, WaterBillDocument obj)
        {
            throw new NotImplementedException();
        }
    }
}
