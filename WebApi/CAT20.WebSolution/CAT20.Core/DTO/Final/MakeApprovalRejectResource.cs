using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;

namespace CAT20.WebApi.Resources.Final;

public class MakeApprovalRejectResource
{
    //public CommitmentLog? CommitmentLog;
    //public CommitmentActionsLog? ApprovedLog;

    public int? CommitmentId { get; set; }
    public string? ActionNote { get; set; }
    public int? State { get; set; }

}