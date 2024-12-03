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
    public interface ICutProvisionService
    {
        Task<bool> Create(CutProvision newCutProvision, HTokenClaim token);

        Task<CutProvisionResource> GetCuProvisionById(int Id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<CutProvisionResource> list)> GetCutProvisionForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<CutProvisionResource> list)> GetAllCutProvisionSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);


        Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token);
        Task<bool> WithdrawCutProvision(int cutProvisionId, HTokenClaim token);
    }
}
