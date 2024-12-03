using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;

namespace CAT20.WebApi.Resources.Final;

public class MakeVoucherApproveRejectResource
{
    
    public int VoucherId { get; set; }
    public string ActionNote { get; set; }
    public int State { get; set; }


}