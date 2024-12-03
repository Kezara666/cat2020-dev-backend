using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Vote
{
    public class CustomVoteBalanceService : ICustomVoteBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomVoteBalanceService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateNewCustomVoteBalance(int customVoteId, Session session, HTokenClaim token)
        {



            try
            {
                var cvt = await _unitOfWork.CustomVoteDetails.GetWithVoteAssignmentById(customVoteId);

                if (cvt == null && cvt.voteAssignment != null)
                {
                    throw new GeneralException("Custom Vote Not Found");
                }

                var vt = await _unitOfWork.VoteDetails.GetByIdAsync(cvt.voteAssignment!.VoteId);

                if (vt == null || cvt.voteAssignment == null)
                {
                    throw new GeneralException("Vote Detail Not Found");

                }

                if (!await _unitOfWork.CustomVoteBalances.HasActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year)) { 

                    if (cvt.ParentId.HasValue)
                    {
                        var parencvtBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.ParentId!.Value, token.sabhaId, session.StartAt.Year);

                        if (parencvtBalance != null)
                        {
                            await AddCustomVoteBalance(vt, cvt, parencvtBalance, session, token);
                        }
                        else
                        {
                            await CreateNewCustomVoteBalance(cvt.ParentId!.Value, session, token);

                            var newparencvtBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.ParentId!.Value, token.sabhaId, session.StartAt.Year);

                            if (newparencvtBalance != null)
                            {
                                await AddCustomVoteBalance(vt, cvt, newparencvtBalance, session, token);
                            }
                            else
                            {
                                throw new GeneralException("Parent Custom Vote Balance Not Created");
                            }
                        }
                    }
                    else
                    {
                        await AddCustomVoteBalance(vt, cvt, null, session, token);
                    }

            }        

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        private async Task<CustomVoteBalance> AddCustomVoteBalance(VoteDetail vt, VoteAssignmentDetails cvt, CustomVoteBalance? parentCustomVoteBalance, Session session, HTokenClaim token)
        {
                        /*
            classification = 1 => income
            classification = 2 => expense
            classification = 3 => asset
            classification = 4 => liability
            classification = 5 => equity
            */


            var cvBalance = new CustomVoteBalance
            {
                CustomVoteDetailId = cvt.Id,
                SabhaId = token.sabhaId,
                Year = session.StartAt.Year,
                AllocationAmount = 0,
                ParentId = parentCustomVoteBalance != null ? parentCustomVoteBalance.Id : null,
                Depth = parentCustomVoteBalance != null ? parentCustomVoteBalance.Depth + 1 : 1,
                Credit = 0,
                ClassificationId = vt.ClassificationID,
                Debit = 0,
                CarryForwardDebit = 0,
                ChildrenCredit = 0,
                ExchangedAmount = 0,
                Status = CustomVoteBalanceStatus.Active,
                TransactionType = VoteBalanceTransactionTypes.Inti,
                CreatedAt = session.StartAt,
                UpdatedAt = session.StartAt,
                CreatedBy = token.userId,
                UpdatedBy = token.userId,
                SystemActionAt = DateTime.Now
            };
            await _unitOfWork.CustomVoteBalances.AddAsync(cvBalance);
            await _unitOfWork.CommitAsync();


            if (vt.ClassificationID == 1)
            {

            }
            else if (vt.ClassificationID == 2)
            {

            }
            else if (vt.ClassificationID == 3)
            {

            }
            else if (vt.ClassificationID == 4)
            {

            }
            else if (vt.ClassificationID == 5)
            {

            }

            else
            {
                throw new Exception("Vote Detail Not Have Classification");
            }


            await _unitOfWork.CommitAsync();

            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

            var cvtbActionLog = new CustomVoteBalanceActionLog
            {

                CustomVoteBalanceId = cvBalance.Id,
                ActionState = FinalAccountActionStates.Init,
                ActionBy = token.userId,
                Comment = "create",
                ActionDateTime = session.StartAt,
                SystemActionAt = DateTime.Now,
            };

            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
            await _unitOfWork.CommitAsync();
            return cvBalance;
        }

        public async Task UpdateCustomVoteBalance(int customVoteId,CashBookTransactionType cbTransaction, VoteBalanceTransactionTypes voteTransaction,Decimal amount,Session session, HTokenClaim token)
        {
            try
            {

                var cvt = await _unitOfWork.CustomVoteDetails.GetVoteAssignmentDetails(customVoteId);

                if (cvt != null)
                {
                    if (cvt.ParentId.HasValue)
                    {
                        var cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                        if (cvBalance != null)
                        {
                            if (cbTransaction == CashBookTransactionType.DEBIT)
                            {
                                cvBalance.TransactionType = voteTransaction;
                                //cvBalance.SubCode = mx.Code;


                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else if (cbTransaction== CashBookTransactionType.CREDIT)
                            {
                                cvBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Transaction Type Not Found");
                            }

                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            await CreateNewCustomVoteBalance(cvt.Id, session, token);

                            //var newCVBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);
                            cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                            if (cvBalance == null)
                            {
                                throw new GeneralException("Custom Vote Balance Not Created");
                            }

                            if (cbTransaction == CashBookTransactionType.DEBIT)
                            {
                                cvBalance.TransactionType = voteTransaction;
                                //cvBalance.SubCode = mx.Code;


                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else if (cbTransaction == CashBookTransactionType.CREDIT)
                            {
                                cvBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Transaction Type Not Found");
                            }

                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }


                        await UpdateCustomVoteBalance(cvt.ParentId!.Value, cbTransaction,voteTransaction, amount, session, token);


                    }
                    else
                    {

                        var cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                        if (cvBalance != null)
                        {
                            if (cbTransaction == CashBookTransactionType.DEBIT)
                            {
                                cvBalance.TransactionType = voteTransaction;
                                //cvBalance.SubCode = mx.Code;


                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else if (cbTransaction == CashBookTransactionType.CREDIT)
                            {
                                cvBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Transaction Type Not Found");
                            }

                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            await CreateNewCustomVoteBalance(cvt.Id, session, token);

                            //var newCVBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);
                             cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                            if (cvBalance == null)
                            {
                                throw new GeneralException("Custom Vote Balance Not Created");
                            }

                            if (cbTransaction == CashBookTransactionType.DEBIT)
                            {
                                cvBalance.TransactionType = voteTransaction;
                                //cvBalance.SubCode = mx.Code;


                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else if (cbTransaction == CashBookTransactionType.CREDIT)
                            {
                                cvBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (cvBalance.ClassificationId == 1)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 2)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 3)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else if (cvBalance.ClassificationId == 4)
                                {
                                    cvBalance.Debit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                                }
                                else if (cvBalance.ClassificationId == 5)
                                {
                                    cvBalance.Credit += (decimal)amount!;
                                    cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Transaction Type Not Found");
                            }

                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }

                    }
                }
                else
                {
                    throw new GeneralException("Custom Vote Not Found");

                }
            }catch(Exception ex)
            {
                throw;
            }

        }

        public async Task UpdateCustomVoteBalanceForOpenBalances(int customVoteId, FAMainTransactionMethod transactionMethod, VoteBalanceTransactionTypes voteTransaction, decimal Amount, Session session, HTokenClaim token)
        {
            try
            {

                var cvt = await _unitOfWork.CustomVoteDetails.GetVoteAssignmentDetails(customVoteId);

                if (cvt != null)
                {
                    if (cvt.ParentId.HasValue)
                    {
                        var cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                        if (cvBalance != null)
                        {
                            if (cvBalance.ClassificationId == 1)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Income Vote Balance");

                            }
                            else if (cvBalance.ClassificationId == 2)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Expense Vote Balance");
                            }
                            else if (cvBalance.ClassificationId == 3)
                            {


                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Debit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Credit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                                  
                                }

                                cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;
                            }
                            else if (cvBalance.ClassificationId == 4)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Credit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Debit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;

                                 

                                }

                                cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                            }
                            else if ( cvBalance.ClassificationId == 5)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Equity Vote Balance");
                            }
                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }

                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            await CreateNewCustomVoteBalance(cvt.Id, session, token);

                            var newCVBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                            if(newCVBalance == null)
                            {
                               throw new GeneralException("Custom Vote Balance Not Created");
                            }

                            if (newCVBalance.ClassificationId == 1)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Income Vote Balance");

                            }
                            else if (newCVBalance.ClassificationId == 2)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Expense Vote Balance");
                            }
                            else if (newCVBalance.ClassificationId == 3)
                            {


                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Debit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Credit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }

                                newCVBalance.CreditDebitRunningBalance = newCVBalance.Debit - newCVBalance.Credit;
                            }
                            else if (newCVBalance.ClassificationId == 4)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Credit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Debit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }

                                newCVBalance.CreditDebitRunningBalance = newCVBalance.Credit - newCVBalance.Debit;

                            }
                            else if (newCVBalance.ClassificationId == 5)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Equity Vote Balance");
                            }
                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }


                            newCVBalance.UpdatedAt = session.StartAt;
                            newCVBalance.UpdatedBy = token.userId;
                            newCVBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(newCVBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = newCVBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }


                        await UpdateCustomVoteBalanceForOpenBalances(cvt.ParentId!.Value, transactionMethod, voteTransaction, Amount, session, token);


                    }
                    else
                    {

                        var cvBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);

                        if (cvBalance != null)
                        {
                            if (cvBalance.ClassificationId == 1)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Income Vote Balance");

                            }
                            else if (cvBalance.ClassificationId == 2)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Expense Vote Balance");
                            }
                            else if (cvBalance.ClassificationId == 3)
                            {


                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Debit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Credit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }

                                cvBalance.CreditDebitRunningBalance = cvBalance.Debit - cvBalance.Credit;
                            }
                            else if (cvBalance.ClassificationId == 4)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Credit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    cvBalance.ExchangedAmount = Amount;
                                    cvBalance.Debit += cvBalance.ExchangedAmount;
                                    cvBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }

                                cvBalance.CreditDebitRunningBalance = cvBalance.Credit - cvBalance.Debit;

                            }
                            else if (cvBalance.ClassificationId == 5)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Equity Vote Balance");
                            }
                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }


                            cvBalance.UpdatedAt = session.StartAt;
                            cvBalance.UpdatedBy = token.userId;
                            cvBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(cvBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = cvBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            await CreateNewCustomVoteBalance(cvt.Id, session, token);

                            var newCVBalance = await _unitOfWork.CustomVoteBalances.GetActiveCustomVoteBalance(cvt.Id, token.sabhaId, session.StartAt.Year);
                            if (newCVBalance == null)
                            {
                                throw new GeneralException("Custom Vote Balance Not Created");
                            }

                            if (newCVBalance.ClassificationId == 1)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Income Vote Balance");

                            }
                            else if (newCVBalance.ClassificationId == 2)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Expense Vote Balance");
                            }
                            else if (newCVBalance.ClassificationId == 3)
                            {


                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Debit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Credit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }

                                newCVBalance.CreditDebitRunningBalance = newCVBalance.Debit - newCVBalance.Credit;
                            }
                            else if (newCVBalance.ClassificationId == 4)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Credit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    newCVBalance.ExchangedAmount = Amount;
                                    newCVBalance.Debit += newCVBalance.ExchangedAmount;
                                    newCVBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;



                                }

                                newCVBalance.CreditDebitRunningBalance = newCVBalance.Credit - newCVBalance.Debit;

                            }
                            else if (newCVBalance.ClassificationId == 5)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Equity Vote Balance");
                            }
                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }



                            newCVBalance.UpdatedAt = session.StartAt;
                            newCVBalance.UpdatedBy = token.userId;
                            newCVBalance.SystemActionAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();

                            var cvtbLog = _mapper.Map<CustomVoteBalance, CustomVoteBalanceLog>(newCVBalance);

                            var cvtbActionLog = new CustomVoteBalanceActionLog
                            {

                                CustomVoteBalanceId = newCVBalance.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "update",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };

                            await _unitOfWork.CustomVoteBalanceLogs.AddAsync(cvtbLog);
                            await _unitOfWork.CustomVoteBalanceActionLogs.AddAsync(cvtbActionLog);
                            await _unitOfWork.CommitAsync();
                        }

                    }
                }
                else
                {
                    throw new GeneralException("Custom Vote Not Found");

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}
