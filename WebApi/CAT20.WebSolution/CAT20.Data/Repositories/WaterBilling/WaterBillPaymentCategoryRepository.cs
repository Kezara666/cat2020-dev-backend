using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterBillPaymentCategoryRepository : Repository<PaymentCategory>, IWaterBillPaymentCategoryRepository
    {
        public WaterBillPaymentCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentCategory>> GetAllForSabha(int sabhaId)
        {
           return await waterBillingDbContext.PaymentCategories.Where(c => c.SabhaId == sabhaId).ToListAsync();
           
        }


        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
