﻿using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterBillPaymentCategoryRepository : IRepository<PaymentCategory>
    {
        Task<IEnumerable<PaymentCategory>> GetAllForSabha(int sabhaId);
    }
}