using CAT20.Core.Models.Enums.HRM;
using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.HelperModels;
using CAT20.Core.CustomExceptions;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using AutoMapper;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Core.Models.HRM;

namespace CAT20.Services.HRM.LoanManagement
{
    public class AdvanceBService : IAdvanceBService
    {
        private readonly IMapper _mapper;
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly IVoucherService _voucherService;

        public AdvanceBService(IMapper mapper, IHRMUnitOfWork unitOfWork,IVoteBalanceService voteBalanceService,IVoucherService voucherService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _voteBalanceService = voteBalanceService;
            _voucherService = voucherService;
        }

        public async Task<AdvanceB> GetLoanById(int id)
        {
            return await _unitOfWork.AdvanceBs.GetLoanById(id);
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoans()
        {
            return await _unitOfWork.AdvanceBs.GetAllLoans();
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansBySabha(int sabhhaid)
        {
            return await _unitOfWork.AdvanceBs.GetAllLoansBySabha(sabhhaid);
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansByOffice(int officeid)
        {
            return await _unitOfWork.AdvanceBs.GetAllLoansByOffice(officeid);
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansByEMPId(int empId)
        {
            return await _unitOfWork.AdvanceBs.GetAllLoansByEMPId(empId);
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansByEMPIdAndLoanTypeId(int empId, int loantypeid)
        {
            return await _unitOfWork.AdvanceBs.GetAllLoansByEMPIdAndLoanTypeId(empId, loantypeid);
        }

        public async Task<IEnumerable<AdvanceB>> GetAllNewLoansBySabhaId(int sabhaid)
        {
            return await _unitOfWork.AdvanceBs.GetAllNewLoansBySabhaId(sabhaid);
        }


        public async Task<(bool,string?,AdvanceB)> Create(AdvanceB advanceB, AdvanceBSettlement openSettlement,  HTokenClaim token)
        {
            try
            {
               

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                    advanceB.SabhaId = token.sabhaId;
                    advanceB.OfficeId = token.officeId;
                    advanceB.CreatedBy = token.userId;

                      

                        if (token.IsFinalAccountsEnabled == 1)
                        {

                            var vote = await _unitOfWork.VoteDetails.GetByIdAsync(advanceB.LedgerAccId);

                            if (vote == null)
                            {
                                throw new FinalAccountException("Unable To Found Vote");
                            }

                            var bankAccountId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteIdByOffice(vote.ID!.Value, token);

                            if (bankAccountId == 0)
                            {
                                throw new FinalAccountException("Unable To Found Bank Account");
                            }


                        advanceB.BankAccId = bankAccountId;
                        //newLoan.LoanNumber = await _unitOfWork.FinalAccountSequenceNumbers.GetNextLoanNumber(token.officeId, token.sabhaId, session.StartAt.Year);







                        if (advanceB.IsNew)
                        {

                            var docNumber = await _unitOfWork.HRMSequenceNumbers.GetNextSequenceNumberForYearSabhaModuleType(session.StartAt.Year, token.sabhaId, 2);

                            if (docNumber == null)
                            {
                                var newDocNumber = new HRMSequenceNumber
                                {
                                    SabhaId = token.sabhaId,
                                    Year = session.StartAt.Year,
                                    ModuleType = 2,
                                    Prefix="ADV-B/",
                                    LastIndex = 0
                                };
                                await _unitOfWork.HRMSequenceNumbers.AddAsync(newDocNumber);
                                await _unitOfWork.CommitAsync();

                                docNumber = newDocNumber;
                            }

                            if (docNumber != null) {

                                advanceB.AdvanceBNumber = docNumber.Prefix + (++docNumber.LastIndex).ToString();

                                 await _unitOfWork.AdvanceBs.AddAsync(advanceB);
                                 await _unitOfWork.CommitAsync();
                            }
                            else
                            {
                                throw new GeneralException("Unable To Create Advance-B Number");
                            }

                                if (advanceB.Id == 0)
                                {
                                    throw new FinalAccountException("Unable To Create Advance-B");

                                }

                                    var voucher =  await _voucherService.AdvancedBVoucher(advanceB, session,token);

                                if (voucher.Item1)
                                {
                                advanceB.VoucherId = voucher.Item3.Id;
                                    advanceB.VoucherNo = voucher.Item3.VoucherSequenceNumber;
                                }
                           

                            }
                            else
                            {

                                


                                advanceB.AdvanceBSettlements = new List<AdvanceBSettlement> { openSettlement };


                                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(advanceB.LedgerAccId, token.sabhaId, session.StartAt.Year);

                                if (voteBalance == null)
                                {
                                    voteBalance = await _voteBalanceService.CreateNewVoteBalance(advanceB.LedgerAccId, session, token);
                                    if (voteBalance == null)
                                    {
                                        throw new Exception("Unable to Create Vote Balance");
                                    }
                                }


                                voteBalance.ExchangedAmount = advanceB.Amount;
                                voteBalance.Debit += voteBalance.ExchangedAmount;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;


                                voteBalance.UpdatedBy = token.userId;
                                voteBalance.UpdatedAt = session.StartAt;
                                voteBalance.SystemActionAt = DateTime.Now;
                                voteBalance.OfficeId = token.officeId;
                                voteBalance.SabhaId = token.sabhaId;
                                voteBalance.SessionIdByOffice = session.Id;


                                voteBalance.Year = session.StartAt.Year;
                                voteBalance.Code = "EMP LOAN O/B";
                                voteBalance.SubCode = advanceB.AdvanceBNumber;

                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);


                                var vtbActionLog = new VoteBalanceActionLog
                                {

                                    VoteBalanceId = voteBalance.Id,
                                    ActionState = FinalAccountActionStates.Init,
                                    ActionBy = token.userId,
                                    Comment = "create",
                                    ActionDateTime = session.StartAt,
                                    SystemActionAt = DateTime.Now,
                                };

                                vtbLog.Id = null;
                                vtbLog.VoteBalanceId = voteBalance.Id;


                                var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                                var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                                if (accumulatedFundBalance == null)
                                {
                                    accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                                }

                                /******************/

                                if (accumulatedFund != null)
                                {

                                    accumulatedFundBalance.Credit += (decimal)voteBalance.ExchangedAmount!;
                                    accumulatedFundBalance.UpdatedBy = token.userId;
                                    accumulatedFundBalance.UpdatedAt = session.StartAt;
                                    accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                    accumulatedFundBalance.ExchangedAmount = (decimal)voteBalance.ExchangedAmount!;

                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                    //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                    accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                    /*vote balance log */
                                    var vtbLogAccumulatedFund = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                    accumulatedFundBalance.ExchangedAmount = 0;

                                    vtbLogAccumulatedFund.Year = session.StartAt.Year;
                                    vtbLogAccumulatedFund.Code = "ADVANCE-B LOAN O/B";
                                    vtbLogAccumulatedFund.SubCode = advanceB.AdvanceBNumber;

                                    vtbLogAccumulatedFund.OfficeId = token.officeId;
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLogAccumulatedFund);



                                    /**********/

                                }
                                else
                                {
                                    throw new FinalAccountException("Vote Balance Not Found For Accumulated Fund");
                                }

                            /******************/

                          

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                            }
                        await _unitOfWork.CommitAsync();

                        return (true, "Save Advance B Sucessfully", advanceB);
                    }
                    else
                    {
                        throw new FinalAccountException("No Final Account Module Enabled");
                    }



                    }
                    else
                    {
                        throw new FinalAccountException("No Active Session Found");
                    }
                
               
            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null,null);
            }
        }
        public Task<(bool, string?, AdvanceB)> UpdateAdvanceB(AdvanceB LoanToUpdate, HTokenClaim token)
        {
            try
            {
                //if (Enum.IsDefined(typeof(LoanPaymentMethod), LoanToUpdate.LoanPaymentMethod))
                //{
                //    LoanToUpdate.UpdatedBy = token.userId;
                //    LoanToUpdate.UpdatedAt = System.DateTime.Now;

                //    //_unitOfWork.Loans.Update(LoanToUpdate);
                //    await _unitOfWork.CommitAsync();

                //return LoanToUpdate;
                //}
                //else
                //{
                //    throw new Exception("Invalid field");
                //}

                throw new Exception("Error updating Loan");
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Loan", ex);
            }
        }

        public Task<IEnumerable<AdvanceB>> GetAllAdvanceBForSettlementSabhaId(int sabhaid, HTokenClaim token)
        {
           return _unitOfWork.AdvanceBs.GetAllAdvanceBForSettlementSabhaId(sabhaid, token);
        }

       
    }
}
