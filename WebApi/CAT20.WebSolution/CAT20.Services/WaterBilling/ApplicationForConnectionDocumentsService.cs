using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class ApplicationForConnectionDocumentsService : IApplicationForConnectionDocumentsService
    {

        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public ApplicationForConnectionDocumentsService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public Task<IEnumerable<ApplicationForConnectionDocument>> ApproveWaterConnectionApplication(List<string> applicationNo)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationForConnectionDocument> Create(ApplicationForConnectionDocument obj, object environment, string _uploadsFolder)
        {
            string? filePath = null;



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

                string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;

                filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await obj.File.CopyToAsync(stream);
                }

                obj.Uri = uniqueFileName;


                await _wb_unitOfWork.ApplicationForConnectionDocuments.AddAsync(obj);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return obj;

        }

        public async Task Delete(ApplicationForConnectionDocument obj, object environment, string _uploadsFolder)
        {

            try
            {


                if (Directory.Exists(_uploadsFolder))
                {
                    var filePath = Path.Combine(_uploadsFolder, obj.Uri);

                    _wb_unitOfWork.ApplicationForConnectionDocuments.Remove(obj);


                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    await _wb_unitOfWork.CommitAsync();
                }
                else
                {
                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());

            }
        }

        public async Task<IEnumerable<ApplicationForConnectionDocument>> GetAllDocumentsForApplication(string applicationNo)
        {
            return await _wb_unitOfWork.ApplicationForConnectionDocuments.GetAllDocumentsForApplication(applicationNo);
        }


        public async Task<ApplicationForConnectionDocument> GetById(int id)
        {
            return await _wb_unitOfWork.ApplicationForConnectionDocuments.GetByIdAsync(id);
        }

        public Task Update(ApplicationForConnectionDocument objToBeUpdated, ApplicationForConnectionDocument obj)
        {
            throw new NotImplementedException();
        }



    }
}
