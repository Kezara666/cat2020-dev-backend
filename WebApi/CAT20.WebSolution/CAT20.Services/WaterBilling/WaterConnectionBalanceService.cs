using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using Irony.Parsing;
using System.Transactions;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.WaterBilling
{
    public class WaterConnectionBalanceService : IWaterConnectionBalanceService
    {
        private readonly IMapper _mapper;
        private readonly IWaterBillingUnitOfWork _unitOfWork;
        private readonly IWaterConnectionBalanceHistoryService _balanceHistoryService;
        private readonly IVoteBalanceService _voteBalanceService;

        public WaterConnectionBalanceService( IMapper mapper, IWaterBillingUnitOfWork unitOfWork, IWaterConnectionBalanceHistoryService balanceHistoryService,IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _balanceHistoryService = balanceHistoryService;
            _voteBalanceService = voteBalanceService;
        }
        public async Task<IEnumerable<WaterConnection>> GetToBillProcess(int year, int month, List<int> subraodIds)
        {
            return await _unitOfWork.Balances.GetToBillProcess(year, month, subraodIds);
        }


        public async Task<bool> ProcessBills(int year, int month, List<int> subraodIds, int userId,HTokenClaim token)
        {
            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if(session == null)
                {
                    throw new Exception("No Active Session Found");
                }

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                   if(!await  UpdateBillsWithOverPayments(year, month, subraodIds[0], userId))
                    {
                        throw new Exception("Update Bills With Over Payments Failed");
                    }

                    scope.Complete();
                }



                var wcs = await _unitOfWork.Balances.GetToBillProcess(year, month, subraodIds);
                var newBalances = new List<WaterConnectionBalance>();

                var today = DateOnly.FromDateTime(DateTime.Now);
                var todayTime = DateTime.Now;

                foreach (var wc in wcs)
                {
                    wc.Balances = wc.Balances.OrderBy(b => b.Id).ToList();

                    decimal? balnceBf = 0.0m;
                    decimal? payments = 0.0m;

                    var billingAccount = await _unitOfWork.Partners.GetByIdAsync(wc.BillingId!.Value);
                    var connectioninfo = await _unitOfWork.MeterConnectInfos.GetById(wc.ConnectionId!);
                    foreach (var b in wc.Balances!)
                    {
                        //balnceBf += b.WaterCharge + b.FixedCharge + b.VATAmount + b.AdditionalCharges;
                        if (b.IsCompleted == false)
                        {
                            balnceBf += b.ThisMonthCharge + b.VATAmount;
                            payments += b.OnTimePaid + b.LatePaid;
                        }
                    }


                       //var x = await CalculatePayments((int)wc.Id,  session.StartAt , 0, false, false, -1);


                        var newBal = new WaterConnectionBalance
                    {
                        //Id = null,
                        WcPrimaryId = wc.Id!.Value,
                        BarCode = $"{year}-{month}-{wc.ConnectionId}",
                        InvoiceNo = " ",
                        Year = year,
                        Month = month,
                        FromDate = wc.Balances.LastOrDefault()!.ToDate!.Value,
                        ToDate = null,
                        BillProcessDate = today,

                        MeterNo = connectioninfo.MeterNo,
                        ConnectionNo = connectioninfo.ConnectionNo,
                        MeterCondition = 1,
                        PreviousMeterReading = wc.Balances.LastOrDefault()!.ThisMonthMeterReading!.Value,
                        ThisMonthMeterReading = null,

                        WaterCharge = 0,
                        FixedCharge = 0,

                        VATRate = 0,
                        VATAmount = 0,
                        ThisMonthCharge = 0,


                        TotalDue = 0,

                        OnTimePaid = 0,
                        LatePaid = 0,
                        Payments = 0,

                        IsCompleted = false,
                        IsFilled = false,
                        IsProcessed = false,


                        LastBillYearMonth = wc.Balances.LastOrDefault()!.Year.ToString() + '-' + wc.Balances.LastOrDefault()!.Month.ToString("D2"),
                        PrintBalanceBF = balnceBf,
                        PrintLastBalance = balnceBf - (payments + wc.RunningOverPay),
                        PrintBillingDetails =billingAccount.Name + "," + billingAccount.Street1 + "," + billingAccount.City + "," + billingAccount.Zip,
                        PrintLastMonthPayments = wc.Balances.LastOrDefault()!.Payments,
                        NoOfPayments = 0,

                        //CreatedAt
                        CreatedBy = userId,

                        //LastYearArrears =x.hWaterBillBalance.LYArrears,
                        //ThisYearArrears =x.hWaterBillBalance.TYArrears,

                        };
                    wc.Balances.LastOrDefault()!.IsProcessed = true;

                    await _unitOfWork.Balances.AddAsync(newBal);

                    newBalances.Add(newBal);

                }


                await _unitOfWork.CommitAsync();

                await _balanceHistoryService.InitBalanceHistory(newBalances, userId);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }


        }
        public async Task<WaterConnection> GetForAddReadingByConnectionId(int officeId, string connectionId)
        {
            return await _unitOfWork.Balances.GetForAddReadingByConnectionId(officeId, connectionId);
        }


        public async Task<WaterConnection> GetForAddReadingByBarCode(int officeId, string barCode)
        {
            return await _unitOfWork.Balances.GetForAddReadingByBarCode(officeId, barCode);
        }

        public async Task<WaterConnection> GetForAddReadingByConnectionNo(int officeId, string connectionNo)
        {
            return await _unitOfWork.Balances.GetForAddReadingByConnectionNo(officeId, connectionNo);
        }

        public Task<WaterConnection> GetForAddReadingById(int wcId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> AddMeterReading(WaterConnectionBalance obj ,HTokenClaim token)
        {
            try
            {
                  var  bal = await _unitOfWork.Balances.GetByIdAsync(obj.Id.Value);

                if (bal != null)
                {
                    bal.ThisMonthMeterReading = obj.ThisMonthMeterReading;


                    if (obj.ToDate == null)
                    {
                        bal.ToDate = DateOnly.FromDateTime(DateTime.Now);
                        obj.ToDate = DateOnly.FromDateTime(DateTime.Now);
                            
                    }
                    else
                    {
                        bal.ToDate = obj.ToDate;
                    }
                    bal.ReadBy = obj.ReadBy;


                    bal.WaterCharge = obj.WaterCharge;
                    bal.FixedCharge = obj.FixedCharge;
                    bal.VATRate = (obj.VATRate / 100);
                    bal.VATAmount = obj.VATAmount;
                    bal.ThisMonthCharge = obj.ThisMonthCharge;
                    bal.ThisMonthChargeWithVAT = obj.ThisMonthChargeWithVAT;
                    bal.TotalDue = obj.TotalDue;



                    bal.CalculationString = obj.CalculationString;
                    bal.IsFilled = true;
                    bal.UpdatedBy = obj.UpdatedBy;
                    bal.UpdatedAt = DateTime.Now;

                    if (!await _balanceHistoryService.CreateBalanceHistory(bal, WbTransactionsType.AddMeterReading, obj.ReadBy!.Value))
                    {
                        throw new Exception("Balance History Creation Failed");
                    }
                }


                //var wc = await _unitOfWork.WaterConnections.GetByIdAsync(obj.WcPrimaryId!.Value);
                var wc = await _unitOfWork.WaterConnections.GetByIdWithSubRoad(bal.WcPrimaryId!.Value);
                if (wc != null)
                {
                    wc.RunningVatRate = (obj.VATRate / 100);
                }
                else
                {
                    throw new Exception("Water Connection Not Found");
                }


                await _unitOfWork.CommitAsync();

                if (token.IsFinalAccountsEnabled==1)
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    { 
                      throw new FinalAccountException("No Active Session Found");
                    }

                        var votes = await _unitOfWork.VoteAssigns.GetAllForWaterProject(wc.SubRoad.WaterProjectId);

                    var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                    if (voteBalanceTYArrears == null)
                    {
                        voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                    }

                    if (voteBalanceTYArrears != null)
                    {

                        voteBalanceTYArrears.Debit += (decimal)obj.ThisMonthCharge;
                        voteBalanceTYArrears.UpdatedBy = token.userId;
                        voteBalanceTYArrears.UpdatedAt = session.StartAt;
                        voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                        voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                        voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                        voteBalanceTYArrears.ExchangedAmount = (decimal)obj.ThisMonthCharge;

                        /*vote balance log */
                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                        voteBalanceTYArrears.ExchangedAmount = 0m;

                        vtbLog.Year = session.StartAt.Year;
                        vtbLog.Code = $"WTR Billing -{bal.Month}";
                        vtbLog.Month = bal.Month;
                        vtbLog.SubCode = wc.MeterConnectInfo!.ConnectionNo;
                        vtbLog.OfficeId = wc.OfficeId;
                        vtbLog.AppCategory = AppCategory.Water_Bill;
                        vtbLog.ModulePrimaryKey = wc.Id;
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                        CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                        /**********/
                    }
                    else
                    {
                        throw new Exception("Vote Balance Not Found For This Year Arrears");
                    }


                    var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 5).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);

                    if (votes != null)
                    {

                        if (voteBalanceIncome == null)
                        {
                            voteBalanceIncome = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceIncome != null)
                        {
                            voteBalanceIncome.Credit += (decimal)obj.ThisMonthCharge;
                            voteBalanceIncome.UpdatedBy = token.userId;
                            voteBalanceIncome.UpdatedAt = session.StartAt;
                            voteBalanceIncome.SystemActionAt = DateTime.Now;

                            voteBalanceIncome.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceIncome.CreditDebitRunningBalance = voteBalanceIncome.Credit - voteBalanceIncome.Debit;
                            voteBalanceIncome.ExchangedAmount = (decimal)obj.ThisMonthCharge;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceIncome);
                            voteBalanceIncome.ExchangedAmount = 0m;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = $"WTR Billing -{bal.Month}";
                            vtbLog.Month = bal.Month;
                            vtbLog.SubCode = wc.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = wc.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = wc.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Over Payment");
                        }
                    }
                    else
                    {
                        throw new Exception("Vote Not Found For Over Payment");
                    }
                }



                return true;
            }
            catch (Exception ex)
            {


                return false;
            }
        }


        private async Task<bool> UpdateBillsWithOverPayments(int year,int month,int subroadId , int userId)
        {
            try
            {
                var  wcids = await  _unitOfWork.WaterConnections.HasOverPayments(subroadId);

                foreach (var wcId in wcids)
                {
                    if(wcId != null){
                        var x = await CalculatePayments(wcId!.Value, new DateTime(year, month-1, 1), 0, true, true, userId);
                        //await _wb_unitOfWork.CommitAsync();
                    }
                }


                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

       

        public async Task<(int totalCount, IEnumerable<WaterConnectionBalance> list)> GetPreviousBills(int wcId, int pageNo)
        {
            try
            {
                return await _unitOfWork.Balances.GetPreviousBills(wcId, pageNo);
            }
            catch (Exception ex)
            {
                List<WaterConnectionBalance> list1 = new List<WaterConnectionBalance>();
                return (0, list1);
            }
        }

        public async Task<IEnumerable<WaterConnectionBalance>> GetForPrintBills(int year, int month, List<int> subroadIds)
        {
            return await _unitOfWork.Balances.GetForPrintBills(year, month, subroadIds);    
        }

        public async Task<IEnumerable<WaterConnection>> GetProcessedBills(int year, int month, List<int> subroadIds)
        {
            return await _unitOfWork.Balances.GetProcessedBills(year, month, subroadIds);
        }

        public async Task<WaterConnection> GetForPaymentsByBarCode(int officeId, string barcode)
        {
            return await _unitOfWork.Balances.GetForPaymentsByBarCode(officeId,barcode);
        }

        public async Task<WaterConnection> GetForPaymentsByConnectionId(int officeId, string connectionNo)
        {
            return await _unitOfWork.Balances.GetForPaymentsByConnectionId(officeId, connectionNo)
;
        }

        public async Task<WaterConnection> GetForPaymentsByConnectionNo(int officeId, string connectionNo)
        {
            return await _unitOfWork.Balances.GetForPaymentsByConnectionNo(officeId, connectionNo)
;
        }

        public Task<WaterConnection> GetForPaymentsById(int wcPrimaryId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<WaterConnectionBalance>> CalculatePayments(int wcPrimaryId,decimal amount,bool isPayment)
        public async Task<(decimal? runningOverPay,IEnumerable<WaterConnectionBalance> WaterConnectionBalances, HWaterBillBalance hWaterBillBalance)> CalculatePayments(int wcPrimaryId, DateTime sessionData, decimal amount, bool isPayment, bool isProcess, int userId)
        {



            var wc = await _unitOfWork.Balances.CalculatePayments(wcPrimaryId);

            if (wc != null && wc.Balances!.Count > 0)
            {
                var hWaterbilBalance = new HWaterBillBalance();
                hWaterbilBalance.PayingAmount = amount;

                decimal? remainder = amount;

                foreach (var b in wc.Balances)
                {
                    if (b.IsCompleted == false)
                    {
                        b.Payable = b.ThisMonthCharge - b.ByExcessDeduction - b.OnTimePaid - b.LatePaid;
                        b.PayableVAT = (b.ThisMonthCharge - b.ByExcessDeduction - b.OnTimePaid - b.LatePaid) * b.VATRate;

                        //if (wc.RunningOverPay > 0)
                        //{
                        //    b.Payable -= wc.RunningOverPay;
                        //    b.ByExcessDeduction = wc.RunningOverPay;
                        //    wc.RunningOverPay = 0;
                        //}

                        if (wc.RunningOverPay > 0 && wc.RunningOverPay < b.Payable)
                        {
                            b.Payable -= wc.RunningOverPay;
                            b.ByExcessDeduction = wc.RunningOverPay;
                            wc.RunningOverPay = 0;
                        }
                        else if (wc.RunningOverPay > 0 && wc.RunningOverPay > b.Payable)
                        {
                            wc.RunningOverPay -= b.Payable;
                            b.ByExcessDeduction = b.Payable;
                            b.Payable = 0;
                        }



                        //if (b.Year < sessionData.Year)
                        //{
                        //    hWaterbilBalance.LYArrears += ((b.Payable) - (b.LatePaid + b.OnTimePaid));
                        //}

                        if (b.Year < sessionData.Year)
                        {
                            hWaterbilBalance.LYArrears += b.Payable;
                        }

                        if (b.Year == sessionData.Year && b.Month < sessionData.Month )
                        {
                            hWaterbilBalance.TYArrears += b.Payable;
                        }

                        if (b.Year == sessionData.Year && b.Month == sessionData.Month)
                        {
                            hWaterbilBalance.TMCharge += b.Payable;
                        }

                        if (b.Payable <= remainder)
                        {


                            if (b.Year < sessionData.Year)
                            {
                                hWaterbilBalance.LYArrearsPaying += b.Payable;
                                hWaterbilBalance.LYAPayingVAT += b.PayableVAT;
                                remainder -= b.Payable + b.PayableVAT;

                                b.PayingAmount = b.Payable;
                                b.PayingVatAmount = b.PayableVAT;

                                if (isPayment)
                                {
                                    b.LatePaid += b.Payable + b.PayableVAT;
                                }
                            }

                            if (b.Year == sessionData.Year && b.Month < sessionData.Month)
                            {
                                hWaterbilBalance.TYArrearsPaying += b.Payable;
                                hWaterbilBalance.TYAPayingVAT += b.PayableVAT;
                                remainder -= b.Payable + b.PayableVAT;

                                b.PayingAmount = b.Payable;
                                b.PayingVatAmount = b.PayableVAT;

                                if (isPayment)
                                {
                                    b.LatePaid += b.Payable + b.PayableVAT;
                                }
                            }

                            if (b.Year == sessionData.Year && b.Month == sessionData.Month)
                            {
                                hWaterbilBalance.TMChargePaying += b.Payable;
                                hWaterbilBalance.TMPayingVAT += b.PayableVAT;
                                remainder -= b.Payable + b.PayableVAT; ;

                                b.PayingAmount = b.Payable;
                                b.PayingVatAmount = b.PayableVAT;

                                if (isPayment)
                                {
                                    b.OnTimePaid += b.Payable + b.PayableVAT;
                                }

                            }

                            if (isPayment && !isProcess)
                            {
                                b.NoOfPayments += 1;
                                
                            }
                            if (isPayment)
                            {
                                b.UpdatedBy = userId;
                                b.UpdatedAt = DateTime.Now;
                            }
                            b.IsCompleted = true;
                        }
                        else if (0 < remainder)
                        {
                            decimal? avaiblePaying = remainder / (1 + b.VATRate);
                            avaiblePaying = Math.Round((decimal)avaiblePaying, 2, MidpointRounding.AwayFromZero);

                            decimal? payingVat = remainder - avaiblePaying;

                            if (b.Year < sessionData.Year)
                            {
                                hWaterbilBalance.LYArrearsPaying += avaiblePaying;
                                hWaterbilBalance.LYAPayingVAT += payingVat;

                                b.PayingAmount += avaiblePaying;
                                b.PayingVatAmount += payingVat;

                                if (isPayment)
                                {
                                    b.LatePaid += avaiblePaying + payingVat;
                                }


                            }

                            if (b.Year == sessionData.Year && b.Month < sessionData.Month)
                            {
                                hWaterbilBalance.TYArrearsPaying += avaiblePaying;
                                hWaterbilBalance.TYAPayingVAT += payingVat;

                                b.PayingAmount += avaiblePaying;
                                b.PayingVatAmount += payingVat;

                                if (isPayment)
                                {
                                    b.LatePaid += avaiblePaying + payingVat;
                                }

                            }

                            if (b.Year == sessionData.Year && b.Month == sessionData.Month)
                            {
                                hWaterbilBalance.TMChargePaying += avaiblePaying;
                                hWaterbilBalance.TMPayingVAT += payingVat;

                                b.PayingAmount += avaiblePaying;
                                b.PayingVatAmount += payingVat;

                                if (isPayment)
                                {
                                    b.OnTimePaid += avaiblePaying + payingVat;
                                }

                            }


                            b.IsCompleted = false;
                            remainder = 0;
                            if (isPayment && !isProcess)
                            {
                                b.NoOfPayments += 1;
                                
                            }
                            if (isPayment) {
                                b.UpdatedBy = userId;
                                b.UpdatedAt = DateTime.Now;
                            }
                        }

                       

                    }
                }

                if (isPayment)
                {

                    wc.Balances.Last().Payments += amount;
                }

                wc.Balances.Last().OverPay += remainder;

                //wc.RunningOverPay = remainder+wc.RunningOverPay;
                wc.RunningOverPay += remainder;
                if (!isPayment)
                {
                    wc.Balances.Last().OverPay =0;
                    wc.Balances.Last().OverPay += wc.RunningOverPay;

                }

                if (isProcess){
                    wc.Balances.Last().OverPay = 0;
                    wc.Balances.Last().OverPay += wc.RunningOverPay;
                }

                hWaterbilBalance.OverPayment = remainder / (1 + wc.Balances.Last().VATRate);
                hWaterbilBalance.OverPaymentVAT = remainder - hWaterbilBalance.OverPayment;

                if (isPayment && !isProcess)
                {

                    if (!await _balanceHistoryService.CreateBalanceHistory(wc.Balances.ToList(), WbTransactionsType.Payment, userId))
                    {
                        throw new Exception("Balance History Creation Failed");
                    }
                }
                if (isProcess)
                {
                    if (!await _balanceHistoryService.CreateBalanceHistory(wc.Balances.ToList(), WbTransactionsType.ProcessBill, userId))
                    {
                        throw new Exception("Balance History Creation Failed");
                    }
                }

                return (wc.RunningOverPay,wc.Balances!, hWaterbilBalance);
            }
            else
            {
                return (0m,wc.Balances!, new HWaterBillBalance());
            }
        }

        public async Task<IEnumerable<WaterConnection>> GetPreviousBills(int year, int month, List<int> subroadIds)
        {
            return await _unitOfWork.Balances.GetPreviousBills(year, month, subroadIds);
        }

        public async Task<WaterConnection> getPreviousBillForWaterConnection(int year, int month, int wcId)
        {
            return await _unitOfWork.Balances.getPreviousBillForWaterConnection(year, month, wcId);
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
