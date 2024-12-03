using CAT20.Core;
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
    public class ProcessConfigurationSettingAssignService : IProcessConfigurationSettingAssignService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;

        public ProcessConfigurationSettingAssignService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ProcessConfigurationSettingAssign>> CreateMultiple(List<ProcessConfigurationSettingAssign> objs)
        {
            try
            {
                await _unitOfWork.ProcessConfigurationSettingAssign.AddRangeAsync(objs);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                return new List<ProcessConfigurationSettingAssign>();
            }
            return objs;
        }

        public async Task<IEnumerable<ProcessConfigurationSettingAssign>> GetAllForSabha(int sabhaid)
        {

            try
            {
                return await _unitOfWork.ProcessConfigurationSettingAssign.GetAllForSabha(sabhaid);
            }
            catch (Exception ex)
            {
                throw ;

            }
          
        }

        public async Task<ProcessConfigurationSettingAssign> GetById(int id)
        {
            return await _unitOfWork.ProcessConfigurationSettingAssign.GetByIdAsync(id);
        }

        public async Task Update(ProcessConfigurationSettingAssign objToBeUpdated, ProcessConfigurationSettingAssign obj)
        {
            objToBeUpdated.ShopRentalProcessConfigarationId = obj.ShopRentalProcessConfigarationId;

            await _unitOfWork.CommitAsync();
        }

        public async Task<ProcessConfigurationSettingAssign> GetByShopId(int shopId)
        {
            return await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shopId);
        }

       
        public async Task<ProcessConfigurationSettingAssign> Create(ProcessConfigurationSettingAssign newProcConfigAssign)
        {
            try
            {
                await _unitOfWork.ProcessConfigurationSettingAssign.AddAsync(newProcConfigAssign);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newProcConfigAssign;
        }
    }
}
