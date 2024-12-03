using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IWaterBillCancelOrderService
    {

        Task<bool> ReversePayment(int mixId, int wcPrimaryId,int approvedBy);
    }
}
