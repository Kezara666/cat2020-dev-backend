using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ICommitmentRepository : IRepository<Commitment>
    {
        Task<IEnumerable<Commitment>> GetAll();
        Task<Commitment> GetCommitmentById(string id);
        Task<Commitment> GetCommitmentById(int? id);
        Task<Commitment> GetForCreateVoucherById(int id);
        Task<(int totalCount,IEnumerable<Commitment> list)> getCommitmentForApproval(int sabhaId, int stage, int pageNo, int  pageSize,string? filterKeyword);
        Task<(int totalCount,IEnumerable<Commitment> list)> getCommitmentForVoucher(int sabhaId, int stage, int pageNo, int  pageSize,string? filterKeyword);
        Task<(int totalCount,IEnumerable<Commitment> list)> getCreatedCommitment(int sabhaId, int stage, int userId,int pageNo, int  pageSize, string? filterKeyword);

    }
}
