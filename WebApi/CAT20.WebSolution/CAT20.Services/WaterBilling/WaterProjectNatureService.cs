using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterProjectNatureService : IWaterProjectNatureService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterProjectNatureService(IWaterBillingUnitOfWork wb_unitOfWork)
        {

            _wb_unitOfWork = wb_unitOfWork;
        }
        public async Task<WaterProjectNature> Create(WaterProjectNature newWPNature)
        {
            try
            {
                await _wb_unitOfWork.WaterProjectNatures.AddAsync(newWPNature);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newWPNature;
        }

        public async Task<bool> Delete(WaterProjectNature obj)
        {
            if (await _wb_unitOfWork.WaterProjectNatures.IsRelationshipsExist(obj.Id))
            {
                _wb_unitOfWork.WaterProjectNatures.Remove(obj);
                await _wb_unitOfWork.CommitAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<WaterProjectNature>> GetAllForSabha(int sabhaid)
        {
            return await _wb_unitOfWork.WaterProjectNatures.GetAllForSabha(sabhaid);
        }

        public async Task<IEnumerable<WaterProjectNature>> GetAllNatures()
        {
            return await _wb_unitOfWork.WaterProjectNatures.GetAllAsync();
        }

        public async Task<WaterProjectNature> GetById(int id)
        {
            return await _wb_unitOfWork.WaterProjectNatures.GetByIdAsync(id);
        }

        public async Task Update(WaterProjectNature objToBeUpdated, WaterProjectNature obj)
        {
            objToBeUpdated.Type = obj.Type;
            objToBeUpdated.SabhaId = obj.SabhaId;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }
    }
}
