using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Services.FinalAccount;

namespace CAT20.Services.FinalAccount;

public class DepositSubInfoService : IDepositSubInfoService
{

    private readonly IVoteUnitOfWork _unitOfWork;

    public DepositSubInfoService(IVoteUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

     public async Task<IEnumerable<DepositSubInfo>> GetAllDepositSubInfoForSabha(int sabahId)
    {
        return await _unitOfWork.DepositSubInfo.GetAllDepositSubInfoForSabha(sabahId);
    }



    public async Task<bool> Create(DepositSubInfo depositSubInfo,HTokenClaim token)
    {
        try
        {
            var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
            if (session != null)
            {

                depositSubInfo.CreatedBy = token.userId;
                depositSubInfo.SabhaId = token.sabhaId;
                depositSubInfo.CreatedAt = session.StartAt;

                depositSubInfo.SystemCreateAt = DateTime.Now;

                await _unitOfWork.DepositSubInfo.AddAsync(depositSubInfo);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                throw new Exception("No Active Session Found");
            }
        } catch (Exception ex)
        {
            return false;
        }
        
    }
}