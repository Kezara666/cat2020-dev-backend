using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IVoteAssignmentService
    {
        Task<IEnumerable<VoteAssignment>> GetAllForSabhaId(int sabhaid);
        Task<IEnumerable<VoteAssignment>> GetAllForOfficeId(int officeid);
        Task<IEnumerable<VoteAssignment>> GetAllForOfficeIdAndAccountDetailId(int officeid, int accountdetailid);
        Task<bool> HasVoteAssignmentsForAccountDetailId(int accountDetailId);
        Task<VoteAssignment> GetById(int id);
        Task<VoteAssignment> Create(VoteAssignment newVoteAssignment);
        Task<(bool,string?)> NewCreate(IEnumerable<VoteAssignment> newVoteAssignmentList,HTokenClaim token);
        Task Update(VoteAssignment voteAssignmentToBeUpdated, VoteAssignment voteAssignment);
        Task Delete(VoteAssignment voteAssignment);
        Task<IEnumerable<VoteAssignment>> GetAllForVoteId(int id);
        Task<bool> HasVoteAssignmentsForVoteId(int id);
        Task<int> GetAssignedBankAccountForSubOffice(int OfficeId);

        Task<int> GetAccountIdByVoteId(int voteId, HTokenClaim token);

    }
}

