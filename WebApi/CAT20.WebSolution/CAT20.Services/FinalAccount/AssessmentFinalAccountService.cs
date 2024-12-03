using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class AssessmentFinalAccountService : IAssessmentFinalAccountService
    {
        private readonly IMapper _mapper;
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;





        public AssessmentFinalAccountService(IMapper mapper,IVoteUnitOfWork unitOfWork, IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token)
        {
            try
            {
                var votesAssign = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(sabhaId);

                foreach(var item in votesAssign)
                {

                    var customVotes = await _unitOfWork.CustomVoteDetails.GetById(item.VoteAssignmentDetailId!.Value);

                    if(customVotes != null)
                    {
                        var voteAssign = await _unitOfWork.VoteAssignments.GetById(customVotes.VoteAssignmentId);

                        if (voteAssign != null)
                        {

                            item.VoteDetailId = voteAssign.VoteId;
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

        public async Task<(bool,string?)> AssessmentInti(int sabhaId,HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {



                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var assmts = await _unitOfWork.Assessments.GetYearEndProcessForFinalAccount(sabhaId);

                        var votes = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(sabhaId);


                        var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(sabhaId);

                        var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, sabhaId, session.StartAt.Year);

                        if (accumulatedFundBalance == null)
                        {
                            accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                        }




                        foreach (var asmt in assmts)
                        {

                            decimal? LYArrears = 0m;
                            decimal? LYWarrant = 0m;
                            decimal? TYArrears = 0m;
                            decimal? TYWarrant = 0m;
                            decimal? OverPay = 0m;
                            decimal? IncomeAmount = asmt.AssessmentBalance!.AnnualAmount;
                            decimal? WarrantAmount = asmt.AssessmentBalance!.Q1!.Warrant + asmt.AssessmentBalance.Q2!.Warrant + asmt.AssessmentBalance.Q3!.Warrant + asmt.AssessmentBalance.Q4!.Warrant;

                            var sysAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustment).LastOrDefault();
                            var jnlAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustment).LastOrDefault();
                            var init = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Init).FirstOrDefault();


                            //var voteBalance = await unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteId, sabhaId, item.Year);

                            if (sysAdj != null)
                            {

                                LYArrears = sysAdj.LYArrears;
                                LYWarrant = sysAdj.LYWarrant;
                                TYArrears = sysAdj.TYArrears;
                                TYWarrant = sysAdj.TYWarrant;
                                OverPay = sysAdj.RunningOverPay;

                            }
                            else if (jnlAdj != null)
                            {
                                LYArrears = jnlAdj.LYArrears;
                                LYWarrant = jnlAdj.LYWarrant;
                                TYArrears = jnlAdj.TYArrears;
                                TYWarrant = jnlAdj.TYWarrant;
                                OverPay = jnlAdj.RunningOverPay;

                            }
                            else if (init != null)
                            {
                                LYArrears = init.LYArrears;
                                LYWarrant = init.LYWarrant;
                                TYArrears = init.TYArrears;
                                TYWarrant = init.TYWarrant;
                                OverPay = init.RunningOverPay;






                            }

                            if (asmt.AssessmentBalance!.Q1!.StartDate.HasValue && asmt.AssessmentBalance!.Q1!.StartDate == asmt.AssessmentBalance.Q1.EndDate)
                            {
                                IncomeAmount -= asmt.AssessmentBalance.Q1.Amount;

                            }

                            if (asmt.AssessmentBalance!.Q2!.StartDate.HasValue && asmt.AssessmentBalance!.Q2!.StartDate == asmt.AssessmentBalance.Q2.EndDate)
                            {
                                IncomeAmount -= asmt.AssessmentBalance.Q2.Amount;
                            }

                            if (asmt.AssessmentBalance!.Q3!.StartDate.HasValue && asmt.AssessmentBalance!.Q3!.StartDate == asmt.AssessmentBalance.Q3.EndDate)
                            {
                                IncomeAmount -= asmt.AssessmentBalance.Q3.Amount;
                            }

                            if (asmt.AssessmentBalance!.Q4!.StartDate.HasValue && asmt.AssessmentBalance!.Q4!.StartDate == asmt.AssessmentBalance.Q4.EndDate)
                            {
                                IncomeAmount -= asmt.AssessmentBalance.Q4.Amount;
                            }


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


                            var voteBalanceLYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);

                            if (voteBalanceLYWarrant == null)
                            {
                                voteBalanceLYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            }

                            if (voteBalanceLYWarrant != null)
                            {

                                voteBalanceLYWarrant.Debit += (decimal)LYWarrant!;
                                voteBalanceLYWarrant.UpdatedBy = token.userId;
                                voteBalanceLYWarrant.UpdatedAt = session.StartAt;
                                voteBalanceLYWarrant.SystemActionAt = DateTime.Now;
                                voteBalanceLYWarrant.ExchangedAmount = (decimal)LYWarrant!;


                                voteBalanceLYWarrant.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant.Debit - voteBalanceLYWarrant.Credit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYWarrant);
                                voteBalanceLYWarrant.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM LYW O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);


                                /**********/

                            }
                            else
                            {
                              throw new Exception("Vote Balance Not Found For Last year Warrant");
                            }


                            /******************/

                            if (accumulatedFund != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)LYWarrant!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM LYW O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                               

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/



                            var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceLYArrears == null)
                            {
                                voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


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
                                vtbLog.Code = "ASM LYA O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;
                                vtbLog.OfficeId = asmt.OfficeId;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;

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
                                accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM LYA O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                               


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceTYWarrant == null)
                            {
                                voteBalanceTYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            }

                            if (voteBalanceTYWarrant != null)
                            {

                                voteBalanceTYWarrant.Debit += (decimal)TYWarrant! +(decimal)WarrantAmount!;
                                voteBalanceTYWarrant.UpdatedBy = token.userId;
                                voteBalanceTYWarrant.UpdatedAt = session.StartAt;
                                voteBalanceTYWarrant.SystemActionAt = DateTime.Now;
                                voteBalanceTYWarrant.ExchangedAmount = (decimal)TYWarrant! + (decimal)WarrantAmount!;

                                voteBalanceTYWarrant.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                                voteBalanceTYWarrant.CreditDebitRunningBalance = voteBalanceTYWarrant.Debit - voteBalanceTYWarrant.Credit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYWarrant);
                                voteBalanceTYWarrant.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM TYW O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/
                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found This year Warrant");
                            }


                            /******************/

                            if (accumulatedFund != null)
                            {

                                accumulatedFundBalance.Credit += (decimal)TYWarrant!;
                                accumulatedFundBalance.UpdatedBy = token.userId;
                                accumulatedFundBalance.UpdatedAt = session.StartAt;
                                accumulatedFundBalance.SystemActionAt = DateTime.Now;
                                accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM TYW O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                               


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceTYArrears == null)
                            {
                                voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


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
                                vtbLog.Code = "ASM TYA O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
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
                                accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                                accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                                accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                                accumulatedFundBalance.ExchangedAmount = 0;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM TYA O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceIncome == null)
                            {
                                voteBalanceIncome = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            }


                            if (voteBalanceIncome != null)
                            {
                                voteBalanceIncome.Debit += (decimal)IncomeAmount!;
                                voteBalanceIncome.UpdatedBy = token.userId;
                                voteBalanceIncome.UpdatedAt = session.StartAt;
                                voteBalanceIncome.SystemActionAt = DateTime.Now;

                                voteBalanceIncome.TransactionType = VoteBalanceTransactionTypes.Billing;
                                voteBalanceIncome.CreditDebitRunningBalance = voteBalanceIncome.Debit - voteBalanceIncome.Credit;
                                voteBalanceIncome.ExchangedAmount = (decimal)IncomeAmount!;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceIncome);
                                voteBalanceIncome.ExchangedAmount =0m;




                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM Taxing O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;
                                vtbLog.OfficeId = asmt.OfficeId;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                                /**********/


                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Tax Payment");
                            }

                            var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v=>v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceOverPay == null)
                            {
                                voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


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
                                vtbLog.Code = "ASM OverPay O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;
                                vtbLog.OfficeId = asmt.OfficeId;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;

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
                                vtbLog.Code = "ASM OverPay O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;

                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                              


                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Accumulated Fund");
                            }

                            /******************/

                            var voteBalanceTaxBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);


                            if (voteBalanceTaxBilling == null)
                            {
                                voteBalanceTaxBilling = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            }


                            if (voteBalanceTaxBilling != null)
                            {

                                //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                                voteBalanceTaxBilling.Credit += (decimal)IncomeAmount!;
                                voteBalanceTaxBilling.UpdatedBy = token.userId;
                                voteBalanceTaxBilling.UpdatedAt = session.StartAt;
                                voteBalanceTaxBilling.SystemActionAt = DateTime.Now;

                                voteBalanceTaxBilling.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                                voteBalanceTaxBilling.ExchangedAmount = (decimal)IncomeAmount!;




                                voteBalanceTaxBilling.CreditDebitRunningBalance = voteBalanceTaxBilling.Credit - voteBalanceTaxBilling.Debit;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTaxBilling);
                                voteBalanceTaxBilling.ExchangedAmount = 0m;

                                vtbLog.Year  = session.StartAt.Year;
                                vtbLog.Code  = "ASM Billing O/B";
                                vtbLog.SubCode  = asmt.AssessmentNo;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;
                                vtbLog.OfficeId = asmt.OfficeId;
                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found Tax Billing");
                            }

                            var voteBalanceWarrantBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, sabhaId, session.StartAt.Year);

                            if(voteBalanceWarrantBilling == null)
                            {
                                voteBalanceWarrantBilling =  await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                            }


                            if (voteBalanceWarrantBilling != null)
                            {


                                voteBalanceWarrantBilling.Credit += (decimal)WarrantAmount!;
                                voteBalanceWarrantBilling.UpdatedBy = token.userId;
                                voteBalanceWarrantBilling.UpdatedAt = session.StartAt;
                                voteBalanceWarrantBilling.SystemActionAt = DateTime.Now;

                                voteBalanceWarrantBilling.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                                voteBalanceWarrantBilling.CreditDebitRunningBalance = voteBalanceWarrantBilling.Credit - voteBalanceWarrantBilling.Debit;
                                voteBalanceWarrantBilling.ExchangedAmount = (decimal)WarrantAmount!;

                                /*vote balance log */
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceWarrantBilling);
                                voteBalanceWarrantBilling.ExchangedAmount = 0m;

                                vtbLog.Year = session.StartAt.Year;
                                vtbLog.Code = "ASM Warranting O/B";
                                vtbLog.SubCode = asmt.AssessmentNo;
                                vtbLog.OfficeId = asmt.OfficeId;
                                vtbLog.AppCategory = AppCategory.Assessment_Tax;
                                vtbLog.ModulePrimaryKey = asmt.Id;

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                                /**********/

                            }
                            else
                            {
                                throw new Exception("Vote Balance Not Found For Warrant Billing");
                            }
                        }

                    }

                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return (true,"Update SuccessFull");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return (false,ex.Message.ToString());

                }
            }
        }

        public async Task<bool> FixMissedBalance(int sabhaId, HTokenClaim token)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
            //    var votes = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(sabhaId);

            //    foreach (var vt in votes)
            //    {
            //        if (vt.PaymentTypeId == 1)
            //        {
            //            var voteBalanceLYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceLYWarrantLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceLYWarrant.Id!.Value);

            //            foreach (var item in voteBalanceLYWarrantLog)
            //            {
            //                item.Debit = item.Credit;
            //                item.Credit = 0m;
            //                item.CreditDebitRunningBalance = item.Debit - item.Credit;
            //            }



            //        }
            //        else if (vt.PaymentTypeId == 2)
            //        {
            //            var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceLYArrearsLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceLYArrears.Id!.Value);

            //            foreach (var item in voteBalanceLYArrearsLog)
            //            {
            //                item.Debit = item.Credit;
            //                item.Credit = 0m;

            //                item.CreditDebitRunningBalance = item.Debit - item.Credit;
            //            }

            //        }
            //        else if (vt.PaymentTypeId == 3)
            //        {
            //            var voteBalanceTYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceTYWarrantLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceTYWarrant.Id!.Value);

            //            foreach (var item in voteBalanceTYWarrantLog)
            //            {
            //                item.Debit = item.Credit;
            //                item.Credit = 0m;
            //                item.CreditDebitRunningBalance = item.Debit - item.Credit;
            //            }
            //        }
            //        else if (vt.PaymentTypeId == 4)
            //        {
            //            var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceTYArrearsLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceTYArrears.Id!.Value);

            //            foreach (var item in voteBalanceTYArrearsLog)
            //            {
            //                item.Debit = item.Credit;
            //                item.Credit = 0m;
            //                item.CreditDebitRunningBalance = item.Debit - item.Credit;
            //            }
            //        }
            //        else if (vt.PaymentTypeId == 5)
            //        {
            //            var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceIncomeLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceIncome.Id!.Value);

            //            foreach (var item in voteBalanceIncomeLog)
            //            {
            //                item.Debit = item.Credit;
            //                item.Credit = 0m;
            //                item.CreditDebitRunningBalance = item.Debit - item.Credit;
            //            }
            //        }
            //        else if (vt.PaymentTypeId == 6)
            //        {
            //            var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceOverPayLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceOverPay.Id!.Value);

            //            foreach (var item in voteBalanceOverPayLog)
            //            {
            //                item.CreditDebitRunningBalance = item.Credit - item.Debit;
            //            }
            //        }
            //        else if (vt.PaymentTypeId == 7)
            //        {
            //            var voteBalanceTaxBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);

            //            var voteBalanceTaxBillingLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceTaxBilling.Id!.Value);

            //            foreach (var item in voteBalanceTaxBillingLog)
            //            {
            //                item.CreditDebitRunningBalance = item.Credit - item.Debit;
            //            }
            //        }
            //        else if (vt.PaymentTypeId == 8)
            //        {
            //            var voteBalanceWarrantBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vt.VoteDetailId.Value, sabhaId, session.StartAt.Year);


            //            var voteBalanceWarrantBillingLog = await _unitOfWork.VoteBalanceLogs.GetVoteBalanceLogs(voteBalanceWarrantBilling.Id!.Value);

            //            foreach (var item in voteBalanceWarrantBillingLog)
            //            {
            //                item.CreditDebitRunningBalance = item.Credit - item.Debit;
            //            }


            //        }


            //    }




            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        private void CreateJournalTransfer(VoteBalanceLog vbl,CashBookTransactionType transactionType)
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

            if(transactionType == CashBookTransactionType.CREDIT)
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
