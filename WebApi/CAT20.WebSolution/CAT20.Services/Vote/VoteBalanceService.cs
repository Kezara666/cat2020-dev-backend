using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Core.Models.Enums;
using AutoMapper;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.HelperModels;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Vote;
using DocumentFormat.OpenXml.Spreadsheet;
using CAT20.Core.DTO.Vote.Save;

namespace CAT20.Services.Vote
{
    public class VoteBalanceService : IVoteBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoteBalanceService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(bool, string)> CreateVoteAllocation(VoteBalance newVoteAllocation, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var vt = await _unitOfWork.VoteDetails.GetByIdAsync(newVoteAllocation.VoteDetailId);

                        if (vt != null)
                        {
                            newVoteAllocation.Status = VoteBalanceStatus.Active;
                            if (vt.ClassificationID == 1)
                            {
                                var vb = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.ID!.Value, token.sabhaId, session.StartAt.Year);

                                if (vb != null)
                                {
                                    throw new FinalAccountException("Vote Estimation Already Exist");

                                }
                                else
                                {
                                    await _unitOfWork.VoteBalances.AddAsync(newVoteAllocation);
                                    await _unitOfWork.CommitAsync();

                                }
                            }
                            else if (vt.ClassificationID == 2)
                            {
                                var vb = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.ID!.Value, token.sabhaId, session.StartAt.Year);

                                if (vb == null)
                                {

                                    newVoteAllocation.RunningBalance = (decimal)newVoteAllocation.AllocationAmount!;
                                    newVoteAllocation.AllocationExchangeAmount = newVoteAllocation.AllocationAmount;
                                    await _unitOfWork.VoteBalances.AddAsync(newVoteAllocation);
                                }
                                else
                                {
                                    throw new FinalAccountException("Vote Allocation Already Exist");
                                }




                            }
                            else
                            {
                                throw new FinalAccountException("Vote Detail Not Have Classification");
                            }

                            newVoteAllocation.TransactionType = VoteBalanceTransactionTypes.Inti;
                            newVoteAllocation.SystemActionAt = DateTime.Now;
                            newVoteAllocation.CreatedAt = session.StartAt;
                            newVoteAllocation.UpdatedAt = session.StartAt;
                            newVoteAllocation.ClassificationId = vt.ClassificationID;
                            newVoteAllocation.CreatedBy = token.userId;
                            newVoteAllocation.UpdatedBy = token.userId;

                            //await _unitOfWork.VoteBalances.AddAsync(newVoteAllocation);
                            await _unitOfWork.CommitAsync();

                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(newVoteAllocation);
                            vtbLog.Code = "ALLOCATE";
                            vtbLog.OfficeId = token.officeId;

                            var vtbActionLog = new VoteBalanceActionLog
                            {

                                VoteBalanceId = newVoteAllocation.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "create",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };



                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            await _unitOfWork.VoteBalanceActionLogs.AddAsync(vtbActionLog);
                            await _unitOfWork.CommitAsync();
                            transaction.Commit();

