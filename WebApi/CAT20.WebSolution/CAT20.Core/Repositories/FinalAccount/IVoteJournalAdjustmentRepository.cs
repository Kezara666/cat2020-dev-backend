using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IVoteJournalAdjustmentRepository :IRepository<VoteJournalAdjustment>
    {

        Task<VoteBalance> GetForJournalAdjustment(int voteDetailId,int year);

        Task<VoteJournalAdjustment> GetJournalAdjustmentById(int id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<VoteJournalAdjustment> list)> GetJournalAdjustmentFroApproval(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<VoteJournalAdjustment> list)> GetAllJournalAdjustmentForSabha(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord);
    }
}
