using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IVoteAssignmentRepository : IRepository<VoteAssignment>
    {
        Task<IEnumerable<VoteAssignment>> GetAllForSabhaId(int sabhaid);
        Task<IEnumerable<VoteAssignment>> GetAllForOfficeId(int officeid);
        Task<IEnumerable<VoteAssignment>> GetAllForOfficeIdAndAccountDetailId(int officeid, int accountdetailid);
        Task<bool> HasVoteAssignmentsForAccountDetailId(int accountDetailId);
        Task<VoteAssignment> GetById(int id);
        Task<IEnumerable<VoteAssignment>> GetAllForVoteId(int Id);
        Task<bool> HasVoteAssignmentsForVoteId(int id);
        Task<VoteAssignment> GetByVoteId(int voteId, HTokenClaim token);
        Task<VoteAssignment> GetByVoteIdAndOffice(int voteId, HTokenClaim token);
        Task<int> GetAccountIdByVoteId( int voteId , HTokenClaim token);
        Task<int> GetAccountIdByVoteIdByOffice( int voteId , HTokenClaim token);
        Task<bool> HasAssigned(int voteId); 
        Task<IEnumerable<int>> GetAssignedVoteIds(int bankAccountId); 
        Task<int> GetAssignedBankAccountForSubOffice(int OfficeId); 
    }
}
