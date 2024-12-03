using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using AutoMapper;
using CAT20.Core.CustomExceptions;
using CAT20.Core.Models.Control;

namespace CAT20.Services.Vote
{
    public class AccountBalanceDetailService : IAccountBalanceDetailService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly IMapper _mapper;

        public AccountBalanceDetailService(IVoteUnitOfWork unitOfWork,IVoteBalanceService voteBalanceService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _voteBalanceService = voteBalanceService;
          _mapper = mapper;
        }
        public async Task<(bool, string?)> CreateAccountBalanceDetail(AccountBalanceDetail newAccountBalanceDetail, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {


                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    var accountDetail = await _unitOfWork.AccountDetails.GetByIdAsync(newAccountBalanceDetail.AccountDetailID);

                    var accountBalanceDetails = await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailByAccountDetailIdSabhaIdAsync(newAccountBalanceDetail.AccountDetailID, token.sabhaId);


                    if (accountBalanceDetails.Count() > 0)
                    {
                        throw new FinalAccountException("Balance Already Created");
                    }

                    if (session != null)
                    {
                        if (accountDetail != null)
                        {

                            accountDetail.RunningBalance = newAccountBalanceDetail.BalanceAmount;

                            await _unitOfWork.AccountBalanceDetails
                          .AddAsync(newAccountBalanceDetail);

                            if (!accountDetail.VoteId.HasValue)
                            {

                                throw new FinalAccountException("Assign Ledger Account That Related To Bank Account");

                            }
                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accountDetail.VoteId!.Value, token.sabhaId, session.StartAt.Year);
                            //var vote = await _unitOfWork.VoteDetails.GetByIdAsync(accountTransfer.FromVoteDetailId);

                            var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                            if(accumulatedFund == null)
                            {
                                throw new FinalAccountException("Accumulated Ledger Fund Ledger Account Not Found");
                            }

                            var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                            if (accumulatedFundBalance == null)
                            {
                                accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                            }

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Credit += accountDetail.RunningBalance;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = accountDetail.RunningBalance;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */

                                accumulatedFundBalance.UpdatedAt =session.StartAt;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;

                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ACC CB O/B";
                                vtbLog.SubCode = accountDetail.AccountNo;

                                vtbLog.OfficeId = token.officeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                                /**********/

                            }
                            else
                            {
                                throw new FinalAccountException("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/


                            if (voteBalance == null)
                            {

                                voteBalance = await _voteBalanceService.CreateNewVoteBalance(accountDetail.VoteId.Value, session, token);
                                if (voteBalance == null)
                                {
                                    throw new FinalAccountException("Unable to Create Vote Balance");
                                }

                                voteBalance.Debit += accountDetail.RunningBalance;
                                voteBalance.ExchangedAmount = accountDetail.RunningBalance;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalance.UpdatedBy = token.userId;
                                voteBalance.UpdatedAt = session.StartAt;
                                voteBalance.SystemActionAt = DateTime.Now;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);


                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                            }


                            CashBook cashBook = new CashBook
                            {
                                SabhaId = token.sabhaId,
                                OfiiceId = token.officeId,
                                SessionId = session.Id,
                                BankAccountId = newAccountBalanceDetail.AccountDetailID,
                                Description = "Initial B/F",

                                Date = session.StartAt,
                                TransactionType = CAT20.Core.Models.Enums.CashBookTransactionType.DEBIT,
                                IncomeCategory = CAT20.Core.Models.Enums.CashBookIncomeCategory.Inti,
                                DirectAmount = newAccountBalanceDetail.BalanceAmount,
                                RunningTotal = newAccountBalanceDetail.BalanceAmount,
                                RowStatus = 1,
                                CreatedAt = DateTime.Now,
                                CreatedBy = token.userId,
                            };



                            await _unitOfWork.CashBook.AddAsync(cashBook);

                            await _unitOfWork.CommitAsync();
                            transaction.Commit();
                            return (true, "Balance Create Successfully");
                        }
                        else
                        {
                            throw new Exception("Account Detail not found");
                        }
                    }
                    else
                    {
                        throw new Exception("No Active session found");
                    }

                   
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    if (e.GetType() == typeof(FinalAccountException))
                    {
                        return (false, e.Message);
                    }
                    else
                    {
                        return (false, null);

                    }
                }
            }
        }
        public async Task DeleteAccountBalanceDetail(AccountBalanceDetail accountBalanceDetail)
        {
            //_unitOfWork.AccountBalanceDetails.Remove(accountBalanceDetail);

            //await _unitOfWork.CommitAsync();

            //_unitOfWork.Programmes.Remove(programme);
            accountBalanceDetail.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<AccountBalanceDetail>> GetAllAccountBalanceDetails()
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllAsync();
        }
        public async Task<AccountBalanceDetail> GetAccountBalanceDetailById(int id)
        {
            return await _unitOfWork.AccountBalanceDetails.GetByIdAsync(id);
        }
        public async Task UpdateAccountBalanceDetail(AccountBalanceDetail accountBalanceDetailToBeUpdated, AccountBalanceDetail accountBalanceDetail)
        {
            //accountBalanceDetailToBeUpdated.Name = accountBalanceDetail.t;

            accountBalanceDetailToBeUpdated.BalanceAmount = accountBalanceDetail.BalanceAmount;
            accountBalanceDetailToBeUpdated.Year = accountBalanceDetail.Year;

            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailId(int Id)
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailByAccountDetailIdAsync(Id);
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailBySabhaId(int Id)
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailBySabhaIdAsync(Id);
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdSabhaId(int AccountDetailId, int SabhaId)
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailByAccountDetailIdSabhaIdAsync(AccountDetailId, SabhaId);
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailsBySabhaId( int SabhaId)
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailBySabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountId(int AccountId)
        {
            return await _unitOfWork.AccountBalanceDetails.GetAllWithAccountBalanceDetailByAccountIdAsync(AccountId);
        }

        
    }
}