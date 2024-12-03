using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.ShopRental
{
    public class ShopRentalProcessConfigarationService : IShopRentalProcessConfigarationService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public ShopRentalProcessConfigarationService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShopRentalProcessConfigaration> Create(ShopRentalProcessConfigaration newProcessConfig)
        {
            try
            {
                await _unitOfWork.ShopRentalProcessConfigaration.AddAsync(newProcessConfig);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newProcessConfig;
        }

        public async Task Update(ShopRentalProcessConfigaration processConfigToBeUpdated, ShopRentalProcessConfigaration processConfig)
        {
            processConfigToBeUpdated.Name                       = processConfig.Name;
            processConfigToBeUpdated.FineRateTypeId             = processConfig.FineRateTypeId;
            processConfigToBeUpdated.FineDailyRate              = processConfig.FineDailyRate;
            processConfigToBeUpdated.FineMonthlyRate            = processConfig.FineMonthlyRate;
            processConfigToBeUpdated.Fine1stMonthRate           = processConfig.Fine1stMonthRate;
            processConfigToBeUpdated.Fine2ndMonthRate           = processConfig.Fine2ndMonthRate;
            processConfigToBeUpdated.Fine3rdMonthRate           = processConfig.Fine3rdMonthRate;
            processConfigToBeUpdated.FineFixAmount = processConfig.FineFixAmount;
            processConfigToBeUpdated.FineMonthlyRate            = processConfig.FineMonthlyRate;
            processConfigToBeUpdated.FineDate                   = processConfig.FineDate;
            processConfigToBeUpdated.RentalPaymentDateTypeId    = processConfig.RentalPaymentDateTypeId;
            processConfigToBeUpdated.FineCalTypeId              = processConfig.FineCalTypeId;
            processConfigToBeUpdated.FineChargingMethodId       = processConfig.FineChargingMethodId;

            processConfigToBeUpdated.UpdatedBy = processConfig.UpdatedBy;
            processConfigToBeUpdated.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ShopRentalProcessConfigaration obj)
        {
            _unitOfWork.ShopRentalProcessConfigaration.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ShopRentalProcessConfigaration> GetById(int id)
        {
            return await _unitOfWork.ShopRentalProcessConfigaration.GetById(id);
        }

        public async Task<IEnumerable<ShopRentalProcessConfigaration>> GetAllForSabha(int sabhaId)
        {
            try
            {

                return await _unitOfWork.ShopRentalProcessConfigaration.GetAllForSabha(sabhaId);
            }
            catch (Exception ex) {
                return null;
            
            }
        }

        public async Task<ShopRentalProcessConfigaration> GetByProcessConfigId(int processConfigId)
        {
            return await _unitOfWork.ShopRentalProcessConfigaration.GetByProcessConfigId(processConfigId);
        }
    }
}
