using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IVoteAssignmentDetailsRepository : IRepository<VoteAssignmentDetails>
    {
        Task<IEnumerable<VoteAssignmentDetails>> GetAll();
        Task<VoteAssignmentDetails> GetById(int id);
        Task<VoteAssignmentDetails> GetVoteAssignmentDetails(int id);
        Task<VoteAssignmentDetails> GetByAssignmentId(int assignmnetId);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllVoteAssignmentDetailsForVoteAssignmentId(int Id);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllForOfficeId(int Id);
        Task<IEnumerable<VoteAssignmentDetails>> GetAllForSabhaId(int Id);
        Task<VoteAssignmentDetails> GetForCrossOrder(int Id);
        Task<VoteAssignmentDetails> GetWithVoteAssignmentById(int Id);


        Task<IEnumerable<VoteAssignmentDetails>> GetCustomVoteWithZeroLevelsForVoteAssignmentId(int assignmnetId);
        ICollection<VoteAssignmentDetails> GetChildren(int? parentId);
        Task<VoteAssignmentDetails> GetCustomVoteWithSubLevels(int Id);

        Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithZeroLevelsForVoteId(int voteId, HTokenClaim token);
        Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithSubLevelsForOfficeAndBankAccountId(int bankaccountid, HTokenClaim token);
        Task<IEnumerable<VoteAssignmentDetails>> getCustomVoteWithZeroLevelsForAccountId(int accountid, HTokenClaim token);
    }
}
