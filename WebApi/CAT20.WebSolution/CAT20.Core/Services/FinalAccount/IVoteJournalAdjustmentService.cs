using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IVoteJournalAdjustmentService
    {

        Task<VoteBalance> GetForJournalAdjustment(int voteDetailId, HTokenClaim token);

        Task<(VoteBalance, string)> GetActiveVoteBalance(int VoteDetailId, HTokenClaim token);

        Task<bool> Create(VoteJournalAdjustment newjournalAdjustment, HTokenClaim token);

        Task<VoteJournalAdjustmentResource> GetJournalAdjustmentById(int Id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<VoteJournalAdjustmentResource> list)> GetJournalAdjustmentFroApproval(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<VoteJournalAdjustmentResource> list)> GetAllJournalAdjustmentForSabha(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord);


        Task<bool> MakeApproveReject(MakeVoteJournalApproveRejectResource request, HTokenClaim token);
        Task<bool> WithdrawJournalAdjustment(int journalId, HTokenClaim token);

    }
}
