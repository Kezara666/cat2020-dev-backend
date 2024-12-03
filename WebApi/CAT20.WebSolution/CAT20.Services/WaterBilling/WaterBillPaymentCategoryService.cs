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
    public class WaterBillPaymentCategoryService : IWaterBillPaymentCategoryService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        public WaterBillPaymentCategoryService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<IEnumerable<PaymentCategory>> GetAll()
        {
           return await _wb_unitOfWork.PaymentCategories.GetAllAsync();
        }

        public async Task<IEnumerable<PaymentCategory>> GetAllForSabha(int sabhaId)
        {
            return await _wb_unitOfWork.PaymentCategories.GetAllForSabha(sabhaId);
        }
    }
}
