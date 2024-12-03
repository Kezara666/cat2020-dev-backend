using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class VoucherLineRepository: Repository<VoucherLine> , IVoucherLineRepository
{
    public VoucherLineRepository(DbContext context) : base(context)
    {
        
    }

    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
}