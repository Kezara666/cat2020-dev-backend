using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.WaterBilling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class MixFinalAccountCorrectionService : IMixFinalAccountCorrectionService
    {
        private readonly ILogger<MixinOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IMixinVoteBalanceService _mixVoteBalanceService;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IAssessmentCancelOrderService _assessmentCancelOrderService;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IWaterConnectionBalanceService _waterConnectionBalanceService;
        private readonly IWaterBillCancelOrderService _waterBillCancelOrderService;

        private readonly IShopRentalProcessPaymentService _shopRentalProcessPaymentService; 
        private readonly IShopRentalCancelOrderService _shopRentalCancelOrderService; 

        public MixFinalAccountCorrectionService(ILogger<MixinOrderService> logger, IMapper mapper, IMixinUnitOfWork unitOfWork, IMixinVoteBalanceService voteBalanceService, IAssessmentBalanceService assessmentBalanceService, IAssessmentCancelOrderService assessmentCancelOrderService, IVoteAssignmentService voteAssignmentService, IWaterConnectionBalanceService waterConnectionBalanceService, IWaterBillCancelOrderService waterBillCancelOrderService, IShopRentalProcessPaymentService shopRentalProcessPaymentService, IShopRentalCancelOrderService shopRentalCancelOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mixVoteBalanceService = voteBalanceService;
            _assessmentBalanceService = assessmentBalanceService;
            _assessmentCancelOrderService = assessmentCancelOrderService;
            _voteAssignmentService = voteAssignmentService;
            _waterConnectionBalanceService = waterConnectionBalanceService;
            _waterBillCancelOrderService = waterBillCancelOrderService;

            _shopRentalProcessPaymentService = shopRentalProcessPaymentService;
            _shopRentalCancelOrderService = shopRentalCancelOrderService;
        }

        public async Task<(bool, string?)> AlignLedgerAccountAndCashBooksForOlderReceipts(int month, HTokenClaim token)
        {
            try
            {
               if (token.IsFinalAccountsEnabled == 1){
                    var activeSeetion = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (activeSeetion != null)
                    {
                        var officeIds = await _unitOfWork.Offices.GetAllWithOfficeIdsBySabhaIdAsync(token.sabhaId);

                        var mnthlyRecipts = await _unitOfWork.MixinOrders.GetForLegerAccountUpdate(month, officeIds.ToList());

                        foreach (var item in mnthlyRecipts)
                        {
                                var session = await _unitOfWork.Sessions.GetByIdAsync(item.SessionId);

                            if (await UpdateVoteBalance(item, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                            {
                                //await _unitOfWork.CommitAsync();
                            }
                            else
                            {
                                throw new FinalAccountException("Vote Balance Entry Not updated.");
                            }



                            if (await CreateCashBookEntry(item, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, session, token))
                            {
                                //await _unitOfWork.CommitAsync();
                            }
                            else
                            {
                                throw new FinalAccountException("Cashbook Entry Not Created.");
                            }
                        }



                        await _unitOfWork.CommitAsync();
                        return (true, "Cash Book And Ledger Account update Successfully");
                    }
                    else
                    {
                        throw new FinalAccountException("No active session found for the office");
                    }
                }
                else
                {
                    throw new FinalAccountException("Final Account Is Not Enabled For This Sabha");
                }

            }
            catch(Exception ex) 
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }

        private async Task<bool> UpdateVoteBalance(MixinOrder mx, Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)

        {
            try
            {
                if (mx.MixinOrderLine.Count == 0)
                {
                    throw new FinalAccountException("No Order Line Found.");
                }


                foreach (var item in mx.MixinOrderLine)
                {


                    if (item.StampAmount > 0)
                    {
                        var stampLedgerAccount = await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                        var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampLedgerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                        if (stampVoteBalance == null)
                        {
                            stampVoteBalance = await _mixVoteBalanceService.CreateNewVoteBalance(stampLedgerAccount.VoteId!.Value, session, token);

                            if (stampVoteBalance == null)
                            {
                                throw new FinalAccountException("Unable to Create Stamp Ledger Account Entry");
                            }


                        }


                        if (stampVoteBalance != null && stampVoteBalance.Id.HasValue)
                        {
                            stampVoteBalance.UpdatedAt = session.StartAt;
                            stampVoteBalance.UpdatedBy = token.userId;
                            stampVoteBalance.SystemActionAt = DateTime.Now;
                            stampVoteBalance.OfficeId = token.officeId;
                            stampVoteBalance.ExchangedAmount = (decimal)item.StampAmount!;
                            stampVoteBalance.SessionIdByOffice = session.Id;


                            if (transactionType == CashBookTransactionType.DEBIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                stampVoteBalance.SubCode = mx.Code;


                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }

                            }



                            else if (transactionType == CashBookTransactionType.CREDIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }




                        }
                        else
                        {
                            throw new FinalAccountException("Vote stamp ledger balance not found.");
                        }
                    }


                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteDetailId!.Value, token.sabhaId, session.StartAt.Year);

                    if (voteBalance == null)
                    {
                        voteBalance = await _mixVoteBalanceService.CreateNewVoteBalance(item.VoteDetailId!.Value, session, token);

                        if (voteBalance == null)
                        {
                            throw new FinalAccountException("Unable to Create Vote Balance Entry");
                        }


                    }


                    if (voteBalance != null && voteBalance.Id.HasValue)
                    {
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.UpdatedBy = token.userId;


                        if (transactionType == CashBookTransactionType.DEBIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                            voteBalance.SubCode = mx.Code;


                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }

                        }



                        else if (transactionType == CashBookTransactionType.CREDIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }
                        }


                        if (voteBalance.ClassificationId == 2)
                        {
                            voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! + voteBalance.Debit - voteBalance.Credit;
                        }

                        voteBalance.ExchangedAmount = (decimal)item.Amount!;

                        voteBalance.OfficeId = token.officeId;
                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.SystemActionAt = DateTime.Now;

                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                        if (transactionType == CashBookTransactionType.DEBIT && item.ClassificationId == 4)
                        {



                            var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                            if (IncomeSubtitleId.HasValue)
                            {

                                var newDeposit = new Deposit
                                {
                                    DepositSubCategoryId = IncomeSubtitleId.Value,
                                    InitialDepositAmount = (decimal)item.Amount!,
                                    DepositDate = session.StartAt,
                                    Description = item.Description,
                                    CreatedAt = session.StartAt,
                                    CreatedBy = token.userId,
                                    SabhaId = token.sabhaId,
                                    OfficeId = token.officeId,
                                    LedgerAccountId = voteBalance.VoteDetailId.Value!,
                                    MixOrderId = mx.Id,
                                    MixOrderLineId = item.Id,
                                    ReceiptNo = mx.Code,
                                    IsEditable = false,
                                    PartnerId = mx.PartnerId!.Value,
                                    SystemCreateAt = DateTime.Now,

                                };

                                await _unitOfWork.Deposits.AddAsync(newDeposit);

                            }
                        }

                        //else if(transactionType == CashBookTransactionType.CREDIT)
                        //{
                        //    var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                        //    if (IncomeSubtitleId.HasValue)
                        //    {



                        //    }
                        //}


                    }
                    else
                    {
                        throw new FinalAccountException("Vote balance not found.");
                    }



                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    var depostis = await _unitOfWork.Deposits.ClearDepots(mx.Id);

                    foreach (var item in depostis)
                    {
                        item.Status = 0;
                        item.UpdatedAt = session.StartAt;
                        item.UpdatedBy = token.userId;
                        item.SystemUpdateAt = DateTime.Now;

                    }
                }



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private async Task<bool> CreateCashBookEntry(MixinOrder mx, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, Session session, HTokenClaim token)
        {

            try
            {
                var account = await _unitOfWork.AccountDetails.GetByIdAsync(mx.AccountDetailId!.Value);
                //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

                if (account == null)
                {
                    throw new FinalAccountException("Account not found.");
                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    account.RunningBalance += mx.TotalAmount;
                }
                else if (transactionType == CashBookTransactionType.CREDIT)
                {

                    account.RunningBalance -= mx.TotalAmount;
                }
                var cashbook = new CashBook
                {
                    //Id
                    SabhaId = token.sabhaId,
                    OfiiceId = mx.OfficeId!.Value,
                    SessionId = mx.SessionId,
                    BankAccountId = mx.AccountDetailId.Value,
                    Date = mx.CreatedAt,

                    TransactionType = transactionType,
                    IncomeCategory = incomeCategory,
                    Code = mx.Code,
                    IncomeItemId = mx.Id,


                    CreatedAt = DateTime.Now,
                    CreatedBy = token.userId,
                    RunningTotal = account.RunningBalance,


                };

                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (voteBalance != null)
                {

                    if (transactionType == CashBookTransactionType.DEBIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                        voteBalance.Debit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }
                    else if (transactionType == CashBookTransactionType.CREDIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;
                        voteBalance.Credit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }


                    voteBalance.ExchangedAmount = mx.TotalAmount;

                    voteBalance.OfficeId = token.officeId;
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                }
                else
                {
                    throw new Exception("Unable to Find Vote Balance");
                }



                //1 = cash
                //2 = cheque
                //3 cross
                //4 = direct
                if (mx.PaymentMethodId == 1)
                {
                    cashbook.CashAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cash;
                }
                else if (mx.PaymentMethodId == 2)
                {
                    cashbook.ChequeAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque;
                    cashbook.ChequeNo = mx.ChequeNumber;

                }
                else if (mx.PaymentMethodId == 3)
                {
                    cashbook.CrossAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cross;
                }
                else if (mx.PaymentMethodId == 4)
                {
                    cashbook.DirectAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Direct;
                }


                await _unitOfWork.CashBook.AddAsync(cashbook);

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}
