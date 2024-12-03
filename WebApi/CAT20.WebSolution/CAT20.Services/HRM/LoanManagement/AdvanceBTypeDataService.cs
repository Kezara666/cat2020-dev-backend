using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using CAT20.Core.DTO.HRM;

namespace CAT20.Services.HRM.LoanManagement
{
    public class AdvanceBTypeDataService : IAdvanceBTypeDataService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        public AdvanceBTypeDataService(IHRMUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AdvanceBTypeData> GetAdvanceBTypeDataById(int id)
        {
            return await _unitOfWork.AdvanceBTypeDatas.GetLoanTypeDataById(id);
        }

        public async Task<IEnumerable<AdvanceBTypeData>> GetAllLoanTypeData()
        {
            return await _unitOfWork.AdvanceBTypeDatas.GetLoanTypeData();
        }
        public async Task<IEnumerable<AdvanceBTypeLedgerMapping>> GetAllAdvancedLedgerTypesMappingForSabha(int sabhaId )
        {
            return await _unitOfWork.AdvanceBTypeDatas.GetAllAdvancedLedgerTypesMappingForSabha(sabhaId);
        }

        public async Task<IEnumerable<AdvanceBTypeData>> GetAdvanceBTypeDataByAccountSystemVersionAndSabhaAsync(int accountsystemversionid, int sabhaid)
        {
            return await _unitOfWork.AdvanceBTypeDatas.GetLoanTypeDataByAccountSystemVersionAndSabhaAsync(accountsystemversionid, sabhaid);
        }

        public async Task<(bool, string?)> saveAdvancedBLoanVoteAssignment(SaveAdvancedBLoanLedgerTypeMapping saveAdvancedBLoanLedgerTypeMappingResource, HTokenClaim token)
        {
            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var vt = await _unitOfWork.VoteDetails.GetByIdAsync(saveAdvancedBLoanLedgerTypeMappingResource.LedgerAccountId);


                    if (vt == null)
                    {
                        throw new Exception("Ledger Account Not Found");
                    }

                    var newSpecialLedgerAccount = new AdvanceBTypeLedgerMapping
                    {
                        //  VoteId = vt.ID,
                        LedgerCode = vt.Code,
                        LedgerId = saveAdvancedBLoanLedgerTypeMappingResource.LedgerAccountId,
                        AdvanceBTypeId = saveAdvancedBLoanLedgerTypeMappingResource.LoanTypeId,
                        SabhaId = token.sabhaId,
                        StatusId = 1,
                        Prefix = "LN",
                        LastIndex = 1
                        //  CreatedBy = token.userId,
                        // CreatedAt = session.StartAt,
                        // SystemActionAt = DateTime.Now,

                    };

                    await _unitOfWork.AdvanceBTypeLedgerMapping.AddAsync(newSpecialLedgerAccount);
                    await _unitOfWork.CommitAsync();
                    return (true, "Advanced B Loan Ledger Account Assigned Successfully");

                }
                else
                {
                    throw new FieldAccessException("Active Session Not Found");
                }

            }
            catch (Exception ex)
            {
                if (ex is FinalAccountException or GeneralException)
                {
                    return (false, ex.Message);
                }
                else
                {
                    return (false, null);
                }



            }
        }
    }
}
