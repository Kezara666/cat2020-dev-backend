using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;
using CAT20.WebApi.Resources.Final;
using CAT20.WebApi.Resources.FInalAccount.Save;
using Irony.Parsing;
using Newtonsoft.Json.Linq;
using System;


namespace CAT20.Services.FinalAccount
{

    public class DepositService : IDepositService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;

        public DepositService(IVoteUnitOfWork unitOfWork,IMapper mapper,IVoteBalanceService voteBalanceService, ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }
       
    

        public async Task<(bool,string?)> CreateUpdateDeposit(SaveDepositResource newDepositResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var newDeposit = _mapper.Map<SaveDepositResource, Deposit>(newDepositResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newDepositResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        if (newDeposit.Id != null)
                        {


                            var deposit = await _unitOfWork.Deposits.GetByIdAsync(newDeposit.Id!.Value);
                            if (deposit != null)
                            {

                                deposit.DepositSubCategoryId = newDeposit.DepositSubCategoryId;
                                deposit.LedgerAccountId = newDeposit.LedgerAccountId;
                                deposit.DepositDate = newDeposit.DepositDate;
                                deposit.ReceiptNo = newDeposit.ReceiptNo;
                                deposit.Description = newDeposit.Description;
                                deposit.PartnerId = newDeposit.PartnerId;
                                deposit.InitialDepositAmount = newDeposit.InitialDepositAmount;
                                deposit.ReleasedAmount = newDeposit.ReleasedAmount;
                                newDeposit.IsEditable = true;

                            }
                            else
                            {
                                throw new Exception("Unable To Found");
                            }
                        }
                        else
                        {

                            newDeposit.Id = null;
                            newDeposit.CreatedAt = session.StartAt;
                            newDeposit.CreatedBy = token.userId;
                            newDeposit.SabhaId = token.sabhaId;
                            newDeposit.OfficeId = token.officeId;
                            newDeposit.SystemCreateAt = DateTime.Now;
                            newDeposit.IsEditable = true;
                            await _unitOfWork.Deposits.AddAsync(newDeposit);

                        }


                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(newDeposit.LedgerAccountId, token.sabhaId, session.StartAt.Year);
                        if(voteBalance== null)
                        {
                            voteBalance = await _voteBalanceService.CreateNewVoteBalance(newDeposit.LedgerAccountId, session, token);
                            if(voteBalance == null)
                            {
                                throw new Exception("Unable to Create Vote Balance");
                            }   
                        }


                        voteBalance.ExchangedAmount = newDeposit.InitialDepositAmount - newDeposit.ReleasedAmount;
                        voteBalance.Credit += voteBalance.ExchangedAmount;
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Credit-voteBalance.Debit;


                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.SystemActionAt = DateTime.Now;
                        voteBalance.OfficeId = token.officeId;
                        voteBalance.SabhaId = token.sabhaId;
                        voteBalance.Code= "DEP O/B";
                        voteBalance.SubCode = newDeposit.ReceiptNo;
                        voteBalance.SessionIdByOffice = session.Id;

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

                            accumulatedFundBalance.Debit += (decimal)voteBalance.ExchangedAmount!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)voteBalance.ExchangedAmount!; 

                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLogAccumulatedFund = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            //vtbLogAccumulatedFund.Year = session.StartAt.Year;
                            vtbLogAccumulatedFund.Code = "DEP O/B";
                            vtbLogAccumulatedFund.SubCode = newDeposit.ReceiptNo;

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
                        await _unitOfWork.CommitAsync();
                                             

                            await _customVoteBalanceService.UpdateCustomVoteBalance(newDeposit.CustomVoteId!.Value, CashBookTransactionType.DEBIT, VoteBalanceTransactionTypes.Credit, (newDeposit.InitialDepositAmount - newDeposit.ReleasedAmount) , session, token);
                      

                        transaction.Commit();



                        return (true, "Successfully Saved");
                    }
                    else
                    {
                        throw new Exception("No Active Session Found");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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

        public  async     Task<(int totalCount, IEnumerable<DepositResource> list)> GetAllDepositsForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId , Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword,HTokenClaim token)
        {
            var deposits = await _unitOfWork.Deposits.GetAllDepositsForSabha(sabhaId,excludedIds, depositSubCategoryId, ledgerAccountId, pageNo,pageSize,filterKeyword);

            var depositsRescouce = _mapper.Map<IEnumerable<Deposit>, IEnumerable<DepositResource>>(deposits.list);

            foreach(var deposit in depositsRescouce)
            {
                
                deposit.Partner = _mapper.Map<Partner, VendorResource>(await _partnerService.GetById(deposit.PartnerId));
                deposit.AccountId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteId(deposit.LedgerAccountId, token);
            }

            return (deposits.totalCount, depositsRescouce);

        }

