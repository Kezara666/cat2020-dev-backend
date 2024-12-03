using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Services.FinalAccount;

public interface IDepositSubInfoService
{
    Task<IEnumerable<DepositSubInfo>> GetAllDepositSubInfoForSabha(int sabahId);
    Task<bool> Create(DepositSubInfo newdocumentSequenceNumber,HTokenClaim token);

}