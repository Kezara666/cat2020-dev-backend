using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterTariffService : IWaterTariffService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterTariffService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<WaterTariff> Create(WaterTariff newTariff)
        {
            try
            {
                await _wb_unitOfWork.WaterTariffs.AddAsync(newTariff);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new WaterTariff();
            }

            return newTariff;
        }

        public async Task Delete(WaterTariff obj)
        {
            _wb_unitOfWork.WaterTariffs.Remove(obj);
            await _wb_unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<WaterTariff>> GetAllTariffs()
        {
            return await _wb_unitOfWork.WaterTariffs.GetAllAsync();
        }

        public async Task<IEnumerable<WaterTariff>> GetAllTariffsForWaterProject(int waterProjectId)
        {
            return await _wb_unitOfWork.WaterTariffs.GetAllTariffsForWaterProject(waterProjectId);
        }

        public async Task<WaterTariff> GetById(int id)
        {
            return await _wb_unitOfWork.WaterTariffs.GetByIdAsync(id);
        }

        public async Task Update(WaterTariff objToBeUpdated, WaterTariff obj)
        {
            objToBeUpdated.WaterProjectId = obj.WaterProjectId;
            objToBeUpdated.NatureId = obj.NatureId;
            objToBeUpdated.RangeStart = obj.RangeStart;
            objToBeUpdated.RangeEnd = obj.RangeEnd;
            objToBeUpdated.UnitPrice = obj.UnitPrice;
            objToBeUpdated.FixedCharge = obj.FixedCharge;

            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }
    }
}
