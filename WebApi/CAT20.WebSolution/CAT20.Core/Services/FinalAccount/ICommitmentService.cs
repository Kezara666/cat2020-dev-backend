using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.DTO.Final.Save;

namespace CAT20.Core.Services.FinalAccount
{
    public interface  ICommitmentService 
    {
       
        Task<(bool,string?)> CreateCommitment(Commitment commitmentToCreate,HTokenClaim token);
        Task<bool> UpdateCommitment(Commitment commitmentToCreate , int pId, HTokenClaim token);
        Task<(bool,string?)> ReleaseCommitmentHold(SaveCommitmentHoldReleaseResource holdReleaseResource, HTokenClaim token);
        Task<(bool,string?)> WithdrawCommitment(int commitmentId, HTokenClaim token);
        Task<bool> MakeCommitmentApprovalReject( MakeApprovalRejectResource approval, HTokenClaim token);
        Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCommitmentForApproval(int sabhaId, int stage, int pageNo, int  pageSize, string? filterKeyword);
        Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCommitmentForVoucher(int sabhaId, int stage, int pageNo, int  pageSize, string? filterKeyword);
        Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCreatedCommitment(int sabhaId, int stage,int userId, int pageNo, int  pageSize, string? filterKeyword);
        Task<CommitmentResource> GetCommitmentById(int id);
        Task<CommitmentResource> GetCommitmentById(string id);

    }
}
