using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class VoucherApprovedLogRepository : Repository<VoucherActionLog>, IVoucherActionLogRepository
{
    public VoucherApprovedLogRepository(DbContext context) : base(context)
    {
    }

    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
}