                            return (true, vt.ClassificationID == 1 ? "Estimation Successful" : "Allocation Successful");
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Detail Not Found");
                        }
                    }
                    else
                    {
                        throw new FinalAccountException("No active Session Found");
                    }
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    if (ex.GetType() == typeof(FinalAccountException))
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
        public async Task<(bool, string)> UpdateVoteAllocation(VoteBalance newVoteAllocation, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var vt = await _unitOfWork.VoteDetails.GetByIdAsync(newVoteAllocation.VoteDetailId);

                        if (vt != null)
                        {
                            if (vt.ClassificationID == 1)
                            {
                                var vb = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.ID!.Value, token.sabhaId, session.StartAt.Year);

                                if (vb == null)
                                {
                                    throw new FinalAccountException("Vote Estimation Not Found");


                                }
                                else
                                {
                                    if (await _unitOfWork.VoteBalances.HasTransactionsOccurred(vb.Id.Value))
                                    {
                                        throw new FinalAccountException("One Or More Transactions Have Occurred. Modification Is Not Allowed");
                                    }
                                    else
                                    {
                                        vb.EstimatedIncome = newVoteAllocation.EstimatedIncome!.Value;
                                        await _unitOfWork.CommitAsync();
                                        newVoteAllocation = vb;
                                    }

                                }
                            }
                            else if (vt.ClassificationID == 2)
                            {
                                var vb = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.ID!.Value, token.sabhaId, session.StartAt.Year);

                                if (vb == null)
                                {
                                    throw new FinalAccountException("Vote Allocation Not Found");



                                }
                                else
                                {
                                    if (await _unitOfWork.VoteBalances.HasTransactionsOccurred(vb.Id.Value))
                                    {
                                        throw new FinalAccountException("One Or More Transactions Have Occurred. Modification Is Not Allowed");
                                    }
                                    else
                                    {
                                        vb.RunningBalance = (decimal)newVoteAllocation.AllocationAmount!;
                                        vb.AllocationAmount = newVoteAllocation.AllocationAmount;
                                        vb.AllocationExchangeAmount = newVoteAllocation.AllocationAmount;
                                        newVoteAllocation = vb;
                                        await _unitOfWork.CommitAsync();

                                    }




                                }




                            }
                            else
                            {
                                throw new FinalAccountException("Vote Detail Not Have Classification");
                            }

                            newVoteAllocation.TransactionType = VoteBalanceTransactionTypes.Inti;
                            newVoteAllocation.SystemActionAt = DateTime.Now;
                            newVoteAllocation.CreatedAt = session.StartAt;
                            newVoteAllocation.UpdatedAt = session.StartAt;
                            newVoteAllocation.ClassificationId = vt.ClassificationID;
                            newVoteAllocation.CreatedBy = token.userId;
                            newVoteAllocation.UpdatedBy = token.userId;

                            //await _unitOfWork.VoteBalances.AddAsync(newVoteAllocation);
                            await _unitOfWork.CommitAsync();

                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(newVoteAllocation);
                            vtbLog.Code = "UPDATE ALLOCATE";
                            vtbLog.OfficeId = token.officeId;

                            var vtbActionLog = new VoteBalanceActionLog
                            {

                                VoteBalanceId = newVoteAllocation.Id,
                                ActionState = FinalAccountActionStates.Init,
                                ActionBy = token.userId,
                                Comment = "create",
                                ActionDateTime = session.StartAt,
                                SystemActionAt = DateTime.Now,
                            };



                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            await _unitOfWork.VoteBalanceActionLogs.AddAsync(vtbActionLog);
                            await _unitOfWork.CommitAsync();
                            transaction.Commit();

                            return (true, vt.ClassificationID == 1 ? "Estimation Successful" : "Allocation Successful");
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Detail Not Found");
                        }
                    }
                    else
                    {
                        throw new FinalAccountException("No active Session Found");
                    }
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    if (ex.GetType() == typeof(FinalAccountException))
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
        public async Task DeleteVoteAllocation(VoteBalance voteAllocation)
        {
            voteAllocation.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<VoteBalance>> GetAllVoteAllocations()
        {
            return await _unitOfWork.VoteBalances.GetAllAsync();
        }
        public async Task<VoteBalance> GetVoteAllocationById(int id)
        {
            return await _unitOfWork.VoteBalances.GetByIdAsync(id);
        }
        public async Task<VoteBalance> getVoteAllocationByVoteDetailId(int id)
        {
            return await _unitOfWork.VoteBalances.getVoteAllocationByVoteDetailId(id);
        }

        public async Task<(int totalCount, IEnumerable<VoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year, int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            return await _unitOfWork.VoteBalances.GetVoteAllocationForSabhaByYearAndProgram(sabhaId, year, classificationId, programId, voteDetailId, pageNo, pageSize, filterKeyword, token);

        }

        public async Task<(VoteBalance, string?)> GetActiveVoteBalance(int VoteDetailId, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {
                    return (await _unitOfWork.VoteBalances.GetActiveVoteBalance(VoteDetailId, token.sabhaId, session.StartAt.Year), null);
                }
                else
                {
                    throw new Exception("No active Session Found");
                }
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }



        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId)
        {
            return await _unitOfWork.VoteBalances.GetAllWithVoteAllocationByVoteDetailIdAsync(VoteDetailId);
        }

        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationBySabhaId(int SabhaId)
        {
            return await _unitOfWork.VoteBalances.GetAllWithVoteAllocationBySabhaIdAsync(SabhaId);
        }
        public async Task<(int totalCount, IEnumerable<VoteBalance> list)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            return await _unitOfWork.VoteBalances.GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(sabhaId, pageNo, pageSize, filterKeyWord);
        }

        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaId(int VoteDetailId, int SabhaId)
        {
            return await _unitOfWork.VoteBalances.GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(VoteDetailId, SabhaId);
        }

        public async Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYear(int VoteDetailId, int SabhaId, int Year)
        {
            return await _unitOfWork.VoteBalances.GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(VoteDetailId, SabhaId, Year);
        }

        public async Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForSabhaId(int SabhaId)
        {
            return await _unitOfWork.VoteBalances.GetAllVoteAllocationsForSabhaIdAsync(SabhaId);
        }

        public async Task<VoteBalance> CreateNewVoteBalance(int VoteDetailId, Session session, HTokenClaim token)
        {



            try
            {

                var vt = await _unitOfWork.VoteDetails.GetByIdAsync(VoteDetailId);

                if (vt != null)
                {
                    /*
               classification = 1 => income
              classification = 2 => expense
              classification = 3 => asset
              classification = 4 => liability
              classification = 5 => equity
      */


                    var voteBalance = new VoteBalance
                    {
                        VoteDetailId = VoteDetailId,
                        SabhaId = token.sabhaId,
                        Year = session.StartAt.Year,
                        AllocationAmount = 0,
                        Credit = 0,
                        ClassificationId = vt.ClassificationID,
                        Debit = 0,
                        ExchangedAmount = 0,
                        Status = VoteBalanceStatus.Active,
                        TransactionType = VoteBalanceTransactionTypes.Inti,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SystemActionAt = DateTime.Now
                    };
                    await _unitOfWork.VoteBalances.AddAsync(voteBalance);
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

                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                    await _unitOfWork.VoteBalanceActionLogs.AddAsync(vtbActionLog);
                    await _unitOfWork.CommitAsync();
                    return voteBalance;


                }
                else
                {
                    throw new Exception("Vote Detail Not Found");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string?)> TakeHold(SaveVoteBalanceTakeHold voteBalanceToHold, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    if (session != null)
                    {

                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(voteBalanceToHold.Id!.Value);

                        if (voteBalance != null)
                        {

                            if (voteBalanceToHold.TakeHoldRate.HasValue)
                            {
                                decimal? takeHoldAmount = (voteBalanceToHold.TakeHoldRate.Value / 100) * voteBalance.AllocationAmount;
                                takeHoldAmount = Math.Round((decimal)takeHoldAmount, 2, MidpointRounding.AwayFromZero);

                                if (takeHoldAmount < voteBalance.AllocationAmount + voteBalance.Credit - voteBalance.Debit - voteBalance.TotalHold - voteBalance.TotalPending)
                                {
                                    voteBalance.TakeHoldRate = voteBalanceToHold.TakeHoldRate.Value;
                                    voteBalance.TakeHoldAmount = (decimal)takeHoldAmount;
                                }
                                else
                                {
                                    throw new FinalAccountException("Take Hold Amount is Greater than Running Balance");

                                }

                            }
                            else
                            {
                                if (voteBalanceToHold.TakeHoldAmount < voteBalance.RunningBalance)
                                {
                                    voteBalance.TakeHoldAmount = voteBalanceToHold.TakeHoldAmount;
                                }
                                else
                                {
                                    throw new FinalAccountException("Take Hold Amount is Greater than Running Balance");

                                }
                            }


                            voteBalance.TransactionType = VoteBalanceTransactionTypes.TakeHold;

                            voteBalance.UpdatedAt = session.StartAt;
                            voteBalance.UpdatedBy = token.userId;
                            voteBalance.SystemActionAt = DateTime.Now;
                            voteBalance.Description = voteBalanceToHold.RequestNote;


                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            await _unitOfWork.CommitAsync();
                            transaction.Commit();
                            return (true, "Hold Successfully");

                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Not Found");

                        }

                    }
                    else
                    {
                        throw new FinalAccountException("No active Session Found");
                    }




                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.GetType() == typeof(FinalAccountException))
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

        public async Task<(bool, string?)> ReleaseTakeHold(ReleaseVoteBalanceTakeHold releaseVoteBalanceTake, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    if (session != null)
                    {

                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(releaseVoteBalanceTake.VoteBalanceId);

                        if (voteBalance != null)
                        {

                            voteBalance.TakeHoldRate = 0m;
                            voteBalance.TakeHoldAmount = 0m;
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.ReleaseTakeHold;


                            voteBalance.UpdatedAt = session.StartAt;
                            voteBalance.UpdatedBy = token.userId;
                            voteBalance.SystemActionAt = DateTime.Now;
                            voteBalance.Description = releaseVoteBalanceTake.ActionNote;

                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                            await _unitOfWork.CommitAsync();
                            transaction.Commit();
                            return (true, "Hold Successfully");

                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Not Found");

                        }

                    }
                    else
                    {
                        throw new FinalAccountException("No active Session Found");
                    }




                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.GetType() == typeof(FinalAccountException))
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

        public async Task<bool> UpdateVoteBalanceForOpenBalances(int voteDetailId, decimal Amount, int? year, int? month, string? code, string? subCode, FAMainTransactionMethod transactionMethod, Session session, HTokenClaim token)
        {


            try
            {

                var vt = await _unitOfWork.VoteDetails.GetByIdAsync(voteDetailId);

                if (vt != null)
                {
                    /*
                 classification = 1 => income
                classification = 2 => expense
                classification = 3 => asset
                classification = 4 => liability
                classification = 5 => equity
                  */


                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(voteDetailId, token.sabhaId, session.StartAt.Year);
                    if (voteBalance == null)
                    {
                        voteBalance = await CreateNewVoteBalance(voteDetailId, session, token);
                        if (voteBalance == null)
                        {
                            throw new Exception("Unable to Create Vote Balance");
                        }
                    }


                    if (voteBalance != null)
                    {

                        var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                        var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                        if (accumulatedFundBalance == null)
                        {
                            accumulatedFundBalance = await CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                        }

                        /******************/

                        if (accumulatedFund != null)
                        {

                            voteBalance.UpdatedBy = token.userId;
                            voteBalance.UpdatedAt = session.StartAt;
                            voteBalance.SystemActionAt = DateTime.Now;
                            voteBalance.OfficeId = token.officeId;
                            voteBalance.SabhaId = token.sabhaId;
                            voteBalance.SessionIdByOffice = session.Id;



                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.OfficeId = token.officeId;
                            accumulatedFundBalance.SabhaId = token.sabhaId;
                            accumulatedFundBalance.SessionIdByOffice = session.Id;



                            //voteBalance.Year = year;
                            //voteBalance.Month = month;
                            voteBalance.Code = code;
                            voteBalance.SubCode = subCode;

                            //accumulatedFundBalance.Year = year;
                            //accumulatedFundBalance.Month = month;
                            accumulatedFundBalance.Code = code;
                            accumulatedFundBalance.SubCode = subCode;




                            if (vt.ClassificationID == 1 && voteBalance.ClassificationId == 1)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Income Vote Balance");

                            }
                            else if (vt.ClassificationID == 2 && voteBalance.ClassificationId == 2)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Expense Vote Balance");
                            }
                            else if (vt.ClassificationID == 3 && voteBalance.ClassificationId == 3)
                            {


                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    voteBalance.ExchangedAmount = Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;


                                    accumulatedFundBalance.ExchangedAmount = Amount;
                                    accumulatedFundBalance.Credit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    voteBalance.ExchangedAmount = Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                                    accumulatedFundBalance.ExchangedAmount = Amount;
                                    accumulatedFundBalance.Debit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                }

                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                            }
                            else if (vt.ClassificationID == 4 && voteBalance.ClassificationId == 4)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    voteBalance.ExchangedAmount = Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                                    accumulatedFundBalance.ExchangedAmount = Amount;
                                    accumulatedFundBalance.Debit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;


                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {
                                    voteBalance.ExchangedAmount = Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;

                                    accumulatedFundBalance.ExchangedAmount = Amount;
                                    accumulatedFundBalance.Credit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                                }

                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (vt.ClassificationID == 5 && voteBalance.ClassificationId == 5)
                            {
                                throw new FinalAccountException("This Transaction Not Allowed For Equity Vote Balance");
                            }

                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }


                            await _unitOfWork.CommitAsync();

                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;
                            var vtbLogAccumulatedFund = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLogAccumulatedFund);

                            await _unitOfWork.CommitAsync();
                            return true;


                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                    }
                    else
                    {
                        throw new FinalAccountException($"Vote Balance Not Found For Vote Code {vt.Code}");
                    }

                }
                else
                {
                    throw new Exception("Vote Detail Not Found");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateVoteBalance(HVoteBalanceTransaction voteBalanceTransaction, HTokenClaim token)
        {
            try
            {

                var vt = await _unitOfWork.VoteDetails.GetByIdAsync(voteBalanceTransaction.VoteDetailId);

                if (vt != null)
                {
                    /*
                 classification = 1 => income
                classification = 2 => expense
                classification = 3 => asset
                classification = 4 => liability
                classification = 5 => equity
                  */


                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(voteBalanceTransaction.VoteDetailId, token.sabhaId, voteBalanceTransaction.Session.StartAt.Year);
                    if (voteBalance == null)
                    {
                        voteBalance = await CreateNewVoteBalance(voteBalanceTransaction.VoteDetailId, voteBalanceTransaction.Session, token);
                        if (voteBalance == null)
                        {
                            throw new Exception(voteBalanceTransaction.NotFoundExceptionMessage);
                        }
                    }

                    if (voteBalance != null)
                    {

                        /******************/

                            if (vt.ClassificationID == 1 && voteBalance.ClassificationId == 1)
                            {
                            if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Forward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                            }
                            else if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Backward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }


                            }

                        }
                            else if (vt.ClassificationID == 2 && voteBalance.ClassificationId == 2)
                            {
                            if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Forward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                            }
                            else if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Backward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }


                            }
                        }
                            else if (vt.ClassificationID == 3 && voteBalance.ClassificationId == 3)
                            {


                                if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Forward)
                                {
                                    if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                            VoteBalanceTransactionTypes.Credit or
                                            VoteBalanceTransactionTypes.BFRBCredit or
                                            VoteBalanceTransactionTypes.BFJACredit or
                                            VoteBalanceTransactionTypes.JournalCredit or
                                            VoteBalanceTransactionTypes.FRCredit or
                                            VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                            VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                    {
                                        voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                        voteBalance.Credit += voteBalance.ExchangedAmount;
                                        voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                    else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                        VoteBalanceTransactionTypes.Debit or
                                        VoteBalanceTransactionTypes.BFRBDebit or
                                        VoteBalanceTransactionTypes.BFJADebit or
                                        VoteBalanceTransactionTypes.JournalDebit or
                                        VoteBalanceTransactionTypes.FRDebit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                    {
                                        voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                        voteBalance.Debit += voteBalance.ExchangedAmount;
                                        voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                            }
                                else if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Backward)
                                {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }


                            }

                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                            }
                            else if (vt.ClassificationID == 4 && voteBalance.ClassificationId == 4)
                            {
                            if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Forward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                            }
                            else if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Backward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType; 
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType; 
                                }


                            }

                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (vt.ClassificationID == 5 && voteBalance.ClassificationId == 5)
                            {
                            if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Forward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }

                            }
                            else if (voteBalanceTransaction.TransactionMethod == FAMainTransactionMethod.Backward)
                            {
                                if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFCredit or
                                        VoteBalanceTransactionTypes.Credit or
                                        VoteBalanceTransactionTypes.BFRBCredit or
                                        VoteBalanceTransactionTypes.BFJACredit or
                                        VoteBalanceTransactionTypes.JournalCredit or
                                        VoteBalanceTransactionTypes.FRCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLCredit or
                                        VoteBalanceTransactionTypes.AutoTransferJNLRBCredit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Debit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType; 
                                }

                                else if (voteBalanceTransaction.TransactionType is VoteBalanceTransactionTypes.BFDebit or
                                    VoteBalanceTransactionTypes.Debit or
                                    VoteBalanceTransactionTypes.BFRBDebit or
                                    VoteBalanceTransactionTypes.BFJADebit or
                                    VoteBalanceTransactionTypes.JournalDebit or
                                    VoteBalanceTransactionTypes.FRDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLDebit or
                                    VoteBalanceTransactionTypes.AutoTransferJNLRBDebit)
                                {
                                    voteBalance.ExchangedAmount = voteBalanceTransaction.Amount;
                                    voteBalance.Credit += voteBalance.ExchangedAmount;
                                    voteBalance.TransactionType = voteBalanceTransaction.TransactionType;
                                }


                            }
                        }

                            else
                            {
                                throw new Exception("Vote Detail Not Have Classification");
                            }


                            await _unitOfWork.CommitAsync();
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            //await _unitOfWork.CommitAsync();
                            return true;
                      
                        /******************/

                    }
                    else
                    {
                        throw new FinalAccountException($"Vote Balance Not Found For Vote Code {vt.Code}");
                    }

                }
                else
                {
                    throw new Exception("Vote Detail Not Found");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(bool, string)> SaveComparativeFiguresBalance(List<saveCompartiveFigureBalance> balance, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    if (session != null)
                    {
                        foreach (var b in balance)
                        {

                            var vt = await _unitOfWork.VoteDetails.GetByIdAsync(b.VoteDetailId);

                            if (vt != null)
                            {
                                var expiredVoteBalance = await _unitOfWork.VoteBalances.GetExpiredVoteBalance(b.VoteDetailId, token.sabhaId, (session.StartAt.Year - 1));

                                if (expiredVoteBalance == null)
                                {
                                    /*
                               classification = 1 => income
                              classification = 2 => expense
                              classification = 3 => asset
                              classification = 4 => liability
                              classification = 5 => equity
                      */


                                    var voteBalance = new VoteBalance
                                    {
                                        VoteDetailId = vt.ID,
                                        SabhaId = token.sabhaId,
                                        Year = (session.StartAt.Year - 1),
                                        AllocationAmount = 0,
                                        Credit = 0,
                                        ClassificationId = vt.ClassificationID,
                                        Debit = 0,
                                        ExchangedAmount = 0,
                                        Status = VoteBalanceStatus.Expired,
                                        TransactionType = VoteBalanceTransactionTypes.Inti,
                                        CreatedAt = session.StartAt,
                                        UpdatedAt = session.StartAt,
                                        CreatedBy = token.userId,
                                        UpdatedBy = token.userId,
                                        SystemActionAt = DateTime.Now
                                    };
                                   


                                    if (vt.ClassificationID == 1)
                                    {
                                        voteBalance.Credit = b.BalanceAmount;
                                        voteBalance.CreditDebitRunningBalance= b.BalanceAmount;
                                    }
                                    else if (vt.ClassificationID == 2)
                                    {
                                        voteBalance.Debit = b.BalanceAmount;
                                        voteBalance.CreditDebitRunningBalance = b.BalanceAmount;
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

                                    await _unitOfWork.VoteBalances.AddAsync(voteBalance);
                                    await _unitOfWork.CommitAsync();

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

                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                    await _unitOfWork.VoteBalanceActionLogs.AddAsync(vtbActionLog);
                                    await _unitOfWork.CommitAsync();


                                }
                                else
                                {
                                    throw new Exception("Vote Balance Figure Already Exist");
                                }


                            }
                            else
                            {
                                throw new Exception("Vote Detail Not Found");

                            }



                        }

                            
                    }
                    else
                    {
                        throw new FinalAccountException("No active Session Found");
                    }

                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return (true, "Save Successfully");


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.GetType() == typeof(FinalAccountException))
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
}