        public async Task<(int totalCount, IEnumerable<DepositResource> list)> GetAllDepositsToRepaymentForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var deposits = await _unitOfWork.Deposits.GetAllDepositsToRepaymentForSabha(sabhaId, excludedIds, depositSubCategoryId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var depositsRescouce = _mapper.Map<IEnumerable<Deposit>, IEnumerable<DepositResource>>(deposits.list);

            foreach (var deposit in depositsRescouce)
            {

                deposit.Partner = _mapper.Map<Partner, VendorResource>(await _partnerService.GetById(deposit.PartnerId));
                deposit.AccountId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteId(deposit.LedgerAccountId, token);
            }

            return (deposits.totalCount, depositsRescouce);

        }


        public async Task<Deposit> GetDepositById(int Id)
        {
            return await _unitOfWork.Deposits.GetByIdAsync(Id);
        }

        //public async Task DeleteDeposit(Deposit deposit)
        //{
        //    _unitOfWork.Deposit.Remove(deposit);

        //    await _unitOfWork.CommitAsync();
        //}
        public async Task<bool> DeleteDeposit(int depositId,HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    var deposit = await _unitOfWork.Deposits.GetByIdAsync(depositId);
                    if (session != null)
                    {
                        if (deposit != null)
                        {

                            deposit.Status = 0;
                           
                            
                    
                        


                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(deposit.LedgerAccountId, token.sabhaId, session.StartAt.Year);
                        if (voteBalance == null)
                        {
                            //voteBalance = await _voteBalanceService.CreateNewVoteBalance(deposit.LedgerAccountId, session, token);
                            //if (voteBalance == null)
                            //{
                            //    throw new Exception("Unable to Create Vote Balance");
                            //}

                                throw new Exception("Unable To Found");
                            }


                        voteBalance.ExchangedAmount = deposit.InitialDepositAmount - deposit.ReleasedAmount;
                        voteBalance.Debit += voteBalance.ExchangedAmount;
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.BFRBDebit;
                        voteBalance.CreditDebitRunningBalance -= voteBalance.ExchangedAmount;



                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                        var vtbActiionlog = new VoteBalanceActionLog
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

                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

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

                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFRBCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLogAccumulatedFund = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                //vtbLogAccumulatedFund.Year = session.StartAt.Year;
                                vtbLogAccumulatedFund.Code = "DEP O/B";
                                vtbLogAccumulatedFund.SubCode = deposit.ReceiptNo;

                                vtbLogAccumulatedFund.OfficeId = token.officeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLogAccumulatedFund);



                                /**********/

                            }
                            else
                            {
                                throw new FinalAccountException("Vote Balance Not Found For Accumulated Fund");
                            }



                            await _customVoteBalanceService.UpdateCustomVoteBalance(deposit.CustomVoteId!.Value, CashBookTransactionType.CREDIT, VoteBalanceTransactionTypes.BFRBCredit, (deposit.InitialDepositAmount - deposit.ReleasedAmount), session, token);

                            await _unitOfWork.CommitAsync();


                        transaction.Commit();



                        return true;
                        }
                        else
                        {
                            throw new Exception("Unable To Found");
                        }
                    }
                    else
                    {
                        throw new Exception("No Active Session Found");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }


    }



}