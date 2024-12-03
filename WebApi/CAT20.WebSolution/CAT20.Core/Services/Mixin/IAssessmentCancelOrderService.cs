using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IAssessmentCancelOrderService
    {

        Task<bool>  ReversePayment(int mixId,int assessmentId);
    }
}
