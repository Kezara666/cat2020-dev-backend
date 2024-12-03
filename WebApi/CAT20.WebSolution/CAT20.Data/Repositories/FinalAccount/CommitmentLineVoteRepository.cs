using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class CommitmentLineVoteRepository: Repository<CommitmentLineVotes> , ICommitmentLineVoteRepository
{
    public CommitmentLineVoteRepository(DbContext context) : base(context)
    {
    }

    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
    
    // public async Task<CommitmentLineVotes> GetCommitmentLineVoteForVoucherPayment(int commitmentId, int commitmentLineId, int voteId)
    // {
    //     return await voteAccDbContext.Commitment
    //         .Include(c => c.CommitmentLine.Where(cl => cl.Id == commitmentLineId))
    //         .ThenInclude(cl => cl.CommitmentLineVotes.Where(clv => clv.VoteId == voteId))
    //         .Where(c => c.Id == commitmentId)
    //         .FirstOrDefaultAsync();
    // }
    
    public async Task<CommitmentLineVotes> GetCommitmentLineVoteForVoucherPayment(int commitmentId, int commitmentLineId, int voteId)
    {
        return await voteAccDbContext.CommitmentLineVotes
            .Include(clv => clv.CommitmentLine)
            .ThenInclude(cl => cl.Commitment)
            .Where(clv => clv.VoteId == voteId && clv.CommitmentLine.Id == commitmentLineId && clv.CommitmentLine.CommitmentId == commitmentId)
            .FirstOrDefaultAsync();
    }
}