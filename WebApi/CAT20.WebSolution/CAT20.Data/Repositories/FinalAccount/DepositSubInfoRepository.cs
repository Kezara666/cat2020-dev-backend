using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class DepositSubInfoRepository : Repository<DepositSubInfo>, IDepositSubInfoRepository
{
    public DepositSubInfoRepository(DbContext context) : base(context)
    {
    }
    
    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }

     public async Task<IEnumerable<DepositSubInfo>> GetAllDepositSubInfoForSabha(int sabahId)
    {
        return await voteAccDbContext.DepositSubInfo
                        .Where(m=> m.SabhaId == sabahId && m.Status !=0)
                        .ToListAsync();
    }

    //public async Task<DepositSubInfo> GetNextSequenceNumberForYearOfficePrefixAsync(int year, int? sabhaId)
    //{
    //    return await voteAccDbContext.DepositSubInfo
    //        .Where(m => m.Year == year && m.SabhaId==sabhaId)
    //        .FirstOrDefaultAsync();
    //}

    //public async Task<bool> HasCommitmentSequenceNumberForCurrentYear(int year, int? sabhaId, string prefix)
    //{
    //    return await voteAccDbContext.DepositSubInfo
    //                                     .AnyAsync(m => m.Year == year && m.SabhaId == sabhaId && m.Prefix == prefix);
    //}
}