using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount;

public interface ICommitmentLineVoteRepository : IRepository<CommitmentLineVotes>
{
    Task<CommitmentLineVotes> GetCommitmentLineVoteForVoucherPayment(int commitmentId, int commitmentLineId, int voteId);
}