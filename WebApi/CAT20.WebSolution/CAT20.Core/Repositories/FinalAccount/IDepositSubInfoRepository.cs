using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount;

public interface IDepositSubInfoRepository: IRepository<DepositSubInfo>
{
    Task<IEnumerable<DepositSubInfo>> GetAllDepositSubInfoForSabha(int sabahId);
    //Task<bool> HasCommitmentSequenceNumberForCurrentYear(int year, int? sabhaId, string prefix);
    //Task<DepositSubInfo> GetNextSequenceNumberForYearOfficePrefixAsync(int year, int? sabhaId);
}