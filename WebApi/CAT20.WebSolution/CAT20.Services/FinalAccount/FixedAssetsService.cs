using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Hosting;
using PdfSharp.Pdf.IO;

namespace CAT20.Services.FinalAccount
{
    public class FixedAssetsService : IFixedAssetsService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;

        public FixedAssetsService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
        }


        public async Task<(int totalCount, IEnumerable<FixedAssetsResource> list)> GetAllFixedAssetsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var fixedDeposit = await _unitOfWork.FixedAssets.GetAllFixedAssetsForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);


            var fixedAssetsResource = _mapper.Map<IEnumerable<FixedAssets>, IEnumerable<FixedAssetsResource>>(fixedDeposit.list);

            foreach (var item in fixedAssetsResource)
            {
                item.AssetLedgerAccount = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.AssetsLedgerAccountId!.Value));
                if (item.GrantLedgerAccountId.HasValue)
                {
                    item.GrantLedgerAccount = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.GrantLedgerAccountId!.Value));
                }
            }

            return (fixedDeposit.totalCount, fixedAssetsResource);

        }



        public async Task<(bool, string?)> CreateUpdateFixedAssets(FixedAssets newFixedAssets, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        if (newFixedAssets.Id != null)
                        {
                            var fixAssets = await _unitOfWork.FixedAssets.GetByIdAsync(newFixedAssets.Id);
                            if (fixAssets != null)
                            {
                                //decimal accumulatedAmount = ((decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation!) - ((decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition);

                                decimal accumulatedAmount = Math.Max( (decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation! - (decimal)fixAssets.GrantAmount! + (decimal)fixAssets.AccumulatedRevenueRecognition, 0);


                                if (await UpdateVoteBalanceForOpenBalances(fixAssets.AssetsLedgerAccountId!.Value, (decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation!, null, null, $"B/F FX REG {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, accumulatedAmount > 0, accumulatedAmount, session, token))
                                {



                                    if (await UpdateVoteBalanceForOpenBalances(fixAssets.GrantLedgerAccountId!.Value, (decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition, null, null, $"B/F FX REG {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, false, 0m, session, token))
                                    {


                                        fixAssets.Status = 0;
                                        fixAssets.SystemActionAt = DateTime.Now;
                                        fixAssets.UpdatedAt = session.StartAt;
                                        fixAssets.UpdatedBy = token.userId;

                                        newFixedAssets.Id = null;
                                        newFixedAssets.CreatedAt = session.StartAt;
                                        newFixedAssets.CreatedBy = token.userId;
                                        newFixedAssets.SabhaId = token.sabhaId;
                                        newFixedAssets.OfficeId = token.officeId;
                                        newFixedAssets.SystemActionAt = DateTime.Now;
                                        await _unitOfWork.FixedAssets.AddAsync(newFixedAssets);

                                        //decimal newAccumulatedAmount = ((decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation!) - ((decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition);

                                        decimal newAccumulatedAmount = Math.Max((decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation! - (decimal)fixAssets.GrantAmount! + (decimal)fixAssets.AccumulatedRevenueRecognition, 0);

                                        await UpdateVoteBalanceForOpenBalances(newFixedAssets.AssetsLedgerAccountId!.Value, (decimal)fixAssets.OriginalORRevaluedAmount!, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, newAccumulatedAmount > 0, newAccumulatedAmount, session, token);

                                        var depreciateAccount = await _unitOfWork.VoteDetails.GetAssetsDepreciationAccounts(token.sabhaId, newFixedAssets.AssetsLedgerAccountId!.Value);

                                        if (depreciateAccount == null)
                                        {
                                            throw new FinalAccountException("Depreciate Account Not Found");
                                        }

                                        await UpdateVoteBalanceForOpenBalances(depreciateAccount.ID!.Value, (decimal)fixAssets.AccumulatedDepreciation!, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, newAccumulatedAmount > 0, newAccumulatedAmount, session, token);

                                        if (newFixedAssets.GrantLedgerAccountId!.HasValue)
                                        {

                                            await UpdateVoteBalanceForOpenBalances(newFixedAssets.GrantLedgerAccountId!.Value, (decimal)newFixedAssets.GrantAmount! - (decimal)newFixedAssets.AccumulatedRevenueRecognition, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, false, 0m, session, token);

                                        }

                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to Update");
                                    }
                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to Update");
                                }



                            }
                            else
                            {
                                throw new GeneralException("Unable To Find Entry");
                            }
                        }
                        else
                        {
                            newFixedAssets.Id = null;
                            newFixedAssets.CreatedAt = session.StartAt;
                            newFixedAssets.CreatedBy = token.userId;
                            newFixedAssets.SabhaId = token.sabhaId;
                            newFixedAssets.OfficeId = token.officeId;
                            newFixedAssets.SystemActionAt = DateTime.Now;

                            await _unitOfWork.FixedAssets.AddAsync(newFixedAssets);

                            //decimal accumulatedAmount = ((decimal)newFixedAssets.OriginalORRevaluedAmount - (decimal)newFixedAssets.AccumulatedDepreciation!) - ((decimal)newFixedAssets.GrantAmount! - (decimal)newFixedAssets.AccumulatedRevenueRecognition);

                            decimal accumulatedAmount = Math.Max((decimal)newFixedAssets.OriginalORRevaluedAmount - (decimal)newFixedAssets.AccumulatedDepreciation! - (decimal)newFixedAssets.GrantAmount! + (decimal)newFixedAssets.AccumulatedRevenueRecognition, 0);

                            await UpdateVoteBalanceForOpenBalances(newFixedAssets.AssetsLedgerAccountId!.Value, (decimal)newFixedAssets.OriginalORRevaluedAmount, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, accumulatedAmount > 0, accumulatedAmount, session, token);


                            var depreciateAccount = await _unitOfWork.VoteDetails.GetAssetsDepreciationAccounts(token.sabhaId, newFixedAssets.AssetsLedgerAccountId!.Value);

                            if (depreciateAccount == null)
                            {
                                throw new FinalAccountException("Depreciate Account Not Found");
                            }

                            await UpdateVoteBalanceForOpenBalances(depreciateAccount.ID!.Value, (decimal)newFixedAssets.AccumulatedDepreciation!, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, false, 0, session, token);


                            if (newFixedAssets.GrantLedgerAccountId!.HasValue)
                            {

                                await UpdateVoteBalanceForOpenBalances(newFixedAssets.GrantLedgerAccountId!.Value, (decimal)newFixedAssets.GrantAmount! - (decimal)newFixedAssets.AccumulatedRevenueRecognition, null, null, $"B/F FX REG {newFixedAssets.AssetsRegNo} ", newFixedAssets.AssetsTitle, FAMainTransactionMethod.Forward, false, 0m, session, token);
                            }


                        }

                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Saved");

                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }

        public async Task<bool> UpdateVoteBalanceForOpenBalances(int voteDetailId, decimal Amount, int? year, int? month, string? code, string? subCode, FAMainTransactionMethod transactionMethod, Boolean hasAccumulatedFundEntry, decimal accumulatedAmount, Session session, HTokenClaim token)
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
                        voteBalance = await _voteBalanceService.CreateNewVoteBalance(voteDetailId, session, token);
                        if (voteBalance == null)
                        {
                            throw new Exception("Unable to Create Vote Balance");
                        }
                    }


                    if (voteBalance != null)
                    {

                        if (hasAccumulatedFundEntry)
                        {

                            var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                            var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                            if (accumulatedFundBalance == null)
                            {
                                accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                            }


                            if (accumulatedFundBalance != null)
                            {
                                if (transactionMethod == FAMainTransactionMethod.Forward)
                                {


                                    accumulatedFundBalance.ExchangedAmount = accumulatedAmount;
                                    accumulatedFundBalance.Credit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;




                                }
                                else if (transactionMethod == FAMainTransactionMethod.Backward)
                                {


                                    accumulatedFundBalance.ExchangedAmount = accumulatedAmount;
                                    accumulatedFundBalance.Debit += accumulatedFundBalance.ExchangedAmount;
                                    accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                }
                                else
                                {
                                    throw new FinalAccountException("Transaction Method Not Found");
                                }

                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;
                                var vtbLogAccumulatedFund = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLogAccumulatedFund);


                            }
                            else
                            {
                                throw new FinalAccountException("Unable to Create Accumulated Fund Vote Balance");
                            }
                        }


                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.SystemActionAt = DateTime.Now;
                        voteBalance.OfficeId = token.officeId;
                        voteBalance.SabhaId = token.sabhaId;
                        voteBalance.SessionIdByOffice = session.Id;





                        //voteBalance.Year = year;
                        //voteBalance.Month = month;
                        voteBalance.Code = code;
                        voteBalance.SubCode = subCode;





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

                            }
                            else if (transactionMethod == FAMainTransactionMethod.Backward)
                            {
                                voteBalance.ExchangedAmount = Amount;
                                voteBalance.Credit += voteBalance.ExchangedAmount;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


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


                            }
                            else if (transactionMethod == FAMainTransactionMethod.Backward)
                            {
                                voteBalance.ExchangedAmount = Amount;
                                voteBalance.Debit += voteBalance.ExchangedAmount;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;


                            }

                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;
                        }
                        else if (vt.ClassificationID == 5 && voteBalance.ClassificationId == 5)
                        {
                            if (transactionMethod == FAMainTransactionMethod.Forward)
                            {
                                voteBalance.ExchangedAmount = Amount;
                                voteBalance.Credit += voteBalance.ExchangedAmount;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;


                            }
                            else if (transactionMethod == FAMainTransactionMethod.Backward)
                            {
                                voteBalance.ExchangedAmount = Amount;
                                voteBalance.Debit += voteBalance.ExchangedAmount;
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;


                            }

                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;
                        }

                        else
                        {
                            throw new Exception("Vote Detail Not Have Classification");
                        }



                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                        await _unitOfWork.CommitAsync();
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

        public async Task<(bool, string?)> DeleteFixedAssets(int fixedAssetsId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var fixAssets = await _unitOfWork.FixedAssets.GetByIdAsync(fixedAssetsId);
                        if (fixAssets != null)
                        {

                            decimal accumulatedAmount = ((decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation!) - ((decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition);


                            var depreciateAccount = await _unitOfWork.VoteDetails.GetAssetsDepreciationAccounts(token.sabhaId, fixAssets.AssetsLedgerAccountId!.Value);

                            if (depreciateAccount == null)
                            {
                                throw new FinalAccountException("Depreciate Account Not Found");
                            }


                            if (await UpdateVoteBalanceForOpenBalances(fixAssets.AssetsLedgerAccountId!.Value, (decimal)fixAssets.OriginalORRevaluedAmount , null, null, $"B/F RB FX REG {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, (accumulatedAmount > 0), accumulatedAmount, session, token)
                            && await UpdateVoteBalanceForOpenBalances(depreciateAccount.ID!.Value,  (decimal)fixAssets.AccumulatedDepreciation!, null, null, $"B/F RB FX REG {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, (false), 0, session, token))
                            {

                                if (fixAssets.GrantLedgerAccountId!.HasValue)
                                {

                                    if (await UpdateVoteBalanceForOpenBalances(fixAssets.GrantLedgerAccountId!.Value, (decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition, null, null, $"B/F FX REG {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, false, 0m, session, token))
                                    {


                                        fixAssets.Status = 0;
                                        fixAssets.SystemActionAt = DateTime.Now;
                                        fixAssets.UpdatedAt = session.StartAt;
                                        fixAssets.UpdatedBy = token.userId;

                                        await _unitOfWork.CommitAsync();
                                        transaction.Commit();

                                        return (true, "Successfully Delete Entry");


                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to Update Grant Account");
                                    }
                                }
                                else
                                {
                                    fixAssets.Status = 0;
                                    fixAssets.SystemActionAt = DateTime.Now;
                                    fixAssets.UpdatedAt = session.StartAt;
                                    fixAssets.UpdatedBy = token.userId;

                                    await _unitOfWork.CommitAsync();
                                    transaction.Commit();

                                    return (true, "Successfully Delete Entry");

                                }
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to Update");
                            }



                        }
                        else
                        {
                            throw new GeneralException("Unable To Find Entry");
                        }
                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }

        public async Task<(bool, string?)> Depreciation(int? fixedAssetsId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var fixAssetsList = await _unitOfWork.FixedAssets.GetForDepreciation(fixedAssetsId,token.sabhaId);



                        foreach (var fixAssets in fixAssetsList)
                        {


                            //decimal accumulatedAmount = ((decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation!) - ((decimal)fixAssets.GrantAmount! - (decimal)fixAssets.AccumulatedRevenueRecognition);

                          

                            var depreciateAccount = await _unitOfWork.VoteDetails.GetAssetsDepreciationAccounts(token.sabhaId, fixAssets.AssetsLedgerAccountId!.Value);

                            if (depreciateAccount == null)
                            {
                                throw new FinalAccountException("Depreciate Account Not Found");
                            }
                            var depreciateRates = await _unitOfWork.DepreciationRates.GetDepreciationRate(depreciateAccount.IncomeSubtitleCode! ,token.sabhaId);
                           
                            // reducing balance method

                            if (depreciateRates != null)
                            {   if(fixAssets.BalanceType == FixedAssetsBalanceTypes.Original)
                                {

                                    decimal actualDepreciation = (decimal)fixAssets.OriginalORRevaluedAmount * ((decimal)depreciateRates.Rate!);

                                    // Assume ReduceBalanceDepreciationMethod is the method you're testing
                                    //decimal actualDepreciation = ReduceBalanceDepreciationMethod(fixAssets.OriginalORRevaluedAmount, (decimal)depreciateRates.Rate,(session.StartAt.Year - (int)fixAssets.AcquiredDate?.Year!));

                                    await UpdateVoteBalanceForOpenBalances(fixAssets.AssetsLedgerAccountId!.Value, (decimal)actualDepreciation, null, null, $"DRPT {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, false, 0, session, token);
                                    await UpdateVoteBalanceForOpenBalances(depreciateAccount.ID!.Value, (decimal)actualDepreciation, null, null, $"DRPT {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Backward, (false), 0, session, token);


                                    decimal grantDepreciation = (decimal)fixAssets.GrantAmount * ((decimal)depreciateRates.Rate!);

                                    if (fixAssets.GrantLedgerAccountId!.HasValue)
                                    {

                                        await UpdateVoteBalanceForOpenBalances(fixAssets.GrantLedgerAccountId!.Value, grantDepreciation , null, null, $"DRPT {fixAssets.AssetsRegNo} ", fixAssets.AssetsTitle, FAMainTransactionMethod.Forward, false, 0m, session, token);

                                    }

                                }else if(fixAssets.BalanceType == FixedAssetsBalanceTypes.Revalue)
                                {
                                    var depreciationBase = CalculateDepreciationBase(1, fixAssets.OriginalORRevaluedAmount, fixAssets.RemainingLifetime!.Value);

                                    decimal netValue = (decimal)fixAssets.OriginalORRevaluedAmount - (decimal)fixAssets.AccumulatedDepreciation;

                                    decimal actualDepreciation = netValue * (depreciationBase);

                                }
                            }




                        }

                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Delete Entry");

                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }
        private decimal ReduceBalanceDepreciationMethod(decimal initialCost, decimal depreciationRate, int year)
        {
            decimal bookValue = initialCost;
            decimal depreciationExpense = 0;

            for (int i = 1; i <= year; i++)
            {
                depreciationExpense = bookValue * (depreciationRate / 100);
                bookValue -= depreciationExpense;
            }

            return depreciationExpense; 
        }

        private decimal CalculateDepreciationBase(decimal salvageValue, decimal cost, int years)
        {
            if (years <= 0 || cost <= 0)
            {
                throw new ArgumentException("Years and Cost must be greater than zero.");
            }

            // Formula: DB = 1 - (SalvageValue / Cost) ^ (1 / Years)
            decimal ratio = salvageValue / cost;
            decimal exponent = 1m / years;
            decimal depreciationRate = 1m - (decimal)Math.Pow((double)ratio, (double)exponent);

            return depreciationRate;
        }

        public async Task<(bool, string?)> Disposal(SaveFixedAssetsDisposalResource disposalRequest, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var fixAssets = await _unitOfWork.FixedAssets.GetByIdAsync(disposalRequest.Id);


                        var netValue = fixAssets.OriginalORRevaluedAmount - fixAssets.AccumulatedDepreciation;
                        fixAssets.ProfitOrLoss = (decimal)disposalRequest.SaleOrScrapAmount - netValue;
                        fixAssets.SaleOrScrapAmount = (decimal)disposalRequest.SaleOrScrapAmount;
                        fixAssets.AcquiredDate = disposalRequest.DisposalDate;
                        fixAssets.UpdatedBy = token.userId;
                        fixAssets.UpdatedAt = session.StartAt;
                        fixAssets.SystemActionAt = DateTime.Now;
                        fixAssets.Status = disposalRequest.Status;



                        /*update AssesetBalnce*/

                        await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                        {
                            VoteDetailId = fixAssets.AssetsLedgerAccountId!.Value,
                            Amount = netValue,
                            Year = session.StartAt.Year,
                            Month = session.StartAt.Month,
                            Code = $"DRPT FX REG {fixAssets.AssetsRegNo}",
                            SubCode = fixAssets.AssetsTitle,
                            TransactionMethod = FAMainTransactionMethod.Backward,
                            TransactionType = VoteBalanceTransactionTypes.Credit,
                            Session = session
                        }, token);


                        var assestSaleAccount =   await _unitOfWork.VoteDetails.GetAssetsSaleAccounts(token.sabhaId);

                        if (assestSaleAccount != null) {
                             await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                            {
                                VoteDetailId = assestSaleAccount.ID!.Value,
                                Amount = netValue,
                                Year = session.StartAt.Year,
                                Month = session.StartAt.Month,
                                Code = $"DRPT FX REG {fixAssets.AssetsRegNo}",
                                SubCode = fixAssets.AssetsTitle,
                                TransactionMethod = FAMainTransactionMethod.Backward,
                                TransactionType = VoteBalanceTransactionTypes.Debit,
                                Session = session
                            }, token);
                        }
                        else
                        {
                            throw new FinalAccountException("Unable Found Assets Sale Account");
                        }





                         var disposalAccount = await _unitOfWork.VoteDetails.GetAssetsDisposalAccounts(token.sabhaId, fixAssets.AssetsLedgerAccountId!.Value);


                        if (disposalAccount != null) 
                        {
                            await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                            {
                                VoteDetailId = disposalAccount.ID!.Value,
                                Amount = netValue,
                                Year = session.StartAt.Year,
                                Month = session.StartAt.Month,
                                Code = $"DRPT FX REG {fixAssets.AssetsRegNo}",
                                SubCode = fixAssets.AssetsTitle,
                                TransactionMethod = FAMainTransactionMethod.Backward,
                                TransactionType = VoteBalanceTransactionTypes.Debit,
                                Session = session
                            }, token);

                        }
                        else
                        {
                            throw new FinalAccountException("Unable Find  Disposal Account");
                        }


                        if (fixAssets.ProfitOrLoss >= 0)
                        {
                            var profitAccount = await _unitOfWork.VoteDetails.GetAssetsProfitAccounts(fixAssets.AssetsLedgerAccountId.Value,token.sabhaId);

                            await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                            {
                                VoteDetailId = profitAccount.ID!.Value,
                                Amount = Math.Abs(fixAssets.SaleOrScrapAmount),
                                Year = session.StartAt.Year,
                                Month = session.StartAt.Month,
                                Code = $"DRPT FX REG {fixAssets.AssetsRegNo}",
                                SubCode = fixAssets.AssetsTitle,
                                TransactionMethod = FAMainTransactionMethod.Backward,
                                TransactionType = VoteBalanceTransactionTypes.Debit,
                                Session = session
                            }, token);

                        }
                        else
                        {
                            var lossAccount = await _unitOfWork.VoteDetails.GetAssetsLossAccounts(fixAssets.AssetsLedgerAccountId.Value, token.sabhaId);

                            await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                            {
                                VoteDetailId = lossAccount.ID!.Value,
                                Amount = Math.Abs(fixAssets.SaleOrScrapAmount),
                                Year = session.StartAt.Year,
                                Month = session.StartAt.Month,
                                Code = $"DRPT FX REG {fixAssets.AssetsRegNo}",
                                SubCode = fixAssets.AssetsTitle,
                                TransactionMethod = FAMainTransactionMethod.Backward,
                                TransactionType = VoteBalanceTransactionTypes.Debit,
                                Session = session
                            }, token);
                        }


                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, $"Successfully Disposed Entry");

                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }
    }
}
