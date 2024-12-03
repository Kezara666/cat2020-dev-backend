using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class CommitmentLogRepository: Repository<CommitmentLog>, ICommitmentLogRepository
{
    public CommitmentLogRepository(DbContext context) : base(context)
    {
    }


}
