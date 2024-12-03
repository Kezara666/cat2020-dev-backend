using CAT20.Core.HelperModels;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class CalculatedBalanceResource
    {
        public decimal? RunningOverPay { get; set; }
        public HWaterBillBalance? hWaterBillBalance {  get; set; }
        
        public IEnumerable<WaterConnectionBalanceResource>? waterConnectionBalances { get; set; }
    }
}
