using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.FinalAccount
{
    public class WaterFinalAccountService : IWaterFinalAccountService
    {
        private readonly IMapper _mapper;
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IOfficeService _officeService;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly IWaterProjectService _waterProjectService;

        public WaterFinalAccountService(IMapper mapper, IVoteUnitOfWork unitOfWork,IOfficeService officeService, IVoteBalanceService voteBalanceService, IWaterProjectService waterProjectService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _officeService = officeService;
            _voteBalanceService = voteBalanceService;
            _waterProjectService = waterProjectService;
        }


        public async Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token)
        {
            try
            {

                var waterProjects = await _waterProjectService.GetAllForSabha(sabhaId);


                var votesAssign = await _unitOfWork.WbVoteAssign.GetForAllProjects(waterProjects.Select(x => x.Id).ToList());

                foreach (var item in votesAssign)
                {

                    var customVotes = await _unitOfWork.CustomVoteDetails.GetById(item.vote!.Value);

                    if (customVotes != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(customVotes.VoteAssignmentId);

                        if (voteAssign != null)
                        {

                            item.VoteDetailsId = voteAssign.VoteId;
                        }
                        else
                        {
                            throw new Exception("Vote Assignment Not Found");
                        }


                    }
                    else
                    {
                        throw new Exception("Vote Assignment Details Not Found");
                    }

                }


                await _unitOfWork.CommitAsync();
                return true;



            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> WaterInit(int sabhaId, HTokenClaim token)
        {
            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var office = await _officeService.getAllOfficesForSabhaId(sabhaId);
                    var waterConnections = await _unitOfWork.WaterConnections.GetYearEndProcessForFinalAccount(office.Select(o => o.ID).ToList());

                    var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(sabhaId);

                    var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, sabhaId, session.StartAt.Year);

                    if (accumulatedFundBalance == null)
                    {
                        accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                    }

                    foreach (var item in waterConnections)
                    {
                        var votes = await _unitOfWork.WbVoteAssign.GetAllForWaterProject(item.SubRoad!.WaterProjectId);

                        if (votes != null)
                        {

                            decimal? LYArrears = 0m;
                            decimal? TYArrears = 0m;
                            decimal? OverPay = 0m;

                            if (item.OpeningBalanceInformation!.MonthlyBalance < 0)
                            {
                                OverPay = Math.Abs((decimal)item.OpeningBalanceInformation!.MonthlyBalance);
                            }
                            else
                            {
                                LYArrears = item.OpeningBalanceInformation!.LastYearArrears;
                                TYArrears = item.OpeningBalanceInformation!.MonthlyBalance - item.OpeningBalanceInformation!.LastYearArrears;
                            }


                            var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), sabhaId, session.StartAt.Year);


                            if (voteBalanceLYArrears == null)
                            {
                                voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                            }

                            if (voteBalanceLYArrears != null)
                            {
                                voteBalanceLYArrears.Debit += (decimal)LYArrears!;
                                voteBalanceLYArrears.UpdatedBy = token.userId;
                                voteBalanceLYArrears.UpdatedAt = session.StartAt;
                                voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                                voteBalanceLYArrears.ExchangedAmount = (decimal)LYArrears!;

                                voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                                voteBalanceLYArrears.ExchangedAmount = 0;


                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR LYA O/B";
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Last year Arrears");
                            }


                            /******************/

                            if (accumulatedFund != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)LYArrears!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)LYArrears!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR LYA O/B";
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), sabhaId, session.StartAt.Year);


                            if (voteBalanceTYArrears == null)
                            {
                                voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                            }

                            if (voteBalanceTYArrears != null)
                            {

                                voteBalanceTYArrears.Debit += (decimal)TYArrears!;
                                voteBalanceTYArrears.UpdatedBy = token.userId;
                                voteBalanceTYArrears.UpdatedAt = session.StartAt;
                                voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                                voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                                voteBalanceTYArrears.ExchangedAmount = (decimal)TYArrears!;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                                voteBalanceTYArrears.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR TYA O/B";
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/
                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For This Year Arrears");
                            }


                            /******************/

                            if (accumulatedFund != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)TYArrears!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)TYArrears!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR TYA O/B";
                                vtbLog.Month = item.OpeningBalanceInformation.Month;
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), sabhaId, session.StartAt.Year);


                            if (voteBalanceOverPay == null)
                            {
                                voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                            }

                            if (voteBalanceOverPay != null)
                            {
                                voteBalanceOverPay.Credit += (decimal)OverPay!;
                                voteBalanceOverPay.UpdatedBy = token.userId;
                                voteBalanceOverPay.UpdatedAt = session.StartAt;
                                voteBalanceOverPay.SystemActionAt = DateTime.Now;

                                voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                                voteBalanceOverPay.ExchangedAmount = (decimal)OverPay!;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                                voteBalanceOverPay.ExchangedAmount = 0m;


                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR OverPay O/B";
                                vtbLog.Month = item.OpeningBalanceInformation.Month;
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Over Payment");
                            }

                            /******************/

                            if (accumulatedFund != null)
                            {

                                accumulatedFundBalance.Debit += (decimal)OverPay!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)OverPay!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "WTR OverPay O/B";
                                vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                                vtbLog.OfficeId = item.OfficeId;
                                vtbLog.AppCategory = AppCategory.Water_Bill;
                                vtbLog.ModulePrimaryKey = item.Id;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/


                        }
                        else
                        {
                            throw new Exception("Vote Assignment Not Found");
                        }
                    }

                }
                else
                {
                    throw new Exception("Active Session Not Found");
                }

                await _unitOfWork.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;

            }
        }

        private void CreateJournalTransfer(VoteBalanceLog vbl, CashBookTransactionType transactionType)
        {
            var journalTransfer = new InternalJournalTransfers
            {
                VoteBalanceId = vbl.VoteBalanceId,
                VoteDetailId = vbl.VoteDetailId,
                SabhaId = vbl.SabhaId,
                Year = vbl.Year,
                Month = vbl.Month,
                Code = vbl.Code,
                SubCode = vbl.SubCode,
                Description = vbl.Description,
                Status = vbl.Status,
                TransactionType = vbl.TransactionType,
                ModulePrimaryKey = vbl.ModulePrimaryKey,
                AppCategory = vbl.AppCategory,
                CreateBy = vbl.UpdatedBy,
                CreateAt = vbl.UpdatedAt,
                SystemActionAt = vbl.SystemActionAt
            };

            if (transactionType == CashBookTransactionType.CREDIT)
            {
                journalTransfer.Credit = vbl.ExchangedAmount;
            }
            else
            {
                journalTransfer.Debit = vbl.ExchangedAmount;
            }

            _unitOfWork.InternalJournalTransfers.AddAsync(journalTransfer);

        }
    }
}
