using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IShopRentalProcessPaymentService
    {
        Task<bool> ProcessPayment(int mixinOrderId, int cashierId, int shopId);
    }
}
