using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IVoteAssignmentDetailsService
    {
        // Existing methods
        Task<IEnumerable<VoteAssignmentDetails>> GetAll();
        Task<VoteAssignmentDetails> GetById(int id);
        Task<VoteAssignmentDetails> Save(VoteAssignmentDetails newVoteAssignmentDetails);
        Task<(bool,string?)> NewSave(VoteAssignmentDetails newVoteAssignmentDetails,HTokenClaim token);
        Task Update(VoteAssignmentDetails voteAssignmentDetailsToBeUpdated, VoteAssignmentDetails voteAssignmentDetails);
        Task Delete(VoteAssignmentDetails voteAssignmentDetails);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllVoteAssignmentDetailsForVoteAssignmentId(int id);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllForOfficeId(int id);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllForSabhaId(int id);

        Task<IEnumerable<HVoteAssignmentDetails>> GetCustomVoteWithSubLevelsForVoteAssignmentId(int assignmentId);

        Task<HVoteAssignmentDetails> GetCustomVoteWithSubLevels(int Id);
        Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForVoteId(int voteId,HTokenClaim token);
        Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForOfficeAndBankAccountId(int bankaccountid, HTokenClaim token);
        Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForAccountId(int voteId, HTokenClaim token);
    }
}
