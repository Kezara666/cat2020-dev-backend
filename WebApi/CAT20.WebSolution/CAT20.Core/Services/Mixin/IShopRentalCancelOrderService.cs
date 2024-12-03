using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IShopRentalCancelOrderService
    {
        Task<bool> ReverseShopRentalPayment(int mixId, int shopId, int approvedBy);
    }
}
