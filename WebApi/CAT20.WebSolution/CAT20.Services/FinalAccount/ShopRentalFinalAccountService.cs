using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
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

namespace CAT20.Services.FinalAccount
{
    public class ShopRentalFinalAccountService : IShopRentalFinalAccountService
    {
        private readonly IMapper _mapper;
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IOfficeService _officeService;
        private readonly IVoteBalanceService _voteBalanceService;

        public ShopRentalFinalAccountService(IMapper mapper, IVoteUnitOfWork unitOfWork, IOfficeService officeService, IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _officeService = officeService;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token)
        {
            try
            {
                var ShopWithVoteAssign  = await _unitOfWork.Shops.GetForUpdateVoteAssignForFinalAccounting(sabhaId);

                foreach (var shop in ShopWithVoteAssign)
                {
                    var cvLYA = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.LastYearArreasAmountVoteId);

                    if (cvLYA!=null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvLYA.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.LastYearArreasAmountVoteDetailId = voteAssign.VoteId;
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

                    var cvLYF = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.LastYearFineAmountVoteId);

                    if (cvLYF != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvLYF.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.LastYearFineAmountVoteDetailId = voteAssign.VoteId;
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


                    var cvTYA = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.ThisYearArrearsAmountVoteId);

                    if (cvTYA != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvTYA.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.ThisYearArrearsAmountVoteDetailId = voteAssign.VoteId;
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


                    var cvTYF = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.ThisYearFineAmountVoteId);

                    if (cvTYF != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvTYF.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.ThisYearFineAmountVoteDetailId = voteAssign.VoteId;
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

                    var cvRNT = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.PropertyRentalVoteId);

                    if (cvRNT != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvRNT.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.PropertyRentalVoteDetailId = voteAssign.VoteId;
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

                    var cvSVR = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.ServiceChargeAmountVoteId);

                    if (cvSVR != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvSVR.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.ServiceChargeAmountVoteDetailId = voteAssign.VoteId;
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


                    var cvSVRA = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.ServiceChargeArreasAmountVoteId);

                    if (cvSVRA != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvSVRA.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.ServiceChargeArreasAmountVoteDetailId = voteAssign.VoteId;
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

                    var cvOVP = await _unitOfWork.CustomVoteDetails.GetById(shop.VoteAssign!.OverPaymentAmountVoteId);

                    if (cvOVP != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(cvOVP.VoteAssignmentId);

                        if (voteAssign != null)
                        {
                            shop.VoteAssign.OverPaymentAmountVoteDetailId = voteAssign.VoteId;
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

                    /************/

                   
                 



                }
                await _unitOfWork.CommitAsync();
                return true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(bool,string)> ShopInit(int sabhaId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {



                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var shops = await _unitOfWork.Shops.GetYearEndProcessForFinalAccount(sabhaId);



                        var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(sabhaId);

                        var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, sabhaId, session.StartAt.Year);

                        if (accumulatedFundBalance == null)
                        {
                            accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                        }




                        foreach (var shop in shops)
                        {


                            /*
                             * ii =Last Year Warrant
                             * i = Last Year Arrears
                             * iv = This Year Warrant
                             * iii = This Year Arrears
                             * 

                            1	Last year Warent
                            2	Last year Arrears
                            3	This year Warent
                            4	This Year Arrears
                            5	Tax payment
                            6	Over Payment

                             */


                            var voteBalanceLYFine = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.LastYearFineAmountVoteDetailId, sabhaId, session.StartAt.Year);

                            if (voteBalanceLYFine == null)
                            {
                                voteBalanceLYFine = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.LastYearFineAmountVoteDetailId, session, token);


                            }

                            if (voteBalanceLYFine != null)
                            {

                                voteBalanceLYFine.Debit += (decimal)shop.OpeningBalance.LastYearFineAmount!;
                                voteBalanceLYFine.UpdatedBy = token.userId;
                                voteBalanceLYFine.UpdatedAt = session.StartAt;
                                voteBalanceLYFine.SystemActionAt = DateTime.Now;
                                voteBalanceLYFine.ExchangedAmount = (decimal)shop.OpeningBalance.LastYearFineAmount!;


                                voteBalanceLYFine.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                voteBalanceLYFine.CreditDebitRunningBalance = voteBalanceLYFine.Debit - voteBalanceLYFine.Credit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYFine);
                                voteBalanceLYFine.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP LYF O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Last year Warrant");
                            }


                            /******************/

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)shop.OpeningBalance.LastYearFineAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)shop.OpeningBalance.LastYearFineAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP LYF O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/



                            var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.LastYearArreasAmountVoteDetailId, sabhaId, session.StartAt.Year);


