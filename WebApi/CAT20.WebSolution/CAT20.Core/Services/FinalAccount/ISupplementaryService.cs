using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ISupplementaryService
    {
        Task<bool> Create(Supplementary newSupplementary, HTokenClaim token);

        Task<SupplementaryResource> GetSupplementaryId(int Id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<SupplementaryResource> list)> GetSupplementaryForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<SupplementaryResource> list)> GetAllSupplementaryForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);


        Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token);
        Task<bool> WithdrawSupplementary(int supplementaryId, HTokenClaim token);
    }
}
