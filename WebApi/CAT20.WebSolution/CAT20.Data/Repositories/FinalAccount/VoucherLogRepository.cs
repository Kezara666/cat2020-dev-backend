using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class VoucherLogRepository : Repository<VoucherLog>, IVoucherLogRepository
{
    public VoucherLogRepository(DbContext context) : base(context)
    {
    }


    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
}