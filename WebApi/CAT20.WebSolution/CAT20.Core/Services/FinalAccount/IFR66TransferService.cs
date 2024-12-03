using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IFR66TransferService
    {
        Task<bool> Create(FR66Transfer newTransfer, HTokenClaim token);

        Task<FR66TransferResource> GetFR66ById(int Id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<FR66TransferResource> list)> GetFR66TransferFroApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<FR66TransferResource> list)> GetAllFR66TransferForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);


        Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token);
        Task<bool> WithdrawFR66Transfer(int journalId, HTokenClaim token);
    }
}