                            if (voteBalanceLYArrears == null)
                            {
                                voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.LastYearArreasAmountVoteDetailId, session, token);


                            }

                            if (voteBalanceLYArrears != null)
                            {
                                voteBalanceLYArrears.Debit += (decimal)shop.OpeningBalance.LastYearArrearsAmount!;
                                voteBalanceLYArrears.UpdatedBy = token.userId;
                                voteBalanceLYArrears.UpdatedAt = session.StartAt;
                                voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                                voteBalanceLYArrears.ExchangedAmount = (decimal)shop.OpeningBalance.LastYearArrearsAmount!;

                                voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                                voteBalanceLYArrears.ExchangedAmount = 0;


                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP LYA O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Last year Arrears");
                            }


                            /******************/

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)shop.OpeningBalance.LastYearArrearsAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)shop.OpeningBalance.LastYearArrearsAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP LYA O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTYFine = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearFineAmountVoteDetailId, sabhaId, session.StartAt.Year);


                            if (voteBalanceTYFine == null)
                            {
                                voteBalanceTYFine = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ThisYearFineAmountVoteDetailId, session, token);


                            }

                            if (voteBalanceTYFine != null)
                            {

                                voteBalanceTYFine.Debit += (decimal)shop.OpeningBalance.ThisYearFineAmount!;
                                voteBalanceTYFine.UpdatedBy = token.userId;
                                voteBalanceTYFine.UpdatedAt = session.StartAt;
                                voteBalanceTYFine.SystemActionAt = DateTime.Now;
                                voteBalanceTYFine.ExchangedAmount = (decimal)shop.OpeningBalance.ThisYearFineAmount!;

                                voteBalanceTYFine.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceTYFine.CreditDebitRunningBalance = voteBalanceTYFine.Debit - voteBalanceTYFine.Credit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYFine);
                                voteBalanceTYFine.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP TYF O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/
                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found This year Warrant");
                            }


                            /******************/

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)shop.OpeningBalance.ThisYearFineAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)shop.OpeningBalance.ThisYearFineAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP TYF O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearArrearsAmountVoteDetailId, sabhaId, session.StartAt.Year);


                            if (voteBalanceTYArrears == null)
                            {
                                voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ThisYearArrearsAmountVoteDetailId, session, token);


                            }

                            if (voteBalanceTYArrears != null)
                            {

                                voteBalanceTYArrears.Debit += (decimal)shop.OpeningBalance.ThisYearArrearsAmount!;
                                voteBalanceTYArrears.UpdatedBy = token.userId;
                                voteBalanceTYArrears.UpdatedAt = session.StartAt;
                                voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                                voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                                voteBalanceTYArrears.ExchangedAmount = (decimal)shop.OpeningBalance.ThisYearArrearsAmount!;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                                voteBalanceTYArrears.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP TYA O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/
                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For This Year Arrears");
                            }


                            /******************/

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)shop.OpeningBalance.ThisYearArrearsAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)shop.OpeningBalance.ThisYearArrearsAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP TYA O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            //var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            //if (voteBalanceIncome == null)
                            //{
                            //    voteBalanceIncome = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            //}


                            //if (voteBalanceIncome != null)
                            //{
                            //    voteBalanceIncome.Debit += (decimal)IncomeAmount!;
                            //    voteBalanceIncome.UpdatedBy = token.userId;
                            //    voteBalanceIncome.UpdatedAt = session.StartAt;
                            //    voteBalanceIncome.SystemActionAt = DateTime.Now;

                            //    voteBalanceIncome.TransactionType = VoteBalanceTransactionTypes.Billing;
                            //    voteBalanceIncome.CreditDebitRunningBalance = voteBalanceIncome.Debit - voteBalanceIncome.Credit;
                            //    voteBalanceIncome.ExchangedAmount = (decimal)IncomeAmount!;

                            //    /*vote balance log */
                            //    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceIncome);
                            //    voteBalanceIncome.ExchangedAmount = 0m;




                            //    vtbLog.Year = session.StartAt.Year;
                            //    vtbLog.Code = "ASM Taxing O/B";
                            //    vtbLog.SubCode = asmt.AssessmentNo;
                            //    vtbLog.OfficeId = asmt.OfficeId;
                            //    vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            //    vtbLog.ModulePrimaryKey = asmt.Id;

                            //    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            //    CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            //    /**********/


                            //}
                            //else
                            //{
                            //    throw new Exception("Vote Balance Not Found For Tax Payment");
                            //}

                            var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.OverPaymentAmountVoteDetailId, sabhaId, session.StartAt.Year);


                            if (voteBalanceOverPay == null)
                            {
                                voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.OverPaymentAmountVoteDetailId, session, token);


                            }

                            if (voteBalanceOverPay != null)
                            {
                                voteBalanceOverPay.Credit += (decimal)shop.OpeningBalance.OverPaymentAmount!;
                                voteBalanceOverPay.UpdatedBy = token.userId;
                                voteBalanceOverPay.UpdatedAt = session.StartAt;
                                voteBalanceOverPay.SystemActionAt = DateTime.Now;

                                voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                                voteBalanceOverPay.ExchangedAmount = (decimal)shop.OpeningBalance.OverPaymentAmount!;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                                voteBalanceOverPay.ExchangedAmount = 0m;


                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP OverPay O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Over Payment");
                            }

                            /******************/

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Debit += (decimal)(decimal)shop.OpeningBalance.OverPaymentAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)(decimal)shop.OpeningBalance.OverPaymentAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP OverPay O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceServiceArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ServiceChargeArreasAmountVoteDetailId, sabhaId, session.StartAt.Year);


                            if (voteBalanceServiceArrears == null)
                            {
                                voteBalanceServiceArrears = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ServiceChargeArreasAmountVoteDetailId, session, token);


                            }


                            if (voteBalanceServiceArrears != null)
                            {

                                //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                                voteBalanceServiceArrears.Credit += (decimal)shop.OpeningBalance.ServiceChargeArreasAmount!;
                                voteBalanceServiceArrears.UpdatedBy = token.userId;
                                voteBalanceServiceArrears.UpdatedAt = session.StartAt;
                                voteBalanceServiceArrears.SystemActionAt = DateTime.Now;

                                voteBalanceServiceArrears.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                voteBalanceServiceArrears.ExchangedAmount = (decimal)shop.OpeningBalance.OverPaymentAmount!;




                                voteBalanceServiceArrears.CreditDebitRunningBalance = voteBalanceServiceArrears.Credit - voteBalanceServiceArrears.Debit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceServiceArrears);
                                voteBalanceServiceArrears.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP SVC O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found Tax Billing");
                            }

                            if (accumulatedFundBalance != null)
                            {

                                accumulatedFundBalance.Debit += (decimal)(decimal)shop.OpeningBalance.ServiceChargeArreasAmount!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)(decimal)shop.OpeningBalance.ServiceChargeArreasAmount!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "SHP SVCA O/B";
                                vtbLog.SubCode = shop.AgreementNo;
                                vtbLog.AppCategory = AppCategory.Shop_Rental;
                                vtbLog.ModulePrimaryKey = shop.Id;
                                vtbLog.OfficeId = shop.OfficeId;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/


                        }

                    }
                    else
                    {
                        throw new Exception("Session Not Found");
                    }

                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return (true, "Update SuccessFull");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return (false, ex.Message.ToString());

                }
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
