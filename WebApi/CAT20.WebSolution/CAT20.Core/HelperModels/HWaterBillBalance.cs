using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HWaterBillBalance
    {
        public decimal? LYArrears { get; set; } = 0;
        public decimal? LYArrearsPaying { get; set; } = 0;

        public decimal? LYAPayingVAT { get; set; } = 0;

        public decimal? TYArrears { get; set; } = 0;
        public decimal? TYArrearsPaying { get; set; } = 0;
        public decimal? TYAPayingVAT { get; set; } = 0;
        public decimal? TMCharge { get; set; } = 0;
        public decimal? TMChargePaying { get; set; } = 0;
        public decimal? TMPayingVAT { get; set; } = 0;

        public decimal? OverPayment { get; set; } = 0;
        public decimal? OverPaymentVAT { get; set; } = 0;
        public decimal? PayingAmount { get; set; } = 0;


    }
}
