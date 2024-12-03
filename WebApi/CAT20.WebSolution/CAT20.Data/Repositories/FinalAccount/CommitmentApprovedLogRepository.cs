using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;


namespace CAT20.Data.Repositories.FinalAccount;

public class CommitmentApprovedLogRepository: Repository<CommitmentActionsLog>, ICommitmentActionLogRepository
{
    public CommitmentApprovedLogRepository(DbContext context) : base(context)
    {
    }


}