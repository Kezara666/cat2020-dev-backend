using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Services.Vote;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.ShopRental
{
    public class ShopRentalProcessService : IShopRentalProcessService
    {
        private readonly IMapper _mapper;
        private readonly IShopRentalUnitOfWork _unitOfWork;
        private readonly ILogger<ShopRentalProcessService> _logger;

        private readonly IShopRentalBalanceService _shopRentalBalanceService;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly IShopRentalProcessConfigarationService _shopRentalProcessConfigarationService;

        public ShopRentalProcessService(IMapper mapper, IShopRentalUnitOfWork unitOfWork, ILogger<ShopRentalProcessService> logger, IShopRentalBalanceService shopRentalBalanceService, IVoteBalanceService voteBalanceService , IShopRentalProcessConfigarationService shopRentalProcessConfigarationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _shopRentalBalanceService = shopRentalBalanceService;
            _voteBalanceService = voteBalanceService;
            _shopRentalProcessConfigarationService = shopRentalProcessConfigarationService;

        }

        //-----
        public async Task<bool> IsCompetedMonthendProcess(int sbahaId, DateOnly date)
        {
            return await _unitOfWork.ShopRentalProcess.IsCompetedMonthendProcess(sbahaId, date);
        }
        //-----
        public async Task<bool> IsCompetedFineProcess(int sbahaId, DateOnly date)
        {
            return await _unitOfWork.ShopRentalProcess.IsCompetedFineProcess(sbahaId, date);
        }
        //-----


        //-----
        public async Task<(int totalCount, IEnumerable<ShopRentalProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo)
        {
            try
            {
                return await _unitOfWork.ShopRentalProcess.getAllProcessForSabha(sabhaId, pageNo);
            }
            catch (Exception ex)
            {
                List<ShopRentalProcess> list1 = new List<ShopRentalProcess>();
                return (0, list1);
            }
        }
        //-----

        public async Task<ShopRentalProcess> getLastDailyFineRateShopRentalprocess(int SbahaId)
        {
            return await _unitOfWork.ShopRentalProcess.getLastDailyFineRateShopRentalprocess(SbahaId);
        }

        public async Task<ShopRentalProcess> getLastDailyFineFixedAmountShopRentalprocess(int SbahaId)
        {
            return await _unitOfWork.ShopRentalProcess.getLastDailyFineFixedAmountShopRentalprocess(SbahaId);
        }

        //-----[ Start : backup] -----
        private async Task<(bool, string)> BackupProcess(int sabhaId, int ProcessConfigId, string processType, object environment, string _uploadsFolder)
        {
            try
            {
                var shopBalance = new List<ShopRentalBalance>();

                if (processType == "MONTHENDPROCESS" && ProcessConfigId == 0)
                {
                    shopBalance = (List<ShopRentalBalance>)await _unitOfWork.ShopRentalBalance.GetForMonthendProcessBackup(sabhaId);
                }
                else
                {
                    shopBalance = (List<ShopRentalBalance>)await _unitOfWork.ShopRentalBalance.GetForFineProcessBackup(sabhaId, ProcessConfigId);
                }

                // Create a new Excel workbook and worksheet
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    var protection = worksheet.Protect("YourPassword");

                    worksheet.Cell(1, 1).Value = "sr_bal_id";
                    worksheet.Cell(1, 2).Value = "sr_bal_proprty_id";
                    worksheet.Cell(1, 3).Value = "sr_bal_shop_id";
                    worksheet.Cell(1, 4).Value = "sr_bal_year";
                    worksheet.Cell(1, 5).Value = "sr_bal_month";

                    worksheet.Cell(1, 6).Value = "sr_bal_arrears_amount";
                    worksheet.Cell(1, 7).Value = "sr_bal_paid_arrears_amount";
                    worksheet.Cell(1, 8).Value = "sr_bal_fine_amount";
                    worksheet.Cell(1, 9).Value = "sr_bal_paid_fine_amount";
                    worksheet.Cell(1, 10).Value = "sr_bal_service_charge_arreas_amount";
                    worksheet.Cell(1, 11).Value = "sr_bal_paid_service_charge_arreas_amount";
                    worksheet.Cell(1, 12).Value = "sr_bal_over_payment_amount";

                    worksheet.Cell(1, 13).Value = "sr_bal_is_completed";
                    worksheet.Cell(1, 14).Value = "sr_bal_current_rental_amount";
                    worksheet.Cell(1, 15).Value = "sr_bal_current_service_charge_amount";

                    worksheet.Cell(1, 16).Value = "sr_bal_ly_arreas";
                    worksheet.Cell(1, 17).Value = "sr_bal_paid_current_rental_amount";
                    worksheet.Cell(1, 18).Value = "sr_bal_paid_current_service_charge_amount";

                    worksheet.Cell(1, 19).Value = "sr_bal_paid_ly_arreas";
                    worksheet.Cell(1, 20).Value = "sr_bal_paid_ly_fine";
                    worksheet.Cell(1, 21).Value = "sr_bal_paid_ty_arreas";
                    worksheet.Cell(1, 22).Value = "sr_bal_paid_ty_fine";
                    worksheet.Cell(1, 23).Value = "sr_bal_ty_arreas";
                    worksheet.Cell(1, 24).Value = "sr_bal_ty_fine";
                    worksheet.Cell(1, 25).Value = "sr_bal_ly_fine";

                    worksheet.Cell(1, 26).Value = "sr_bal_ly_ty_service_charge_arreas";
                    worksheet.Cell(1, 27).Value = "sr_bal_paid_ly_ty_service_charge_arreas";

                    worksheet.Cell(1, 28).Value = "sr_bal_office_id";
                    worksheet.Cell(1, 29).Value = "sr_bal_sabha_id";

                    int row = 2;

                    foreach (var item in shopBalance)
                    {
                        worksheet.Cell(row, 1).Value = item.Id;
                        worksheet.Cell(row, 2).Value = item.PropertyId;
                        worksheet.Cell(row, 3).Value = item.ShopId;
                        worksheet.Cell(row, 4).Value = item.Year;
                        worksheet.Cell(row, 5).Value = item.Month;

                        worksheet.Cell(row, 6).Value = item.ArrearsAmount;
                        worksheet.Cell(row, 7).Value = item.PaidArrearsAmount;
                        worksheet.Cell(row, 8).Value = item.FineAmount;
                        worksheet.Cell(row, 9).Value = item.PaidFineAmount;
                        worksheet.Cell(row, 10).Value = item.ServiceChargeArreasAmount;
                        worksheet.Cell(row, 11).Value = item.PaidServiceChargeArreasAmount;
                        worksheet.Cell(row, 12).Value = item.OverPaymentAmount;

                        worksheet.Cell(row, 13).Value = item.IsCompleted;
                        worksheet.Cell(row, 14).Value = item.CurrentRentalAmount;
                        worksheet.Cell(row, 15).Value = item.CurrentServiceChargeAmount;

                        worksheet.Cell(row, 16).Value = item.LYArreas;
                        worksheet.Cell(row, 17).Value = item.PaidCurrentRentalAmount;
                        worksheet.Cell(row, 18).Value = item.PaidCurrentServiceChargeAmount;

                        worksheet.Cell(row, 19).Value = item.PaidLYArreas;
                        worksheet.Cell(row, 20).Value = item.PaidLYFine;
                        worksheet.Cell(row, 21).Value = item.PaidTYArreas;
                        worksheet.Cell(row, 22).Value = item.PaidTYFine;
                        worksheet.Cell(row, 23).Value = item.TYArreas;
                        worksheet.Cell(row, 24).Value = item.TYFine;
                        worksheet.Cell(row, 25).Value = item.LYFine;

                        worksheet.Cell(1, 26).Value = item.TYLYServiceChargeArreas;
                        worksheet.Cell(1, 27).Value = item.PaidTYLYServiceChargeArreas;

                        worksheet.Cell(1, 28).Value = item.OfficeId;
                        worksheet.Cell(1, 29).Value = item.SabhaId;

                        row++;
                    }

                    worksheet.Column(1).Style.Protection.SetLocked(true);

                    //sheet styles

                    worksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    foreach (IXLColumn column in worksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    //worksheet.Column(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Column(3).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightGreen;
                    worksheet.Column(4).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightBlue;
                    worksheet.Column(5).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightCoral;
                    worksheet.Column(29).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                    // Generate a unique file name
                    var uniqueFileName = $"sr_bal_bkp_{processType}_{sabhaId}_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";

                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    var filePath = System.IO.Path.Combine(_uploadsFolder, "ShopRental Balance", uniqueFileName);
                    workbook.SaveAs(filePath);

                    return (true, uniqueFileName);
                }
            }
            catch (Exception ex)
            {
                return (false, "");
            }
        }
        //-----[ End : backup] -----


        //-----[ Start : Check whether there exists an active session] -----
        //*****
        //note: There should not be an active session in order to proceed (monthend-process).
        //note: If there is no active session, it means there are no bills in draft.
        //*****

        //---* for monthend
        //****
        //note: Last session year/{month}/date < year/{month}/1
        //****
        private async Task<bool> FinalizeMonthendProcess(int sabhaId, DateTime processDate)
        {
            try
            {
                var oficces = await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sabhaId);

                bool hasToCommit = false;
                foreach (var office in oficces)
                {

                    if (await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForOffice(office.ID.Value))
                    {
                        //----
                        var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(office.ID);
                        if (hasactiveSession == null)
                        {
                            throw new Exception("Active Session Is Not Found");
                        }
                        //else
                        //{
                        //    //has an active session

                        //    var lastSession = await _unitOfWork.Sessions.HasLastSessionMatched(office.ID.Value);


                        //    if (lastSession != null)
                        //    {
                        //        //Please verify if the start date of the session is the 1st day of the month.
                        //        if (!(lastSession.StartAt.Month == processDate.Month && lastSession.StartAt.Day == processDate.Day))
                        //        {
                        //            //If a session does not exist, create a session on the 1st day of the month without ending it.
                        //            var s = new Session
                        //            {
                        //                Name = "MIX" + "-" + processDate.ToString("yyyy-MM-dd-HH:mm"),
                        //                CreatedAt = processDate,
                        //                StartAt = processDate,
                        //                CreatedBy = -1,
                        //                OfficeId = office.ID.Value,
                        //                Module = "MIX",
                        //                Active = 1, //create a active session
                        //                Rescue = 0,
                        //                RescueStartedAt = processDate,
                        //                UpdatedAt = processDate,
                        //                UpdatedBy = -1,

                        //                //StopAt = processDate,
                        //            };

                        //            //s.SetActiveFalse();

                        //            await _unitOfWork.Sessions.AddAsync(s);

                        //            hasToCommit = true;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("No Last Session Found.");
                        //    }
                        //}
                        //----
                    }
                }
                if (hasToCommit)
                {
                    await _unitOfWork.CommitAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data Finalize processing.{EventType}", "Shop Rental");
                return false;
            }
        }
        //----* for monthly fine process
        //****
        //note: Last session year/{month}/{date} < year/{month}/{fineDate}
        //****
        //private async Task<bool> FinalizeMonthlyFineProcess(int sabhaId, DateTime processDate)
        //{
        //    try
        //    {
        //        var oficces = await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sabhaId);

        //        bool hasToCommit = false;
        //        foreach (var office in oficces)
        //        {

        //            if (await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForOffice(office.ID.Value))
        //            {
        //                //----
        //                var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(office.ID);
        //                if (hasactiveSession == null)
        //                {
        //                    var lastSession = await _unitOfWork.Sessions.HasLastSessionMatched(office.ID.Value);

        //                    if (lastSession != null)
        //                    {
        //                        if (!(lastSession.StartAt.Month == processDate.Month && lastSession.StartAt.Day == processDate.Day))
        //                        {

        //                            var s = new Session
        //                            {
        //                                Name = "MIX" + "-" + processDate.ToString("yyyy-MM-dd-HH:mm"),
        //                                CreatedAt = processDate,
        //                                StartAt = processDate,
        //                                CreatedBy = -1,
        //                                OfficeId = office.ID.Value,
        //                                Module = "MIX",
        //                                Active = 0,
        //                                Rescue = 0,
        //                                RescueStartedAt = processDate,
        //                                UpdatedAt = processDate,
        //                                UpdatedBy = -1,
        //                                StopAt = processDate,
        //                            };

        //                            s.SetActiveFalse();
        //                            await _unitOfWork.Sessions.AddAsync(s);

        //                            hasToCommit = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("No Session Found.");
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("Active Session Found.");
        //                }
        //                //----
        //            }
        //        }
        //        if (hasToCommit)
        //        {
        //            await _unitOfWork.CommitAsync();

        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.InnerException, "An error occurred during data Finalize processing.{EventType}", "Shop Rental");
        //        return false;
        //    }
        //}

        //----* for daily fine process
        //****
        //note: Last session day < {day}
        //****
        //private async Task<bool> FinalizeDailyFineProcess(int sabhaId, DateTime processDate)
        //{
        //    try
        //    {
        //        var oficces = await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sabhaId);

        //        bool hasToCommit = false;
        //        foreach (var office in oficces)
        //        {

        //            if (await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForOffice(office.ID.Value))
        //            {
        //                //----
        //                var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(office.ID);
        //                if (hasactiveSession == null)
        //                {
        //                    var lastSession = await _unitOfWork.Sessions.HasLastSessionMatched(office.ID.Value);

        //                    if (lastSession != null)
        //                    {
        //                        if (!(lastSession.StartAt.Day == processDate.Day))
        //                        {
        //                            //***
        //                            //Note: To avoid invoicing for the previous month after the month-end process is completed, a new session will be created if the last session is not aligned with the billing process date.
        //                            //***
        //                            var s = new Session
        //                            {
        //                                Name = "MIX" + "-" + processDate.ToString("yyyy-MM-dd-HH:mm"),
        //                                CreatedAt = processDate,
        //                                StartAt = processDate,
        //                                CreatedBy = -1,
        //                                OfficeId = office.ID.Value,
        //                                Module = "MIX",
        //                                Active = 0,
        //                                Rescue = 0,
        //                                RescueStartedAt = processDate,
        //                                UpdatedAt = processDate,
        //                                UpdatedBy = -1,
        //                                StopAt = processDate,
        //                            };

        //                            s.SetActiveFalse();
        //                            await _unitOfWork.Sessions.AddAsync(s);

        //                            hasToCommit = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("No Session Found.");
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("Active Session Found.");
        //                }
        //                //----
        //            }
        //        }
        //        if (hasToCommit)
        //        {
        //            await _unitOfWork.CommitAsync();

        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.InnerException, "An error occurred during data Finalize processing.{EventType}", "Shop Rental");
        //        return false;
        //    }
        //}


        //******************* Update - fine process ********************************
        //note: There should be an active session in order to proceed (fine-process).
        private async Task<bool> FinalizeFineProcess(int sabhaId)
        {
            try
            {
                var oficces = await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sabhaId);

                foreach (var office in oficces)
                {

                    if (await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForOffice(office.ID.Value))
                    {
                        //----
                        var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(office.ID);
                        if (hasactiveSession == null)
                        {
                            throw new Exception("Active Session Not Found.");
                        }
                        //----
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data Finalize processing.{EventType}", "Shop Rental");
                return false;
            }
        }
        //******************* Update - fine process ********************************
        //-----[ End : Check whether there exists an active session] -------



        //-----[ Start : monthend process] -------
        public async Task<(bool, string)> MonthEndProcess(ShopRentalProcess shopRentalProcess, object environment, string _backupFolder, HTokenClaim token)
        {

            shopRentalProcess.Id = null;
            //******
            //note: The month-end process will be executed at 12:00:00 PM on the first day of every month.
            //******
            if (await FinalizeMonthendProcess(shopRentalProcess.ShabaId, new DateTime(DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth, shopRentalProcess.CurrentSessionDay, 12, 0, 0)))
            {
                using (var dbtransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        //----------------------------------------------------------------------
                        //note: there is no process config id for monthend process, so pass zero for that parameter
                        (var state, var uniqueFileName) = await BackupProcess(shopRentalProcess.ShabaId, 0, "MONTHENDPROCESS", environment, _backupFolder);
                        if (!state)
                        {
                            return (false, "The backup process did not go as planned.");
                        }
                        else
                        {
                            //------- [Start :  logic] --------
                            bool hasToCreateNextMonthBalanceRecord = false;

                            var activeHoldShopsForSabha = await _unitOfWork.Shops.GetAllShopsForMonthendProcessBySabhaId(shopRentalProcess.ShabaId);

                            if (activeHoldShopsForSabha != null && activeHoldShopsForSabha.Count() != 0)
                            {
                                foreach (var shop in activeHoldShopsForSabha)
                                {
                                    var lastBalanceRecord = await _unitOfWork.ShopRentalBalance.GetLastBalanceInfo(shop.Id.Value);

                                    if (lastBalanceRecord != null)
                                    {
                                        //*******
                                        if (shopRentalProcess.Date == null)
                                        {
                                            int lastBalanceRecordLastDay = 0;
                                            lastBalanceRecordLastDay = GetLastDayOfMonth(lastBalanceRecord.Year, lastBalanceRecord.Month);
                                            if (lastBalanceRecordLastDay <= 0)
                                            {
                                                throw new FinalAccountException("Error occured while getting last day of the last month");
                                            }
                                            else
                                            {
                                                shopRentalProcess.Date = new DateOnly(lastBalanceRecord.Year, lastBalanceRecord.Month, lastBalanceRecordLastDay);
                                            }
                                        }
                                        //*******


                                        //************
                                        decimal nextMonthRecord_overPaymentAmount = 0;
                                        decimal nextMonthRecord_LYFine = 0;
                                        decimal nextMonthRecord_LYArreas = 0;
                                        decimal nextMonthRecord_TYFine = 0;
                                        decimal nextMonthRecord_TYArreas = 0;
                                        decimal nextMonthRecord_TYLYServiceChargeArreas = 0;
                                        decimal nextMonthRecord_PaidCurrentServiceChargeAmount = 0;
                                        decimal nextMonthRecord_PaidCurrentRentalAmount = 0;

                                        int nextMonthRecord_NoOfPayments = 0;
                                        bool nextMonthRecord_IsCompleted = false;
                                        bool nextMonthRecord_IsProcessed = false;
                                        //*************

                                        //#1 - Arreas case
                                        if (lastBalanceRecord.IsCompleted == false)
                                        {
                                            if (shop.Status == ShopStatus.Active)
                                            {
                                                //#1.1 - Arreas Transfering - only for active shop 
                                                if (lastBalanceRecord.Shop.OpeningBalance.MonthId != lastBalanceRecord.Month)
                                                {
                                                    if ((shop.Rental - lastBalanceRecord.PaidCurrentRentalAmount) != 0)
                                                    {
                                                        lastBalanceRecord.ArrearsAmount = shop.Rental - lastBalanceRecord.PaidCurrentRentalAmount;
                                                    }

                                                    if ((shop.ServiceCharge - lastBalanceRecord.PaidCurrentServiceChargeAmount) != 0)
                                                    {
                                                        lastBalanceRecord.ServiceChargeArreasAmount = shop.ServiceCharge - lastBalanceRecord.PaidCurrentServiceChargeAmount;
                                                    }

                                                    await _unitOfWork.CommitAsync();

                                                    lastBalanceRecord = await _unitOfWork.ShopRentalBalance.GetLastBalanceInfo(shop.Id.Value);
                                                }
                                            }

                                            //#1.2 - next month balance recored created with arreas
                                            (nextMonthRecord_LYFine, nextMonthRecord_LYArreas, nextMonthRecord_TYFine, nextMonthRecord_TYArreas, nextMonthRecord_TYLYServiceChargeArreas) = await _shopRentalBalanceService.calculateTotalTyLyArreasAmountForYearMonth(lastBalanceRecord.ShopId.Value, lastBalanceRecord.Year, lastBalanceRecord.Month);

                                            hasToCreateNextMonthBalanceRecord = true;


                                        }
                                        else
                                        {
                                            //#2 - next month balance recored created with all settled/overpayment
                                            if (shop.Status == ShopStatus.Active)
                                            {
                                                if (lastBalanceRecord.OverPaymentAmount > 0)
                                                {
                                                    nextMonthRecord_NoOfPayments = 1;
                                                    //---

                                                    decimal remaingOverPayAmount_afterRental = 0;
                                                    //decimal remaingOverPayAmount_afterServiceCharge = 0;

                                                    if (lastBalanceRecord.OverPaymentAmount == shop.Rental)
                                                    {
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)shop.Rental;
                                                        nextMonthRecord_IsCompleted = (shop.ServiceCharge == 0) ? true : false;
                                                    }
                                                    else if (lastBalanceRecord.OverPaymentAmount > shop.Rental)
                                                    {
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)shop.Rental;

                                                        //---
                                                        remaingOverPayAmount_afterRental = (decimal)lastBalanceRecord.OverPaymentAmount - (decimal)shop.Rental;
                                                        //---

                                                        if (remaingOverPayAmount_afterRental == shop.ServiceCharge)
                                                        {
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = (decimal)shop.ServiceCharge;

                                                            nextMonthRecord_IsCompleted = true;
                                                            nextMonthRecord_IsProcessed = true;
                                                        }
                                                        else if (remaingOverPayAmount_afterRental > shop.ServiceCharge)
                                                        {
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = (decimal)shop.ServiceCharge;

                                                            //---
                                                            //remaingOverPayAmount_afterServiceCharge = (decimal)remaingOverPayAmount_afterRental - (decimal)shop.ServiceCharge;
                                                            //---

                                                            nextMonthRecord_overPaymentAmount = (decimal)remaingOverPayAmount_afterRental - (decimal)shop.ServiceCharge;

                                                            nextMonthRecord_IsCompleted = true;
                                                            nextMonthRecord_IsProcessed = true;
                                                        }
                                                        else
                                                        {
                                                            //remaingOverPayAmount_afterRental < shop.ServiceCharge
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = remaingOverPayAmount_afterRental;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //lastBalanceRecord.OverPaymentAmount < shop.Rental
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)lastBalanceRecord.OverPaymentAmount;
                                                    }
                                                }

                                                hasToCreateNextMonthBalanceRecord = true;
                                            }
                                            else if(shop.Status == ShopStatus.Hold)
                                            {

                                                //Hold shop - all payment has been settled
                                                //Overpaymnt has been disabled in the payemt for hold shops

                                                //change shop status to Inactive
                                                //shop.Status = ShopStatus.Inactive;

                                                //await _unitOfWork.CommitAsync();


                                                if (lastBalanceRecord.OverPaymentAmount > 0)
                                                {
                                                    nextMonthRecord_NoOfPayments = 1;
                                                    //---

                                                    decimal remaingOverPayAmount_afterRental = 0;
                                                    //decimal remaingOverPayAmount_afterServiceCharge = 0;

                                                    if (lastBalanceRecord.OverPaymentAmount == shop.Rental)
                                                    {
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)shop.Rental;
                                                        nextMonthRecord_IsCompleted = (shop.ServiceCharge == 0) ? true : false;
                                                    }
                                                    else if (lastBalanceRecord.OverPaymentAmount > shop.Rental)
                                                    {
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)shop.Rental;

                                                        //---
                                                        remaingOverPayAmount_afterRental = (decimal)lastBalanceRecord.OverPaymentAmount - (decimal)shop.Rental;
                                                        //---

                                                        if (remaingOverPayAmount_afterRental == shop.ServiceCharge)
                                                        {
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = (decimal)shop.ServiceCharge;

                                                            nextMonthRecord_IsCompleted = true;
                                                            nextMonthRecord_IsProcessed = true;
                                                        }
                                                        else if (remaingOverPayAmount_afterRental > shop.ServiceCharge)
                                                        {
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = (decimal)shop.ServiceCharge;

                                                            //---
                                                            //remaingOverPayAmount_afterServiceCharge = (decimal)remaingOverPayAmount_afterRental - (decimal)shop.ServiceCharge;
                                                            //---

                                                            nextMonthRecord_overPaymentAmount = (decimal)remaingOverPayAmount_afterRental - (decimal)shop.ServiceCharge;

                                                            nextMonthRecord_IsCompleted = true;
                                                            nextMonthRecord_IsProcessed = true;
                                                        }
                                                        else
                                                        {
                                                            //remaingOverPayAmount_afterRental < shop.ServiceCharge
                                                            nextMonthRecord_PaidCurrentServiceChargeAmount = remaingOverPayAmount_afterRental;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //lastBalanceRecord.OverPaymentAmount < shop.Rental
                                                        nextMonthRecord_PaidCurrentRentalAmount = (decimal)lastBalanceRecord.OverPaymentAmount;
                                                    }
                                                }


                                                hasToCreateNextMonthBalanceRecord = true; //prevent creating next month record for hold shops which have settled all payments
                                                
                                            }
                                            else
                                            {
                                                hasToCreateNextMonthBalanceRecord = false;
                                            }
                                        }

                                        if (hasToCreateNextMonthBalanceRecord)
                                        {
                                            int IsHold = 0;
                                            if (shop.Status == ShopStatus.Hold)
                                            { 
                                                IsHold = 1 ;
                                            }

                                            var nextMonthBalance = new ShopRentalBalance
                                            {
                                                //-------------------------------------------------------
                                                PropertyId = lastBalanceRecord.PropertyId,
                                                ShopId = lastBalanceRecord.ShopId,
                                                OfficeId = lastBalanceRecord.OfficeId,
                                                SabhaId = lastBalanceRecord.SabhaId,

                                                Year = DateTime.Now.Year,
                                                Month = shopRentalProcess.CurrentSessionMonth,   //current session month
                                                FromDate = new DateOnly(DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth, 1),
                                                ToDate = null,
                                                //-------------------------------------------------------


                                                //-------------------------------------------------------
                                                ArrearsAmount = 0, //this year Arreas amount
                                                FineAmount = 0, //this value will be assigned in the fine process
                                                ServiceChargeArreasAmount = 0,
                                                OverPaymentAmount = nextMonthRecord_overPaymentAmount,
                                                //-------------------------------------------------------

                                                IsCompleted = nextMonthRecord_IsCompleted,
                                                IsProcessed = nextMonthRecord_IsProcessed,
                                                NoOfPayments = nextMonthRecord_NoOfPayments,
                                                CreatedBy = token.userId,
                                                HasTransaction = false,

                                                //------[Start: fields for Report]-------
                                                LYFine = nextMonthRecord_LYFine,
                                                LYArreas = nextMonthRecord_LYArreas,
                                                TYFine = nextMonthRecord_TYFine,
                                                TYArreas = nextMonthRecord_TYArreas,
                                                TYLYServiceChargeArreas = nextMonthRecord_TYLYServiceChargeArreas,

                                                CurrentRentalAmount = shop.Rental,
                                                PaidCurrentRentalAmount = nextMonthRecord_PaidCurrentRentalAmount,

                                                CurrentServiceChargeAmount = shop.ServiceCharge,
                                                PaidCurrentServiceChargeAmount = nextMonthRecord_PaidCurrentServiceChargeAmount,
                                                IsHold = IsHold
                                                //------[End: fields for Report]---------
                                            };

                                            if (nextMonthRecord_overPaymentAmount > 0)
                                            {
                                                nextMonthBalance.IsCompleted = true;
                                            }

                                            shop.OpeningBalance.IsProcessed = true;

                                            await _unitOfWork.ShopRentalBalance.AddAsync(nextMonthBalance);
                                        }

                                        //Note: Final accounts is valid only for ACTIVE shops
                                        if (shop.Status == ShopStatus.Active)
                                        {
                                            if (token.IsFinalAccountsEnabled == 1)
                                            {
                                                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                if (session != null)
                                                {

                                                    await UpdateLedgerAccounts(shop.Id.Value, (decimal)shop.Rental!, (decimal)shop.ServiceCharge!, nextMonthRecord_PaidCurrentRentalAmount, nextMonthRecord_PaidCurrentServiceChargeAmount, (decimal)(nextMonthRecord_PaidCurrentRentalAmount + nextMonthRecord_PaidCurrentServiceChargeAmount), session, token);
                                                }
                                                else
                                                {
                                                    throw new FinalAccountException("No Active Session Found.");
                                                }
                                            }
                                        }

                                        //await _unitOfWork.CommitAsync();
                                    }
                                    else
                                    {
                                        throw new Exception("To process monthend, last balance info cannot be null");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Monthend process can be applied only for active shops");
                            }
                            //------- [End :  logic] --------


                            //*****
                            //Note: The values of other fields are included in the response.
                            //*****
                            shopRentalProcess.BackUpKey = uniqueFileName;
                            shopRentalProcess.ProcessType = ShopRentalProcessType.MonthendProcess;
                            await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                            //--------

                            await _unitOfWork.CommitAsync();

                            dbtransaction.Commit(); // Commit the transaction if everything succeeds

                            _logger.LogInformation("Monthend Process Successfully Completed. {EventType}", "ShopRental");

                            return (true, "Monthend Process Successfully Completed");
                        }
                        //----------------------------------------------------------------------
                    }
                    catch (Exception ex)
                    {
                        dbtransaction.Rollback(); // Rollback the transaction if an exception occurs
                        _logger.LogError(ex, "An error occurred during monthend processing.{EventType}", "ShopRental");
                        return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                    }
                }
            }
            else
            {
                return (false, "Session verification was not successful.");
            }
        }
        //-----[ End : monthend process] -------


        //-----[Start: get last day of the month]--------
        private int GetLastDayOfMonth(int year, int month)
        {
            try
            {
                // Validate input (optional)
                if (month < 1 || month > 12)
                {
                    return -1;
                }

                // Calculate the last day of the month
                int lastDay = DateTime.DaysInMonth(year, month);

                return lastDay;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //-----[End: get last day of the month]----------


     public async   Task<(bool, string?)> SkipFineProcess(ShopRentalProcess shopRentalProcess , HTokenClaim token)
        {
            try
            {

                if (shopRentalProcess.Id == null || shopRentalProcess.Id ==0)
                {
                    shopRentalProcess.Id = null;
                    shopRentalProcess.ProceedDate = DateTime.Now;
                    shopRentalProcess.ActionBy = token.userId;
                    shopRentalProcess.ProcessType = ShopRentalProcessType.FineProcess;
                    shopRentalProcess.IsSkippeed = 1;

                    await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                }
                else
                {
                    return (false, "Unable to skip the fine process");
                }
                await _unitOfWork.CommitAsync();
                return (true, "Fine Process Skipped Successfully");
            }
            catch (Exception ex) {
                return (false,ex.Message);
             
            }
        }
        //-----[ Start : fine process] -------
        public async Task<(bool, string)> FineProcess(ShopRentalProcess shopRentalProcess, object environment, string _backupFolder, HTokenClaim token)
        {
            shopRentalProcess.Id = null;
            using (var dbtransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var processConfigSetting = await _unitOfWork.ShopRentalProcessConfigaration.GetByProcessConfigId(shopRentalProcess.ProcessConfigId.Value);

                    if (processConfigSetting != null)
                    {
                        if (processConfigSetting.FineRateTypeId == 1)
                        {

                            //var processConfigurationSettingsForSabha = await _unitOfWork.ShopRentalProcessConfigaration.GetAllForSabha(token.sabhaId);
                            //Daily
                            //******
                            //note: The fine process will be executed at 12:00:00 PM on every day.
                            //******
                            //if (await FinalizeDailyFineProcess(shopRentalProcess.ShabaId, new DateTime(DateTime.Now.Year, shopRentalProcess.LastEndedSessionMonth, shopRentalProcess.LastEndedSessionDay, 12, 0, 0)))
                            if (await FinalizeFineProcess(shopRentalProcess.ShabaId))
                            {
                                (var state, var uniqueFileName) = await BackupProcess(shopRentalProcess.ShabaId, shopRentalProcess.ProcessConfigId.Value, "FINEPROCESS", environment, _backupFolder);
                                if (!state)
                                {
                                    return (false, "The backup process did not go as planned.");
                                }
                                else
                                {
                                    //-------- [Start - logic] --------
                                    var activeShopsForSabha = await _unitOfWork.Shops.GetAllShopsForFineProcessBySabha(shopRentalProcess.ShabaId, shopRentalProcess.ProcessConfigId.Value, processConfigSetting.FineRateTypeId);

                                    if (activeShopsForSabha != null && activeShopsForSabha.Count() != 0)
                                    {
                                        foreach (var shop in activeShopsForSabha)
                                        {
                                            var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shop.Id.Value);

                                            if (processConfigAssignmentInfo != null)
                                            {
                                                //-------------[Start - rental payment date type -------------
                                                if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId == 2)
                                                {
                                                    //Current Month Rental Before Fine Process Date - 2 //daily process should not have previous month option
                                                    var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value, DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth);

                                                    if (currentMonthBalanceRecord != null)
                                                    {
                                                        //*******
                                                        if (shopRentalProcess.Date == null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, shopRentalProcess.CurrentSessionDay);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                        }
                                                        //*******

                                                        if (currentMonthBalanceRecord.IsCompleted == false)
                                                        {
                                                            decimal thisMonthFineForShop = 0;
                                                            thisMonthFineForShop = await CreateFineForShop(shop.Id.Value, new DateOnly(DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth, shopRentalProcess.CurrentSessionDay), shop.Rental.Value, processConfigAssignmentInfo.ShopRentalProcessConfigaration);
                                                            if (thisMonthFineForShop < 0)
                                                            {
                                                                throw new Exception("Unable To Run Daily Fine Process.");
                                                            }
                                                            else
                                                            {
                                                                currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                                currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop; //Report field

                                                                //------[Start - final account values]-----
                                                                if (token.IsFinalAccountsEnabled == 1)
                                                                {
                                                                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                    if (session != null)
                                                                    {

                                                                         await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, session, token);
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FinalAccountException("No Active Session Found.");
                                                                    }
                                                                }
                                                                //------[End - final account values]-----

                                                                // await _unitOfWork.CommitAsync();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Last balnce record is null");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new ArgumentNullException("Invalid rental payment date type");
                                                }
                                                //-------------[End - rental payment date type ---------------
                                            }
                                            else
                                            {
                                                throw new ArgumentNullException("Process config asiignment info cannot be null");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("There is no active shops");
                                    }
                                    //-------- [End - logic] ----------
                                }
                                //----

                                //*****
                                //Note: The values of other fields are included in the response.
                                shopRentalProcess.BackUpKey = uniqueFileName;
                                shopRentalProcess.ProcessType = ShopRentalProcessType.FineProcess;
                                await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                                //--------

                                await _unitOfWork.CommitAsync();

                                dbtransaction.Commit(); // Commit the transaction if everything succeeds

                                _logger.LogInformation("Fine Process Successfully Completed. {EventType}", "ShopRental");

                                return (true, "Fine Process Successfully Completed");
                            }
                            else
                            {
                                return (false, "Session verification was not successful.");
                            }
                        }
                        else
                        {
                            //******
                            //note: The month-end process will be executed at 12:00:00 PM on the fine process day of every month.
                            //******
                            //if (await FinalizeMonthlyFineProcess(shopRentalProcess.ShabaId, new DateTime(DateTime.Now.Year, shopRentalProcess.LastEndedSessionMonth, processConfigSetting.FineDate, 12, 0, 0)))
                            if (await FinalizeFineProcess(shopRentalProcess.ShabaId))
                            {
                                (var state, var uniqueFileName) = await BackupProcess(shopRentalProcess.ShabaId, shopRentalProcess.ProcessConfigId.Value, "FINEPROCESS", environment, _backupFolder);
                                if (!state)
                                {
                                    return (false, "The backup process did not go as planned.");
                                }
                                else
                                {
                                    //-------- [Start - logic] --------
                                    var activeShopsForSabha = await _unitOfWork.Shops.GetAllShopsForFineProcessBySabha(shopRentalProcess.ShabaId, shopRentalProcess.ProcessConfigId.Value, processConfigSetting.FineRateTypeId);

                                    if (activeShopsForSabha != null && activeShopsForSabha.Count() != 0)
                                    {
                                        foreach (var shop in activeShopsForSabha)
                                        {
                                            var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shop.Id.Value);

                                            if(processConfigAssignmentInfo != null)
                                            {
                                                //-------------[Start - rental payment date type -------------
                                                if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId == 2)
                                                {
                                                    //Current Month Rental Before Fine Process Date - 2
                                                    var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value, DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth);

                                                    if (currentMonthBalanceRecord != null)
                                                    {
                                                        //*******
                                                        if (shopRentalProcess.Date == null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, shopRentalProcess.CurrentSessionDay);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                        }
                                                        //*******

                                                        if (currentMonthBalanceRecord.IsCompleted == false)
                                                        {
                                                            decimal thisMonthFineForShop = 0;
                                                            thisMonthFineForShop = await CreateFineForShop(shop.Id.Value, new DateOnly(DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth, shopRentalProcess.CurrentSessionDay + 1).AddMonths(1), shop.Rental.Value, processConfigAssignmentInfo.ShopRentalProcessConfigaration);
                                                            if (thisMonthFineForShop < 0)
                                                            {
                                                                throw new Exception("Unable To Run Monthly Fine Process.");
                                                            }
                                                            else
                                                            {
                                                                currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                                currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop; //Report field

                                                                ////------[Start - final account values]-----
                                                                if (token.IsFinalAccountsEnabled == 1)
                                                                {
                                                                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                    if (session != null)
                                                                    {

                                                                        await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, session, token);
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FinalAccountException("No Active Session Found.");
                                                                    }
                                                                }
                                                                //------[End - final account values]-------

                                                                //  await _unitOfWork.CommitAsync();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Current balance record is null");
                                                    }
                                                }
                                                else if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId == 1)
                                                {
                                                    //Last Month Rental Before Fine Process Date - 1

                                                    var prevMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value, shopRentalProcess.CurrentSessionMonth == 1 ? (DateTime.Now.Year - 1) : DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth == 1 ? 12 : shopRentalProcess.CurrentSessionMonth - 1);

                                                    if (prevMonthBalanceRecord != null)
                                                    {
                                                        //*******
                                                        if (shopRentalProcess.Date == null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(prevMonthBalanceRecord.Year, shopRentalProcess.CurrentSessionMonth == 1 ? 12 : shopRentalProcess.CurrentSessionMonth , shopRentalProcess.CurrentSessionDay);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                        }
                                                        //*******

                                                        if (prevMonthBalanceRecord.IsCompleted == false)
                                                        {
                                                            decimal thisMonthFineForShop = 0;
                                                            thisMonthFineForShop = await CreateFineForShop(shop.Id.Value, new DateOnly(shopRentalProcess.CurrentSessionMonth == 1 ? (DateTime.Now.Year - 1) : DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth == 1 ? 12 : shopRentalProcess.CurrentSessionMonth - 1, shopRentalProcess.CurrentSessionDay + 1).AddMonths(1), shop.Rental.Value, processConfigAssignmentInfo.ShopRentalProcessConfigaration);
                                                            if (thisMonthFineForShop < 0)
                                                            {
                                                                throw new Exception("Unable To Run Monthly Fine Process.");
                                                            }
                                                            else
                                                            {

                                                                prevMonthBalanceRecord.FineAmount = thisMonthFineForShop;

                                                                //---- [Start : update last record report field] --------
                                                                var curentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value, DateTime.Now.Year, shopRentalProcess.CurrentSessionMonth);
                                                                if (curentMonthBalanceRecord != null)
                                                                {
                                                                    curentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;  //Report field
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception("Current month balance record cannot be null");
                                                                }
                                                                //---- [End : update last record report field] ----------





                                                                //------[Start - final account values]-----
                                                                if (token.IsFinalAccountsEnabled == 1)
                                                                {
                                                                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                    if (session != null)
                                                                    {

                                                                          await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, session, token);
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FinalAccountException("No Active Session Found.");
                                                                    }
                                                                }
                                                                //------[End - final account values]-------

                                                                //  await _unitOfWork.CommitAsync();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Current balnce record is null");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new ArgumentNullException("Invalid rental payment date type");
                                                }
                                                //-------------[End - rental payment date type ---------------
                                            }
                                            else
                                            {
                                                throw new ArgumentNullException("Process config asiignment info cannot be null");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("There is no active shops");
                                    }
                                    //-------- [End - logic] ----------
                                }
                                //----

                                //*****
                                //Note: The values of other fields are included in the response.
                                //*****
                                shopRentalProcess.BackUpKey = uniqueFileName;
                                shopRentalProcess.ProcessType = ShopRentalProcessType.FineProcess;
                               await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                                //--------

                               await _unitOfWork.CommitAsync();

                                dbtransaction.Commit(); // Commit the transaction if everything succeeds

                                _logger.LogInformation("Fine Process Successfully Completed. {EventType}", "ShopRental");

                                return (true, "Fine Process Successfully Completed");
                            }
                            else
                            {
                                return (false, "Session verification was not successful.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Process Config setting should not be null to run fine process");
                    }
                }

                //catch (DbUpdateConcurrencyException ex)
                //{
                //    dbtransaction.Rollback();
                //    _logger.LogError(ex, "Optimistic concurrency error occurred during fine processing.{EventType}", "ShopRental");

                //    foreach (var entry in ex.Entries)
                //    {
                //        if (entry.Entity is ShopRentalProcess)
                //        {
                //            var proposedValues = entry.CurrentValues;
                //            var databaseValues = entry.GetDatabaseValues();

                //            if (databaseValues != null)
                //            {
                //                entry.OriginalValues.SetValues(databaseValues);
                //            }
                //        }
                //    }

                //    return (false, "Optimistic concurrency error occurred. Please retry.");
                //}




                catch (Exception ex)
                {

                    dbtransaction.Rollback(); // Rollback the transaction if an exception occurs
                    _logger.LogError(ex, "An error occurred during fine processing.{EventType}", "ShopRental");
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }
        //-----[ End : fine process] -------
       public async Task<(bool, string)> DailyFineProcess(Session session, HTokenClaim token)
        {
            //using (var dbtransaction = _unitOfWork.BeginTransaction())
            //{
                try
                {
                    ShopRentalProcess shopRentalProcess = new ShopRentalProcess();
                    shopRentalProcess.Id = null;

                    var processConfigurationSettingsForSabha = await _unitOfWork.ShopRentalProcessConfigaration.GetAllForSabha(token.sabhaId);
                    if (processConfigurationSettingsForSabha != null && processConfigurationSettingsForSabha.Any(p => p.FineCalTypeId == 1 || p.FineCalTypeId == 4))
                    {
                        foreach (var processConfigSetting in processConfigurationSettingsForSabha.Where(p => p.FineCalTypeId == 1 || p.FineCalTypeId == 4))
                        {
                                if (processConfigSetting.FineRateTypeId == 1)
                                {
                                    var activeShopsForSabha = await _unitOfWork.Shops.GetAllShopsForFineProcessBySabha(token.sabhaId, processConfigSetting.Id.Value, processConfigSetting.FineRateTypeId);
                                    if (activeShopsForSabha != null && activeShopsForSabha.Count() > 0)
                                    {
                                        foreach (var shop in activeShopsForSabha)
                                        {
                                            var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shop.Id.Value);
                                            if (processConfigAssignmentInfo != null)
                                            {
                                                if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId == 2)
                                                {
                                                    if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineCalTypeId == 1)
                                                    {

                                                        var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value,session.SessionDate.Year, session.SessionDate.Month);
                                                        if (currentMonthBalanceRecord != null)
                                                        {

                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, session.SessionDate.Day);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;

                                                            decimal thisMonthFineForShop = 0;

                                                            //thisMonthFineForShop = Math.Round((decimal)(currentMonthBalanceRecord.CurrentRentalAmount.Value * processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);

                                                            //currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                            //currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;

                                                            if (currentMonthBalanceRecord.IsCompleted == false && currentMonthBalanceRecord.OverPaymentAmount == 0)
                                                            {
                                                                var lastfineprocess = await getLastDailyFineRateShopRentalprocess(token.sabhaId);

                                                                int NoOfDates = 0;

                                                                if (lastfineprocess.ProceedDate.HasValue)
                                                                {
                                                                    DateTime sessionDateTime = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                    TimeSpan dateDifference = sessionDateTime - lastfineprocess.ProceedDate.Value.Date;
                                                                    NoOfDates = Math.Abs(dateDifference.Days);
                                                                }

                                                                if (NoOfDates > 0)
                                                                {
                                                                    for (int i = 0; i <= NoOfDates; i++)
                                                                    {
                                                                        thisMonthFineForShop += Math.Round((decimal)((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount) * processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);
                                                                    }

                                                                    currentMonthBalanceRecord.FineAmount += thisMonthFineForShop;
                                                                    currentMonthBalanceRecord.CurrentMonthNewFine += thisMonthFineForShop;

                                                                    if (token.IsFinalAccountsEnabled == 1)
                                                                    {
                                                                        var sessions = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                        if (sessions != null)
                                                                        {
                                                                            await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, sessions, token);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new FinalAccountException("No Active Session Found.");
                                                                        }
                                                                    }
                                                                }
                                                                DailyFineProcessLog dailyFineProcessLog = new DailyFineProcessLog();

                                                                dailyFineProcessLog.Id = null;
                                                                dailyFineProcessLog.DailyFixedFineAmount = processConfigSetting.FineFixAmount;
                                                                dailyFineProcessLog.NoOfDates = NoOfDates;
                                                                dailyFineProcessLog.TotalFineAmount = thisMonthFineForShop;
                                                                dailyFineProcessLog.ShopId = shop.Id.Value;
                                                                dailyFineProcessLog.CreatedAt = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                dailyFineProcessLog.CreatedBy = -1;
                                                                dailyFineProcessLog.ProcessConfigurationId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                                dailyFineProcessLog.BalanceId = currentMonthBalanceRecord.Id.Value;

                                                                await _unitOfWork.DailyFineProcessLog.AddAsync(dailyFineProcessLog);
                                                               // await _unitOfWork.CommitAsync();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Current balance record not found");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value,session.SessionDate.Year, session.SessionDate.Month);
                                                        if (currentMonthBalanceRecord != null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, session.SessionDate.Day);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;

                                                            decimal thisMonthFineForShop = 0;
                                                            //thisMonthFineForShop = Math.Round((decimal)((currentMonthBalanceRecord.CurrentRentalAmount.Value + (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) * processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);

                                                            //currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                            //currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;

                                                            if (currentMonthBalanceRecord.IsCompleted == false && currentMonthBalanceRecord.OverPaymentAmount == 0)
                                                            {
                                                                var lastfineprocess = await getLastDailyFineRateShopRentalprocess(token.sabhaId);
                                                                int NoOfDates = 0;

                                                                if (lastfineprocess.ProceedDate.HasValue)
                                                                {
                                                                    DateTime sessionDateTime = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                    TimeSpan dateDifference = sessionDateTime - lastfineprocess.ProceedDate.Value.Date;
                                                                    NoOfDates = Math.Abs(dateDifference.Days);


                                                                }
                                                                if (NoOfDates > 0)
                                                                {
                                                                    for (int i = 0; i <= NoOfDates; i++)
                                                                    {
                                                                      //  thisMonthFineForShop += Math.Round((decimal)(((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount) +( currentMonthBalanceRecord.ServiceChargeArreasAmount - currentMonthBalanceRecord.PaidServiceChargeArreasAmount)+ (currentMonthBalanceRecord.TYFine - currentMonthBalanceRecord.PaidTYFine)+ (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) * processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);
                                                                        thisMonthFineForShop += Math.Round((decimal)(((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount)   + (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) * processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);
                                                                    }

                                                                    currentMonthBalanceRecord.FineAmount += thisMonthFineForShop;
                                                                    currentMonthBalanceRecord.CurrentMonthNewFine += thisMonthFineForShop;

                                                                    if (token.IsFinalAccountsEnabled == 1)
                                                                    {
                                                                        var sessions = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                        if (sessions != null)
                                                                        {
                                                                            await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, sessions, token);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new FinalAccountException("No Active Session Found.");
                                                                        }
                                                                    }
                                                                }

                                                                DailyFineProcessLog dailyFineProcessLog = new DailyFineProcessLog();

                                                                dailyFineProcessLog.Id = null;
                                                                dailyFineProcessLog.DailyFixedFineAmount = processConfigSetting.FineFixAmount;
                                                                dailyFineProcessLog.NoOfDates = NoOfDates;
                                                                dailyFineProcessLog.TotalFineAmount = thisMonthFineForShop;
                                                                dailyFineProcessLog.ShopId = shop.Id.Value;
                                                                dailyFineProcessLog.CreatedAt = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                dailyFineProcessLog.CreatedBy = -1;
                                                                dailyFineProcessLog.ProcessConfigurationId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                                dailyFineProcessLog.BalanceId = currentMonthBalanceRecord.Id.Value;

                                                                await _unitOfWork.DailyFineProcessLog.AddAsync(dailyFineProcessLog);
                                                             //   await _unitOfWork.CommitAsync();
                                                            }

                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Current balance record not found");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Invalid rental payment date type");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("process configuration information cannot be null");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("No active shops ");
                                    }

                                    shopRentalProcess.ProcessType = ShopRentalProcessType.FineProcess;
                                    await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                                    //--------

                                    await _unitOfWork.CommitAsync();

                                    //dbtransaction.Commit(); // Commit the transaction if everything succeeds

                                    _logger.LogInformation("Fine Process Successfully Completed. {EventType}", "ShopRental");
                                    return (true, "fine process run successfully");
                                }
                                else if (processConfigSetting.FineRateTypeId == 4)
                                {
                                    var activeShopsForSabha = await _unitOfWork.Shops.GetAllShopsForFineProcessBySabha(token.sabhaId, processConfigSetting.Id.Value, processConfigSetting.FineRateTypeId);

                                    if (activeShopsForSabha != null && activeShopsForSabha.Count() > 0)
                                    {
                                        foreach (var shop in activeShopsForSabha)
                                        {
                                            var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shop.Id.Value);
                                            if (processConfigAssignmentInfo != null)
                                            {
                                                if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId == 2)
                                                {
                                                    if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineCalTypeId == 1)
                                                    {
                                                        var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value,session.SessionDate.Year, session.SessionDate.Month);
                                                        if (currentMonthBalanceRecord != null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, session.SessionDate.Day);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                            decimal thisMonthFineForShop = 0;
                                                            //thisMonthFineForShop = Math.Round((decimal)(currentMonthBalanceRecord.CurrentRentalAmount.Value + processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineFixAmount.Value), 2, MidpointRounding.AwayFromZero);
                                                            //currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                            //currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;

                                                            if (currentMonthBalanceRecord.IsCompleted == false && currentMonthBalanceRecord.OverPaymentAmount == 0)
                                                            {
                                                                var lastfineprocess = await getLastDailyFineRateShopRentalprocess(token.sabhaId);

                                                                int NoOfDates = 0;

                                                                if (lastfineprocess.ProceedDate.HasValue)
                                                                {
                                                                    DateTime sessionDateTime = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                    TimeSpan dateDifference = sessionDateTime - lastfineprocess.ProceedDate.Value.Date;
                                                                    NoOfDates = Math.Abs(dateDifference.Days);
                                                                }
                                                                if (NoOfDates > 0)
                                                                {
                                                                    for (int i = 0; i <= NoOfDates; i++)
                                                                    {
                                                                        thisMonthFineForShop += Math.Round((decimal)((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount) + processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineFixAmount.Value), 2, MidpointRounding.AwayFromZero);

                                                                    }

                                                                    currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                                    currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;

                                                                    if (token.IsFinalAccountsEnabled == 1)
                                                                    {
                                                                        var sessions = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                        if (sessions != null)
                                                                        {

                                                                            await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, sessions, token);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new FinalAccountException("No Active Session Found.");
                                                                        }
                                                                    }
                                                                }

                                                                DailyFineProcessLog dailyFineProcessLog = new DailyFineProcessLog();

                                                                dailyFineProcessLog.Id = null;
                                                                dailyFineProcessLog.DailyFixedFineAmount = processConfigSetting.FineFixAmount;
                                                                dailyFineProcessLog.NoOfDates = NoOfDates;
                                                                dailyFineProcessLog.TotalFineAmount = thisMonthFineForShop;
                                                                dailyFineProcessLog.ShopId = shop.Id.Value;
                                                                dailyFineProcessLog.CreatedAt = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                dailyFineProcessLog.CreatedBy = -1;
                                                                dailyFineProcessLog.ProcessConfigurationId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                                dailyFineProcessLog.BalanceId = currentMonthBalanceRecord.Id.Value;

                                                                await _unitOfWork.DailyFineProcessLog.AddAsync(dailyFineProcessLog);
                                                                //await _unitOfWork.CommitAsync();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Current balance record not found");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var currentMonthBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shop.Id.Value, session.SessionDate.Year, session.SessionDate.Month);
                                                        if (currentMonthBalanceRecord != null)
                                                        {
                                                            shopRentalProcess.Date = new DateOnly(currentMonthBalanceRecord.Year, currentMonthBalanceRecord.Month, session.SessionDate.Day);
                                                            shopRentalProcess.ProcessConfigId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;

                                                            decimal thisMonthFineForShop = 0;

                                                            //thisMonthFineForShop = Math.Round((decimal)((currentMonthBalanceRecord.CurrentRentalAmount.Value + (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) + processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);

                                                            //currentMonthBalanceRecord.FineAmount = thisMonthFineForShop;
                                                            //currentMonthBalanceRecord.CurrentMonthNewFine = thisMonthFineForShop;

                                                            if (currentMonthBalanceRecord.IsCompleted == false && currentMonthBalanceRecord.OverPaymentAmount == 0)
                                                            {
                                                                var lastfineprocess = await getLastDailyFineRateShopRentalprocess(token.sabhaId);

                                                                int NoOfDates = 0;

                                                                if (lastfineprocess.ProceedDate.HasValue)
                                                                {
                                                                    DateTime sessionDateTime = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                    TimeSpan dateDifference = sessionDateTime - lastfineprocess.ProceedDate.Value.Date;
                                                                    NoOfDates = Math.Abs(dateDifference.Days);
                                                                }
                                                                if (NoOfDates > 0)
                                                                {
                                                                    for (int i = 0; i <= NoOfDates; i++)
                                                                    {
                                                                        //thisMonthFineForShop += Math.Round((decimal)(((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount) + (currentMonthBalanceRecord.ServiceChargeArreasAmount - currentMonthBalanceRecord.PaidServiceChargeArreasAmount) + (currentMonthBalanceRecord.TYFine - currentMonthBalanceRecord.PaidTYFine) + (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) + processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineDailyRate.Value), 2, MidpointRounding.AwayFromZero);
                                                                        thisMonthFineForShop += Math.Round((decimal)(((currentMonthBalanceRecord.CurrentRentalAmount.Value - currentMonthBalanceRecord.PaidCurrentRentalAmount)  + (currentMonthBalanceRecord.ArrearsAmount - currentMonthBalanceRecord.PaidArrearsAmount)) + processConfigAssignmentInfo.ShopRentalProcessConfigaration.FineFixAmount.Value), 2, MidpointRounding.AwayFromZero);

                                                                    }
                                                                    currentMonthBalanceRecord.FineAmount += thisMonthFineForShop;
                                                                    currentMonthBalanceRecord.CurrentMonthNewFine += thisMonthFineForShop;

                                                                    if (token.IsFinalAccountsEnabled == 1)
                                                                    {
                                                                        var sessions = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                                                                        if (sessions != null)
                                                                        {

                                                                            await UpdateLedgerAccountsForFine(shop.Id.Value, thisMonthFineForShop, sessions, token);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new FinalAccountException("No Active Session Found.");
                                                                        }
                                                                    }
                                                                }
                                                                DailyFineProcessLog dailyFineProcessLog = new DailyFineProcessLog();

                                                                dailyFineProcessLog.Id = null;
                                                                dailyFineProcessLog.DailyFixedFineAmount = processConfigSetting.FineFixAmount;
                                                                dailyFineProcessLog.NoOfDates = NoOfDates;
                                                                dailyFineProcessLog.TotalFineAmount = thisMonthFineForShop;
                                                                dailyFineProcessLog.ShopId = shop.Id.Value;
                                                                dailyFineProcessLog.CreatedAt = session.SessionDate.ToDateTime(TimeOnly.MinValue);
                                                                dailyFineProcessLog.CreatedBy = -1;
                                                                dailyFineProcessLog.ProcessConfigurationId = processConfigAssignmentInfo.ShopRentalProcessConfigarationId;
                                                                dailyFineProcessLog.BalanceId = currentMonthBalanceRecord.Id.Value;

                                                                await _unitOfWork.DailyFineProcessLog.AddAsync(dailyFineProcessLog);
                                                                //await _unitOfWork.CommitAsync(); 
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Current balance record not found");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Invalid rental payment date type");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("Process configuration information cannot be null");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("No active shops ");
                                    }
                                    shopRentalProcess.ProcessType = ShopRentalProcessType.FineProcess;
                                    await _unitOfWork.ShopRentalProcess.AddAsync(shopRentalProcess);
                                    //--------
                                    await _unitOfWork.CommitAsync();

                                    //dbtransaction.Commit(); // Commit the transaction if everything succeeds

                                    _logger.LogInformation("Fine Process Successfully Completed. {EventType}", "ShopRental");

                                    return (true, "fine process completed successfully");
                                }
                                else
                                {

                                }
                        }
                    }
                    else
                    {
                        return (true, "No Daily Fine Configuration Found for Sabha !. So No need of Daily Fine Process");
                    }
                    return (true, "Error Occured When fetching all fine configurations for sabha");
                    //throw new Exception("Error Occured When fetching all fine configurations for sabha");
                }
                catch (Exception ex) {
                    //dbtransaction.Rollback();
                    return (false, ex.Message);
                }
            //}
        }


        //-----[ Start : functions related to fine process] -------
        //--------- [Start - CreateFineForShop] ------
        private async Task<decimal> CreateFineForShop(int shopId, DateOnly currentSessionDate, decimal currentRentalAmount, ShopRentalProcessConfigaration processConfigGroupDetails)
        {
            try
            {
                //------ [Start: rate validation according to fine rate type] ---
                decimal verified_fineDailyRate = 0;
                decimal verified_fineMonthlyRate = 0;
                decimal verified_fine1stMonthRate = 0;
                decimal verified_fine2ndMonthRate = 0;
                decimal verified_fine3rdMonthRate = 0;

                //---
                decimal calculatedFineForShop = 0;
                //---

                if (processConfigGroupDetails.FineRateTypeId == 1)
                {
                    //Daily
                    verified_fineDailyRate = (decimal)processConfigGroupDetails.FineDailyRate;

                    //calculatedFineForShop = await ProcessShopRentalDailyFineProcess(propertyId, shopId, currentSessionDate, processConfigGroupDetails.FineRateTypeId, processConfigGroupDetails.FineCalTypeId, processConfigGroupDetails.RentalPaymentDateTypeId, processConfigGroupDetails.FineChargingMethodId, processConfigGroupDetails.FineDate, verified_fineDailyRate, verified_fineMonthlyRate, verified_fine1stMonthRate, verified_fine2ndMonthRate, verified_fine3rdMonthRate, currentRentalAmount);
                }
                else if (processConfigGroupDetails.FineRateTypeId == 2)
                {
                    //Monthly
                    verified_fineMonthlyRate = (decimal)processConfigGroupDetails.FineMonthlyRate;

                    calculatedFineForShop = await ProcessShopRentalMonthlyFineProcess(shopId, currentSessionDate, processConfigGroupDetails.FineRateTypeId, processConfigGroupDetails.FineCalTypeId, processConfigGroupDetails.RentalPaymentDateTypeId, processConfigGroupDetails.FineChargingMethodId, processConfigGroupDetails.FineDate, verified_fineDailyRate, verified_fineMonthlyRate, verified_fine1stMonthRate, verified_fine2ndMonthRate, verified_fine3rdMonthRate, currentRentalAmount);
                }
                else
                {
                    //Month level wise
                    verified_fine1stMonthRate = (decimal)processConfigGroupDetails.Fine1stMonthRate;
                    verified_fine2ndMonthRate = (decimal)processConfigGroupDetails.Fine2ndMonthRate;
                    verified_fine3rdMonthRate = (decimal)processConfigGroupDetails.Fine3rdMonthRate;

                    //calculatedFineForShop = await ProcessShopRentalMonthlyLevelWiseFineProcess(propertyId, shopId, currentSessionDate, processConfigGroupDetails.FineRateTypeId, processConfigGroupDetails.FineCalTypeId, processConfigGroupDetails.RentalPaymentDateTypeId, processConfigGroupDetails.FineChargingMethodId, processConfigGroupDetails.FineDate, verified_fineDailyRate, verified_fineMonthlyRate, verified_fine1stMonthRate, verified_fine2ndMonthRate, verified_fine3rdMonthRate, currentRentalAmount);
                }
                //------ [End: rate validation according to fine rate type] -----


                if (calculatedFineForShop >= 0)
                {
                    return calculatedFineForShop;
                }
                else
                {
                    throw new ArgumentNullException("error while processing");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "Error occured.{EventType}", "ShopRental");
                return -1;
            }
        }
        //--------- [End - CreateFineForShop] ------


        //---------- [Start : Fine process]---------------------------------------------
        //#1 Daily
        //------------

        //#2 - Monthly
        private async Task<decimal> ProcessShopRentalMonthlyFineProcess(int shopId, DateOnly currentSessionDate, int fineRateTypeId, int fineCalTypeId, int rentalPaymentDateTypeId, int fineChargingMethodId, int fineProcessDate, decimal fineDailyRate, decimal fineMonthlyRate, decimal fine1stMonthRate, decimal fine2ndMonthRate, decimal fine3rdMonthRate, decimal currentRentalAmount)
        {
            try
            {
                decimal calculatedTotalFine = 0;

                //--------- [Start - get not completed balnces of the shop] ---------
                var lastBalanceRecord = await _shopRentalBalanceService.GetCurrentMonthBalanceInfo(shopId, currentSessionDate.Month == 1 ? (DateTime.Now.Year - 1) : DateTime.Now.Year, currentSessionDate.Month == 1 ? 12 : currentSessionDate.Month - 1);

                if (lastBalanceRecord != null)
                {
                    //#1 call CalculateShopRentalFine() method here
                    calculatedTotalFine = CalculateShopRentalFine(currentSessionDate, fineRateTypeId, fineCalTypeId, rentalPaymentDateTypeId, fineChargingMethodId, fineProcessDate, fineDailyRate, fineMonthlyRate, fine1stMonthRate, fine2ndMonthRate, fine3rdMonthRate, currentRentalAmount, lastBalanceRecord.Year, lastBalanceRecord.Month, lastBalanceRecord.ArrearsAmount.Value, lastBalanceRecord.PaidArrearsAmount.Value);
                    //---

                    return calculatedTotalFine;
                }
                else
                {
                    return -1;
                }
                //--------- [End - get not completed balnces of the shop] -----------
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during monthly fine processing.{EventType}", "Shop Renal");
                return -1;
            }
        }
        //------------







        //#3 - Monthly Level Wise
        //------------




        //private async Task<decimal> ProcessShopRentalFineProcess(int propertyId, int shopId, DateOnly currentSessionDate, int fineRateTypeId, int fineCalTypeId, int rentalPaymentDateTypeId, int fineChargingMethodId, int fineProcessDate, decimal fineDailyRate, decimal fineMonthlyRate, decimal fine1stMonthRate, decimal fine2ndMonthRate, decimal fine3rdMonthRate, decimal currentRentalAmount)
        //{
        //    try
        //    {
        //        decimal prevMonthlyTotalFine = 0;
        //        decimal calculatedTotalFine = 0;

        //        //--------- [Start - get not completed balnces of the shop] ---------
        //        var shopAllBalanceNotCompletedInfo = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceInformationOfShop(shopId);

        //        if (shopAllBalanceNotCompletedInfo != null && shopAllBalanceNotCompletedInfo.Count() != 0)
        //        {
        //            //#0 get year list
        //            var years = shopAllBalanceNotCompletedInfo.Select(y => y.Year).Distinct().ToList();

        //            //----
        //            foreach (var year in years)
        //            {
        //                for (int month = 1; month <= 12; month++)
        //                {
        //                    var shRowYearMonthlyBalance = await _unitOfWork.ShopRentalBalance.GetShopRentalNotCompletedBalanceByYearMonth(year, month, shopId);

        //                    if (shRowYearMonthlyBalance != null)
        //                    {
        //                        if (shRowYearMonthlyBalance.IsCompleted == false)
        //                        {
        //                            prevMonthlyTotalFine += (decimal)shRowYearMonthlyBalance.FineAmount;

        //                            if ((shRowYearMonthlyBalance.CurrentRentalAmount - shRowYearMonthlyBalance.PaidCurrentRentalAmount != 0))
        //                            {
        //                                //#1 call CalculateShopRentalFine() method here
        //                                calculatedTotalFine += CalculateShopRentalFine(currentSessionDate, fineRateTypeId, fineCalTypeId, rentalPaymentDateTypeId, fineChargingMethodId, fineProcessDate, fineDailyRate, fineMonthlyRate, fine1stMonthRate, fine2ndMonthRate, fine3rdMonthRate, shRowYearMonthlyBalance.CurrentRentalAmount.Value, shRowYearMonthlyBalance.Year, shRowYearMonthlyBalance.Month, shRowYearMonthlyBalance.ArrearsAmount.Value, shRowYearMonthlyBalance.PaidArrearsAmount.Value);
        //                                //---
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //----

        //            return (calculatedTotalFine - prevMonthlyTotalFine);
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //        //--------- [End - get not completed balnces of the shop] -----------
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.InnerException, "An error occurred during fine processing.{EventType}", "Shop Rental");
        //        return -1;
        //    }
        //}
        //---------- [End : Fine process]---------------------------------------------




        //--------- [Start - fine calculation] ------
        private decimal CalculateShopRentalFine(DateOnly currentSessionDate, int fineRateTypeId, int fineCalTypeId, int rentalPaymentDateTypeId, int fineChargingMethodId, int fineProcessDate, decimal fineDailyRate, decimal fineMonthlyRate, decimal fine1stMonthRate, decimal fine2ndMonthRate, decimal fine3rdMonthRate, decimal currentRentalAmount, int rowYear, int rowMonth, decimal rowMonthArreasAmount, decimal rowMonthArreasPaidAmount)
        {
            try
            {
                int arrearsDuration_days_original = 0;
                int arrearsDuration_days = 0;
                int arrearsDuration_months = 0;
                decimal calculatedFine = 0;
                decimal fineMultiplicationFactor = 0;

                var currentDate = currentSessionDate;
                var rowDate = new DateOnly(rowYear, rowMonth, fineProcessDate + 1);

                arrearsDuration_days_original = (currentDate.DayNumber - rowDate.DayNumber) + 1;

                if (arrearsDuration_days_original <= 0)
                {
                    throw new ArgumentNullException("Arreas duration cannot be 0 or negative for the fine process");
                }
                else
                {
                    if (arrearsDuration_days_original >= 30)
                    {
                        arrearsDuration_months = arrearsDuration_days_original / 30;

                        arrearsDuration_days = arrearsDuration_months * 30;
                    }

                    //-- [Start - Fine charging method] ----
                    if (fineChargingMethodId == 1)
                    {
                        //++ Start - for remaining arreas * rate ++

                        fineMultiplicationFactor = rowMonthArreasAmount - rowMonthArreasPaidAmount;
                    }
                    else
                    {
                        //++ Start - for rental * rate  ++

                        fineMultiplicationFactor = currentRentalAmount;
                    }
                    //-- [End - Fine charging method] ------



                    //--|| Start - Fine Rate Type ||--
                    if (fineRateTypeId == 1)
                    {
                        //Daily

                        if (fineCalTypeId == 1)
                        {
                            //fine for rental only
                            calculatedFine = Math.Round((decimal)(arrearsDuration_days * (fineMultiplicationFactor) * fineDailyRate), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            //fine for rental + arraes
                            calculatedFine = Math.Round((decimal)(arrearsDuration_days * (fineMultiplicationFactor + rowMonthArreasAmount) * fineDailyRate), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    else if (fineRateTypeId == 2)
                    {
                        // Monthly wise

                        if (fineCalTypeId == 1)
                        {
                            //fine for rental only
                            calculatedFine = Math.Round((decimal)(arrearsDuration_months * (fineMultiplicationFactor) * fineMonthlyRate), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            //fine for rental + arraes
                            calculatedFine = Math.Round((decimal)(arrearsDuration_months * (fineMultiplicationFactor + rowMonthArreasAmount) * fineMonthlyRate), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    else
                    {
                        // Month level wise

                        if (fineCalTypeId == 1)
                        {
                            //fine for rental only

                            if (arrearsDuration_months <= 3)
                            {
                                calculatedFine = Math.Round((decimal)(1 * (fineMultiplicationFactor) * fine1stMonthRate) + (1 * (fineMultiplicationFactor) * fine2ndMonthRate) + ((arrearsDuration_months - 2) * (fineMultiplicationFactor) * fine1stMonthRate), 2, MidpointRounding.AwayFromZero);                              
                            }
                            else if (arrearsDuration_months <= 2)
                            {
                                calculatedFine = Math.Round((decimal)(arrearsDuration_months * (fineMultiplicationFactor + rowMonthArreasAmount) * fineMonthlyRate), 2, MidpointRounding.AwayFromZero);
                            }
                            else if (arrearsDuration_months <= 1)
                            {
                                calculatedFine = Math.Round((decimal)(arrearsDuration_months * (fineMultiplicationFactor) * fine1stMonthRate), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                calculatedFine = 0;
                            }
                        }
                        else
                        {
                            //fine for rental + arraes

                            if (arrearsDuration_months <= 3)
                            {
                                calculatedFine = Math.Round((decimal)(1 * (fineMultiplicationFactor + rowMonthArreasAmount) * fine1stMonthRate) + (1 * (fineMultiplicationFactor + rowMonthArreasAmount) * fine2ndMonthRate) + ((arrearsDuration_months - 2) * (fineMultiplicationFactor + rowMonthArreasAmount) * fine1stMonthRate), 2, MidpointRounding.AwayFromZero);
                            }
                            else if (arrearsDuration_months <= 2)
                            {
                                calculatedFine = Math.Round((decimal)(1 * (fineMultiplicationFactor + rowMonthArreasAmount) * fine1stMonthRate) + ((arrearsDuration_months - 1) * (fineMultiplicationFactor + rowMonthArreasAmount) * fine2ndMonthRate), 2, MidpointRounding.AwayFromZero);
                            }
                            else if (arrearsDuration_months <= 1)
                            {
                                calculatedFine = Math.Round((decimal)(arrearsDuration_months * (fineMultiplicationFactor + rowMonthArreasAmount) * fine1stMonthRate), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                calculatedFine = 0;
                            }
                        }
                    }
                    //--|| End - Fine Rate Type   ||--

                    return (calculatedFine);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Error occurred");
            }
        }
        //--------- [End - fine calculation] -------
        //-----[ End : functions related to fine process] ---------
        //*********************************************************

        private async Task<bool> UpdateLedgerAccounts(int shopId, decimal billingAmount, decimal serviceBilling, decimal reduceBilling, decimal reduceServiceBilling, decimal reduceOverPay, Session session, HTokenClaim token)
        {
            try
            {

                var shop = await _unitOfWork.Shops.GetMonthlyProcessForFinalAccount(shopId);


                /*Income Account */


                var voteBalanceBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyRentalIncomeVoteId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceBilling == null)
                {
                    voteBalanceBilling = await _voteBalanceService.CreateNewVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyRentalIncomeVoteId, session, token);


                }

                if (voteBalanceBilling != null)
                {

                    voteBalanceBilling.Credit += (decimal)billingAmount;
                    voteBalanceBilling.UpdatedBy = token.userId;
                    voteBalanceBilling.UpdatedAt = session.StartAt;
                    voteBalanceBilling.SystemActionAt = DateTime.Now;

                    voteBalanceBilling.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceBilling.CreditDebitRunningBalance = voteBalanceBilling.Credit - voteBalanceBilling.Debit;
                    voteBalanceBilling.ExchangedAmount = (decimal)billingAmount;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceBilling);
                    voteBalanceBilling.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP Billing";
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
                    throw new Exception("Vote Balance Not Found For Rental Billing");
                }


                var voteBalanceServiceBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyServiceChargeIncomeVoteId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceServiceBilling == null)
                {
                    voteBalanceServiceBilling = await _voteBalanceService.CreateNewVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyServiceChargeIncomeVoteId, session, token);


                }

                if (voteBalanceServiceBilling != null)
                {

                    voteBalanceServiceBilling.Credit += (decimal)serviceBilling;
                    voteBalanceServiceBilling.UpdatedBy = token.userId;
                    voteBalanceServiceBilling.UpdatedAt = session.StartAt;
                    voteBalanceServiceBilling.SystemActionAt = DateTime.Now;

                    voteBalanceServiceBilling.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceServiceBilling.CreditDebitRunningBalance = voteBalanceServiceBilling.Credit - voteBalanceServiceBilling.Debit;
                    voteBalanceServiceBilling.ExchangedAmount = (decimal)serviceBilling;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceBilling);
                    voteBalanceServiceBilling.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP SVc Billing";
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
                    throw new Exception("Vote Balance Not Found For Service Billing");
                }


                /******* Asset Account Billing **********/


                var voteBalanceServiceArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ServiceChargeArreasAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceServiceArrears == null)
                {
                    voteBalanceServiceArrears = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ServiceChargeArreasAmountVoteDetailId, session, token);


                }


                if (voteBalanceServiceArrears != null)
                {

                    //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                    voteBalanceServiceArrears.Debit += (decimal)serviceBilling;
                    voteBalanceServiceArrears.UpdatedBy = token.userId;
                    voteBalanceServiceArrears.UpdatedAt = session.StartAt;
                    voteBalanceServiceArrears.SystemActionAt = DateTime.Now;

                    voteBalanceServiceArrears.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceServiceArrears.ExchangedAmount = (decimal)serviceBilling;




                    voteBalanceServiceArrears.CreditDebitRunningBalance = voteBalanceServiceArrears.Debit - voteBalanceServiceArrears.Credit;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceServiceArrears);
                    voteBalanceServiceArrears.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP SVC Billing";
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
                    throw new Exception("Vote Balance Not Found Service Arrears");
                }


                var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearArrearsAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceTYArrears == null)
                {
                    voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ThisYearArrearsAmountVoteDetailId, session, token);


                }

                if (voteBalanceTYArrears != null)
                {

                    voteBalanceTYArrears.Debit += (decimal)billingAmount;
                    voteBalanceTYArrears.UpdatedBy = token.userId;
                    voteBalanceTYArrears.UpdatedAt = session.StartAt;
                    voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                    voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                    voteBalanceTYArrears.ExchangedAmount = (decimal)billingAmount;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                    voteBalanceTYArrears.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP Billing";
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

                /******* Asset Account Update **********/

                if (voteBalanceServiceArrears != null)
                {

                    //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                    voteBalanceServiceArrears.Credit += (decimal)reduceServiceBilling;
                    voteBalanceServiceArrears.UpdatedBy = token.userId;
                    voteBalanceServiceArrears.UpdatedAt = session.StartAt;
                    voteBalanceServiceArrears.SystemActionAt = DateTime.Now;

                    voteBalanceServiceArrears.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceServiceArrears.ExchangedAmount = (decimal)reduceServiceBilling;




                    voteBalanceServiceArrears.CreditDebitRunningBalance = voteBalanceServiceArrears.Debit - voteBalanceServiceArrears.Credit;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceServiceArrears);
                    voteBalanceServiceArrears.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP SVC Deduction";
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
                    throw new Exception("Vote Balance Not Found Service Arrears");
                }


                if (voteBalanceTYArrears != null)
                {

                    voteBalanceTYArrears.Credit += (decimal)reduceBilling;
                    voteBalanceTYArrears.UpdatedBy = token.userId;
                    voteBalanceTYArrears.UpdatedAt = session.StartAt;
                    voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                    voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.Income;
                    voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                    voteBalanceTYArrears.ExchangedAmount = (decimal)reduceBilling;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                    voteBalanceTYArrears.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP Billing Deduction";
                    vtbLog.SubCode = shop.AgreementNo;
                    vtbLog.AppCategory = AppCategory.Shop_Rental;
                    vtbLog.ModulePrimaryKey = shop.Id;
                    vtbLog.OfficeId = shop.OfficeId;
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                    /**********/
                }
                else
                {
                    throw new Exception("Vote Balance Not Found For This Year Arrears");
                }

                var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.OverPaymentAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceOverPay == null)
                {
                    voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.OverPaymentAmountVoteDetailId, session, token);


                }

                if (voteBalanceOverPay != null)
                {
                    voteBalanceOverPay.Debit += (decimal)reduceOverPay;
                    voteBalanceOverPay.UpdatedBy = token.userId;
                    voteBalanceOverPay.UpdatedAt = session.StartAt;
                    voteBalanceOverPay.SystemActionAt = DateTime.Now;

                    voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.Income;
                    voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                    voteBalanceOverPay.ExchangedAmount = (decimal)shop.OpeningBalance.OverPaymentAmount!;


                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                    voteBalanceOverPay.ExchangedAmount = 0m;


                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP OverPay Deduction";
                    vtbLog.SubCode = shop.AgreementNo;
                    vtbLog.AppCategory = AppCategory.Shop_Rental;
                    vtbLog.ModulePrimaryKey = shop.Id;
                    vtbLog.OfficeId = shop.OfficeId;

                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                    /**********/

                }
                else
                {
                    throw new Exception("Vote Balance Not Found For Over Payment");
                }

                return true;

            }
            catch
            {
                throw;
            }
        }

        private async Task<bool> UpdateLedgerAccountsForFine(int shopId, decimal fineAmount, Session session, HTokenClaim token)
        {
            try
            {

                var shop = await _unitOfWork.Shops.GetMonthlyProcessForFinalAccount(shopId);


                var voteBalanceFineBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyFineIncomeVoteId, token.sabhaId, session.StartAt.Year);


                if (voteBalanceFineBilling == null)
                {
                    voteBalanceFineBilling = await _voteBalanceService.CreateNewVoteBalance(shop.RecievableIncomeVoteAssign!.PropertyFineIncomeVoteId, session, token);


                }

                if (voteBalanceFineBilling != null)
                {

                    voteBalanceFineBilling.Credit += (decimal)fineAmount;
                    voteBalanceFineBilling.UpdatedBy = token.userId;
                    voteBalanceFineBilling.UpdatedAt = session.StartAt;
                    voteBalanceFineBilling.SystemActionAt = DateTime.Now;
                    voteBalanceFineBilling.ExchangedAmount = (decimal)fineAmount;

                    voteBalanceFineBilling.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceFineBilling.CreditDebitRunningBalance = voteBalanceFineBilling.Debit - voteBalanceFineBilling.Credit;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceFineBilling);
                    voteBalanceFineBilling.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP Fine Billing";
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
                    throw new Exception("Vote Balance Not Found This year Fine Billing");
                }

                var voteBalanceTYFine = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearFineAmountVoteDetailId, token.sabhaId, session.StartAt.Year);

                if (voteBalanceTYFine == null)
                {
                    voteBalanceTYFine = await _voteBalanceService.CreateNewVoteBalance(shop.VoteAssign!.ThisYearFineAmountVoteDetailId, session, token);


                }

                if (voteBalanceTYFine != null)
                {

                    voteBalanceTYFine.Debit += (decimal)fineAmount;
                    voteBalanceTYFine.UpdatedBy = token.userId;
                    voteBalanceTYFine.UpdatedAt = session.StartAt;
                    voteBalanceTYFine.SystemActionAt = DateTime.Now;
                    voteBalanceTYFine.ExchangedAmount = (decimal)fineAmount;

                    voteBalanceTYFine.TransactionType = VoteBalanceTransactionTypes.Billing;
                    voteBalanceTYFine.CreditDebitRunningBalance = voteBalanceTYFine.Debit - voteBalanceTYFine.Credit;

                    /*vote balance log */
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYFine);
                    voteBalanceTYFine.ExchangedAmount = 0m;

                    vtbLog.Year = session.StartAt.Year;
                    vtbLog.Code = "SHP Fine Billing";
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
                    throw new Exception("Vote Balance Not Found This year Fine");
                }


                return true;
            }
            catch
            {
                throw;
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
