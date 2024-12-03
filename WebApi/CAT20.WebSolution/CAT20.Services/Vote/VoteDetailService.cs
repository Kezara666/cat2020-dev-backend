using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Core.Models.Control;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;

namespace CAT20.Services.Vote
{
    public class VoteDetailService : IVoteDetailService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public VoteDetailService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool, string?, VoteDetail)> CreateVoteDetail(VoteDetail newVoteDetail,HTokenClaim token)
        {
            try
            {


                if (!await _unitOfWork.VoteDetails.HasVoteDetailExist(newVoteDetail.Code,token)) {
                    await _unitOfWork.VoteDetails
                    .AddAsync(newVoteDetail);
                    await _unitOfWork.CommitAsync();

                    return (true, "Vote Details Create Successful", newVoteDetail);
                }
                else
                {
                    throw new FinalAccountException("Vote Details Code Already Exist");
                }
                }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null,new VoteDetail());
            }
            
        }
        public async Task DeleteVoteDetail(VoteDetail voteDetail)
        {
            voteDetail.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetails()
        {
            return await _unitOfWork.VoteDetails.GetAllAsync();
        }
        public async Task<VoteDetail> GetVoteDetailById(int id)
        {
            return await _unitOfWork.VoteDetails.GetByIdAsync(id);
        }
        public async Task UpdateVoteDetail(VoteDetail voteDetailToBeUpdated, VoteDetail voteDetail)
        {
            voteDetailToBeUpdated.Code = voteDetail.Code;
            voteDetailToBeUpdated.NameSinhala = voteDetail.NameSinhala;
            voteDetailToBeUpdated.NameEnglish = voteDetail.NameEnglish;
            voteDetailToBeUpdated.NameTamil = voteDetail.NameTamil;
            voteDetailToBeUpdated.VoteOrder = voteDetail.VoteOrder;
            voteDetailToBeUpdated.ProgrammeID = voteDetail.ProgrammeID;
            voteDetailToBeUpdated.ProgrammeNameSinhala = voteDetail.ProgrammeNameSinhala;
            voteDetailToBeUpdated.ProgrammeNameEnglish = voteDetail.ProgrammeNameEnglish;
            voteDetailToBeUpdated.ProgrammeNameTamil = voteDetail.ProgrammeNameTamil;
            voteDetailToBeUpdated.ProgrammeCode = voteDetail.ProgrammeCode;
            voteDetailToBeUpdated.ProjectID = voteDetail.ProjectID;
            voteDetailToBeUpdated.ProjectNameSinhala = voteDetail.ProjectNameSinhala;
            voteDetailToBeUpdated.ProjectNameEnglish = voteDetail.ProjectNameEnglish;
            voteDetailToBeUpdated.ProjectNameTamil = voteDetail.ProjectNameTamil;
            voteDetailToBeUpdated.ProjectCode = voteDetail.ProjectCode;
            voteDetailToBeUpdated.SubprojectID = voteDetail.SubprojectID;
            voteDetailToBeUpdated.SubprojectNameSinhala = voteDetail.SubprojectNameSinhala;
            voteDetailToBeUpdated.SubprojectNameEnglish = voteDetail.SubprojectNameEnglish;
            voteDetailToBeUpdated.SubprojectNameTamil = voteDetail.SubprojectNameTamil;
            voteDetailToBeUpdated.SubprojectCode = voteDetail.SubprojectCode;
            voteDetailToBeUpdated.IncomeTitleID = voteDetail.IncomeTitleID;
            voteDetailToBeUpdated.IncomeTitleNameSinhala = voteDetail.IncomeTitleNameSinhala;
            voteDetailToBeUpdated.IncomeTitleNameEnglish = voteDetail.IncomeTitleNameEnglish;
            voteDetailToBeUpdated.IncomeTitleNameTamil = voteDetail.IncomeTitleNameTamil;
            voteDetailToBeUpdated.IncomeTitleCode = voteDetail.IncomeTitleCode;
            voteDetailToBeUpdated.IncomeSubtitleID = voteDetail.IncomeSubtitleID;
            voteDetailToBeUpdated.IncomeSubtitleNameSinhala = voteDetail.IncomeSubtitleNameSinhala;
            voteDetailToBeUpdated.IncomeSubtitleNameEnglish = voteDetail.IncomeSubtitleNameEnglish;
            voteDetailToBeUpdated.IncomeSubtitleNameTamil = voteDetail.IncomeSubtitleNameTamil;
            voteDetailToBeUpdated.IncomeSubtitleCode = voteDetail.IncomeSubtitleCode;
            voteDetailToBeUpdated.IncomeOrExpense = voteDetail.IncomeOrExpense;
            voteDetailToBeUpdated.VoteOrBal = voteDetail.IncomeOrExpense;
            voteDetailToBeUpdated.ClassificationID = voteDetail.ClassificationID;
            voteDetailToBeUpdated.ClasssificationDescription = voteDetail.ClasssificationDescription;
            voteDetailToBeUpdated.MainLedgerAccountID = voteDetail.MainLedgerAccountID;
            voteDetailToBeUpdated.MainLedgerAccountDescription = voteDetail.MainLedgerAccountDescription;
            voteDetailToBeUpdated.UpdatedBy = voteDetail.UpdatedBy;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailBySabhaId(int SabhaId)
        {
            return await _unitOfWork.VoteDetails.GetAllWithVoteDetailBySabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeId(int ProgrammeId)
        {
            return await _unitOfWork.VoteDetails.GetAllWithVoteDetailByProgrammeIdAsync(ProgrammeId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByIncomeSubTitleId(int IncomeSubTitleId)
        {
            return await _unitOfWork.VoteDetails.GetAllWithVoteDetailByIncomeSubTitleIdAsync(IncomeSubTitleId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdandSabhaId(int ProgrammeId, int SabhaId)
        {
            return await _unitOfWork.VoteDetails.GetAllWithVoteDetailByProgrammeIdandSabhaIdAsync(ProgrammeId, SabhaId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsForSabhaId(int SabhaId)
        {
            return await _unitOfWork.VoteDetails.GetAllVoteDetailsForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsProgammesByClassificationId(int ClassificationId)
        {
            return await _unitOfWork.VoteDetails.GetAllVoteDetailsProgammesByClassificationIdAsync(ClassificationId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(int ProgrammeId, int ClassificationId, int MainLedgerAccId, int LedgerCategoryId, int LedgerAccountId, int SabhaId)
        {
            return await _unitOfWork.VoteDetails.GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(ProgrammeId, ClassificationId, MainLedgerAccId, LedgerCategoryId, LedgerAccountId, SabhaId);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllLedgerAccountsForDepositSubCategoryId(int depositSubCategoryId, int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetAllLedgerAccountsForDepositSubCategoryId(depositSubCategoryId, sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllImprestLedgerAccountsForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetAllImprestLedgerAccountsForSabhaId(sabhaid);
        }
        //new route final account
        public async Task<IEnumerable<VoteDetail>> GetLAbankLoanLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetLAbankLoanLeggerAccountForSabhaId(sabhaid);
        }


        public async Task<IEnumerable<VoteDetail>> GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(sabhaid);
        }


        public async Task<IEnumerable<VoteDetail>> GetIndustrialCreditorsLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetIndustrialCreditorsLeggerAccountForSabhaId(sabhaid);
        }


        public async Task<IEnumerable<VoteDetail>> GetFixAssestsForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetFixAssestsForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetFixAssests2ForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetFixAssests2ForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetIndustrialDebitorsLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetIndustrialDebitorsLeggerAccountForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetStoresAdvanceAccountsLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetStoresAdvanceAccountsLeggerAccountForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetPrepayableLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetPrepayableLeggerAccountForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetFixedDepositLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetFixedDepositLeggerAccountForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetReceivableExchangeLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetReceivableExchangeLeggerAccountForSabhaId(sabhaid);
        }


        public async Task<IEnumerable<VoteDetail>> GetReceivableNonExchangeLeggerAccountForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetReceivableNonExchangeLeggerAccountForSabhaId(sabhaid);
        }

        //.................//

        public async Task<IEnumerable<VoteDetail>> GetAllAccountTransferLedgerAccountsForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetAllAccountTransferLedgerAccountsForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> GetAllShopRentalExpectedIncomeAccountsForSabhaId(int sabhaid)
        {
            try
            {
                return await _unitOfWork.VoteDetails.GetAllShopRentalExpectedIncomeAccountsForSabhaId(sabhaid);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<VoteDetail>> GetPayRollAccountsForSabha(int SabhaId)
        {
           return _unitOfWork.VoteDetails.GetPayRollAccountsForSabha(SabhaId);
        }

        public Task<IEnumerable<VoteDetail>> GetStoreAdvanceAssetsAccountsForSabha(int SabhaId)
        {
            return _unitOfWork.VoteDetails.GetStoreAdvanceAssetsAccountsForSabha(SabhaId);
        }


        public Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType)
        {
            return _unitOfWork.VoteDetails.GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(sabhaId,isCreditor,isDebtor, creditorsDebtorsType);
        }

        public Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType)
        {
            return _unitOfWork.VoteDetails.GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(sabhaId, isCreditor, isDebtor, creditorsDebtorsType);
        }

        public async Task<IEnumerable<VoteDetail>> GetSingleOpeningBalanceLedgerAccount(int sabhaid)
        {
            return await _unitOfWork.VoteDetails.GetSingleOpeningBalanceLedgerAccount(sabhaid);
        }

        public async Task<IEnumerable<VoteDetail>> getVoteDetalByFilterValues(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int?  projectId,int? subProject,int SabhaId)
        {
            return await _unitOfWork.VoteDetails.getVoteDetalByFilterValues(programmeId, classificationId, mainLedgerAccId, ledgerCategoryId, ledgerAccountId, subLedgerId, projectId, subProject, SabhaId);
        }

        public async Task<IEnumerable<VoteDetail>> getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int sabhaId, HTokenClaim token)
        {
            try
            {
                var activeSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (activeSession != null)
                {
                    return await _unitOfWork.VoteDetails.getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(programmeId, classificationId, mainLedgerAccId, ledgerCategoryId, ledgerAccountId, subLedgerId, projectId, subProject, sabhaId, activeSession.StartAt.Year, 1);
                }
                else
                {
                    throw new GeneralException("No Acive Session");
                }
            }
            catch
            {
                throw;

            }
        }
    }
}