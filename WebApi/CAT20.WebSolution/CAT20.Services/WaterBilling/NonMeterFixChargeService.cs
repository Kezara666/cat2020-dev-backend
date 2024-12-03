using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class NonMeterFixChargeService : INonMeterFixChargeService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public NonMeterFixChargeService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }
        public async Task<NonMeterFixCharge> Create(NonMeterFixCharge newCharge)
        {
            try
            {
                await  _wb_unitOfWork.NonMeterFixCharges.AddAsync(newCharge);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new NonMeterFixCharge();
            }

            return newCharge;
        }

        public async Task Delete(NonMeterFixCharge obj)
        {
            _wb_unitOfWork.NonMeterFixCharges.Remove(obj);
            await _wb_unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<NonMeterFixCharge>> GetAllCharges()
        {
            return await _wb_unitOfWork.NonMeterFixCharges.GetAllAsync();
        }

        public async Task<IEnumerable<NonMeterFixCharge>> GetAllChargesForProject(int waterProjectId)
        {
            return await _wb_unitOfWork.NonMeterFixCharges.GetAllChargesForProject(waterProjectId);
        }

        public async Task<NonMeterFixCharge> GetById(int id)
        {
            return await _wb_unitOfWork.NonMeterFixCharges.GetByIdAsync(id);
        }

        public async Task Update(NonMeterFixCharge objToBeUpdated, NonMeterFixCharge obj)
        {
            objToBeUpdated.WaterProjectId = obj.WaterProjectId;
            objToBeUpdated.NatureId = obj.NatureId;
            objToBeUpdated.FixedCharge = obj.FixedCharge;

            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }
    }
}
