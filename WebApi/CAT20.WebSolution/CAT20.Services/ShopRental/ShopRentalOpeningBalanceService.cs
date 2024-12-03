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
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Vml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.ShopRental
{
    public class ShopRentalOpeningBalanceService : IShopRentalOpeningBalanceService
    {
        private readonly IMapper _mapper;
        private readonly IShopRentalUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;

        public ShopRentalOpeningBalanceService( IMapper mapper, IShopRentalUnitOfWork unitOfWork,IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<OpeningBalance> GetOpeningBalanceByShopId(int shopId)
        {
            try
            {
                return await _unitOfWork.ShopRentalOpeningBalance.GetOpeningBalanceByShopId(shopId);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OpeningBalance> GetById(int id)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetByIdAsync(id);
        }

        public async Task<OpeningBalance> Create(OpeningBalance obj)
        {
            try
            {
                //-------------- [Start - copy OpeningBalance record to ShopRentalBalance] ------------
                var property = await _unitOfWork.Properties.GetById(obj.PropertyId);
                var shop = await _unitOfWork.Shops.GetById(obj.ShopId);

                if (obj.LastYearArrearsAmount > 0 || obj.LastYearFineAmount > 0)
                {
                    //lastYear record + this year record will be created

                    //Last Year Record
                    var lastYearBalance = new ShopRentalBalance
                    {
                        //-------------------------------------------------------
                        PropertyId = obj.PropertyId,
                        ShopId = obj.ShopId,
                        OfficeId = obj.OfficeId,
                        SabhaId = obj.SabhaId,

                        Year = (obj.Year - 1), //last year
                        Month = 12, //last year last month = 12
                        FromDate = new DateOnly((obj.Year - 1), 1, 1),
                        ToDate = null,
                        //-------------------------------------------------------


                        //-------------------------------------------------------
                        ArrearsAmount = obj.LastYearArrearsAmount,
                        FineAmount = obj.LastYearFineAmount,
                        ServiceChargeArreasAmount = 0,
                        OverPaymentAmount = 0,
                        //-------------------------------------------------------

                        IsCompleted = false,
                        IsProcessed = false,
                        NoOfPayments = 0,
                        CreatedBy = obj.CreatedBy,
                        HasTransaction = false,

                        //------[Start: fields for Report]-------
                        LYFine = 0,
                        LYArreas = 0,
                        TYFine = 0,
                        TYArreas = 0,

                        CurrentMonthNewFine = obj.LastYearFineAmount,
                        TYLYServiceChargeArreas = 0, 

                        CurrentServiceChargeAmount = shop.ServiceCharge,
                        PaidCurrentServiceChargeAmount = shop.ServiceCharge, //because service charge arreas amount is set in TY Record 

                        CurrentRentalAmount = shop.Rental,
                        PaidCurrentRentalAmount = (shop.Rental - obj.LastYearArrearsAmount> 0) ? shop.Rental - obj.LastYearArrearsAmount : 0,
                        //------[End: fields for Report]---------
                    };

                    //This Year Record
                    var thisYearBalance = new ShopRentalBalance
                    {
                        //-------------------------------------------------------
                        PropertyId = obj.PropertyId,
                        ShopId = obj.ShopId,
                        OfficeId = obj.OfficeId,
                        SabhaId = obj.SabhaId,

                        Year = obj.Year, //Current year
                        Month = obj.MonthId,   //current month
                        FromDate = new DateOnly(obj.Year, obj.MonthId, 1),
                        ToDate = null,
                        //-------------------------------------------------------


                        //-------------------------------------------------------
                        ArrearsAmount = obj.ThisYearArrearsAmount, //this year Arreas amount
                        FineAmount = obj.ThisYearFineAmount, //this year Arreas amount
                        ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount,
                        OverPaymentAmount = 0,
                        //-------------------------------------------------------

                        IsCompleted = false,
                        IsProcessed = false,
                        NoOfPayments = 0,
                        CreatedBy = obj.CreatedBy,
                        HasTransaction = false,

                        //------[Start: fields for Report]-------
                        LYFine = obj.LastYearFineAmount,
                        LYArreas = obj.LastYearArrearsAmount,
                        TYFine = 0,
                        TYArreas = 0,

                        CurrentMonthNewFine = obj.ThisYearFineAmount,
                        TYLYServiceChargeArreas = 0,

                        CurrentServiceChargeAmount = shop.ServiceCharge,
                        PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0,

                        CurrentRentalAmount = shop.Rental,
                        PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0,
                        //------[End: fields for Report]---------
                    };

                    await _unitOfWork.ShopRentalBalance.AddAsync(lastYearBalance);
                    await _unitOfWork.ShopRentalBalance.AddAsync(thisYearBalance);
                    await _unitOfWork.CommitAsync();

                    obj.BalanceIdForLastYearArrears = lastYearBalance.Id;
                    obj.BalanceIdForCurrentBalance  = thisYearBalance.Id;
                }
                else if(obj.LastYearArrearsAmount == 0 && obj.LastYearFineAmount == 0)
                {
                    //this year record will be the only one created

                    var thisYearBalance = new ShopRentalBalance
                    {
                        PropertyId = obj.PropertyId,
                        ShopId = obj.ShopId,
                        OfficeId = obj.OfficeId,
                        SabhaId = obj.SabhaId,

                        Year = obj.Year, //Current year
                        Month = obj.MonthId,   //current month
                        FromDate = new DateOnly(obj.Year, obj.MonthId, 1),

                        ToDate = null,
                        //-------------------------------------------------------


                        //-------------------------------------------------------
                        ArrearsAmount = obj.ThisYearArrearsAmount, //this year Arreas amount
                        FineAmount = obj.ThisYearFineAmount, //this year Arreas amount
                        ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount,
                        OverPaymentAmount = obj.OverPaymentAmount,
                        //-------------------------------------------------------

                        IsCompleted = (obj.ThisYearArrearsAmount == 0 && obj.ThisYearFineAmount == 0 && obj.ServiceChargeArreasAmount == 0 && obj.OverPaymentAmount >= 0) ? true : false,
                        //IsCompleted = false,
                        IsProcessed = false,
                        NoOfPayments = 0,
                        CreatedBy = obj.CreatedBy,
                        HasTransaction = false,

                        //------[Start: fields for Report]-------
                        LYFine = obj.LastYearFineAmount,
                        LYArreas = obj.LastYearArrearsAmount,
                        TYFine = 0,
                        TYArreas = 0,

                        CurrentMonthNewFine = obj.ThisYearFineAmount,
                        TYLYServiceChargeArreas = 0,

                        CurrentServiceChargeAmount = shop.ServiceCharge,
                        PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0,

                        CurrentRentalAmount = shop.Rental,
                        PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0,
                        //------[End: fields for Report]---------
                    };

                    await _unitOfWork.ShopRentalBalance.AddAsync(thisYearBalance);
                    await _unitOfWork.CommitAsync();

                    obj.BalanceIdForCurrentBalance = thisYearBalance.Id;
                }
                else
                {
                    throw new Exception("Input values cannot be null!");
                }
                //-------------- [End - copy OpeningBalance record to ShopRentalBalance] --------------

                await _unitOfWork.ShopRentalOpeningBalance.AddAsync(obj);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return obj;
        }
        //----


        //----
        public async Task Update(OpeningBalance objToBeUpdated, OpeningBalance obj)
        {
            try
            {
                var bal = await _unitOfWork.ShopRentalBalance.GetByIdAsync(objToBeUpdated.BalanceIdForCurrentBalance!.Value);
                var today = DateOnly.FromDateTime(DateTime.Now);

                if (bal.IsProcessed != true)
                {
                    var property = await _unitOfWork.Properties.GetById(obj.PropertyId);
                    var shop = await _unitOfWork.Shops.GetById(obj.ShopId);

                    //------------------ [ Start : update balance table record ] -----------------------------
                    //case : 1 (prev- TY record only) | (updateReq - TY record only)
                    if (objToBeUpdated.BalanceIdForCurrentBalance.HasValue && !objToBeUpdated.BalanceIdForLastYearArrears.HasValue && obj.LastYearArrearsAmount == 0 && obj.LastYearFineAmount == 0)
                    {

                        bal.OfficeId = obj.OfficeId;
                        bal.SabhaId = obj.SabhaId;
                        bal.Year = obj.Year;
                        bal.Month = obj.MonthId;
                        bal.FromDate = new DateOnly(obj.Year, obj.MonthId, 1);

                        bal.ArrearsAmount = obj.ThisYearArrearsAmount;
                        bal.FineAmount = obj.ThisYearFineAmount;
                        bal.ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount;
                        bal.OverPaymentAmount = obj.OverPaymentAmount;

                        bal.IsCompleted = (obj.ThisYearArrearsAmount == 0 && obj.ThisYearFineAmount == 0 && obj.ServiceChargeArreasAmount == 0 && obj.OverPaymentAmount >= 0) ? true : false;
                        //bal.IsCompleted = false;
                        bal.UpdatedBy = obj.CreatedBy;
                        bal.UpdatedAt = DateTime.Now;

                        //------[Start: fields for Report]-------
                        bal.LYFine = obj.LastYearFineAmount;
                        bal.LYArreas = obj.LastYearArrearsAmount;
                        bal.TYFine = 0;
                        bal.TYArreas = 0;

                        bal.CurrentMonthNewFine = obj.ThisYearFineAmount;
                        bal.TYLYServiceChargeArreas = 0;

                        bal.CurrentServiceChargeAmount = shop.ServiceCharge;
                        bal.PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0;

                        bal.CurrentRentalAmount = shop.Rental;
                        bal.PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0;
                        //------[End: fields for Report]---------
                    }
                    else if (objToBeUpdated.BalanceIdForLastYearArrears.HasValue && objToBeUpdated.BalanceIdForLastYearArrears != 0)
                    {
                        //case : 2 (prev- TY + Ly record) | (updateReq - TY + LY record)
                        var lyearBal = await _unitOfWork.ShopRentalBalance.GetByIdAsync(objToBeUpdated.BalanceIdForLastYearArrears);

                        if(obj.LastYearArrearsAmount == 0 && obj.LastYearFineAmount == 0)
                        {
                            //# first step - remove LY balance record
                            _unitOfWork.ShopRentalBalance.Remove(lyearBal);
                            await _unitOfWork.CommitAsync();

                            //--------[Start - update - this year record]---------------------
                            //#2 this year record
                            //-------------------------------------------------------
                            bal.OfficeId = obj.OfficeId;
                            bal.SabhaId = obj.SabhaId;

                            bal.Year = obj.Year; //Current year
                            bal.Month = obj.MonthId;   //current month
                            bal.FromDate = new DateOnly(obj.Year, obj.MonthId, 1);

                            bal.ArrearsAmount = obj.ThisYearArrearsAmount; //this year Arreas amount
                            bal.FineAmount = obj.ThisYearFineAmount; //this year Arreas amount
                            bal.ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount;
                            bal.OverPaymentAmount = obj.OverPaymentAmount;
                            //-------------------------------------------------------

                            bal.IsCompleted = (obj.ThisYearArrearsAmount == 0 && obj.ThisYearFineAmount == 0 && obj.ServiceChargeArreasAmount == 0 && obj.OverPaymentAmount >= 0) ? true : false;
                            bal.UpdatedBy = obj.CreatedBy;
                            bal.UpdatedAt = DateTime.Now;

                            //------[Start: fields for Report]-------
                            bal.LYFine = obj.LastYearFineAmount;
                            bal.LYArreas = obj.LastYearArrearsAmount;
                            bal.TYFine = 0;
                            bal.TYArreas = 0;

                            bal.CurrentMonthNewFine = obj.ThisYearFineAmount;
                            bal.TYLYServiceChargeArreas = 0;

                            bal.CurrentServiceChargeAmount = shop.ServiceCharge;
                            bal.PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0;

                            bal.CurrentRentalAmount = shop.Rental;
                            bal.PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0;
                            //------[End: fields for Report]---------
                            //--------[End - update - this year record]---------------------

                            obj.BalanceIdForLastYearArrears = null;
                        }
                        else
                        {
                            //--------[Start - update - last year record]---------------------
                            lyearBal.OfficeId = obj.OfficeId;
                            lyearBal.SabhaId = obj.SabhaId;
                            lyearBal.Year = (obj.Year - 1);
                            lyearBal.Month = 12; //last year
                            lyearBal.FromDate = new DateOnly((obj.Year - 1), 1, 1);

                            lyearBal.ArrearsAmount = obj.LastYearArrearsAmount;
                            lyearBal.FineAmount = obj.LastYearFineAmount;
                            lyearBal.ServiceChargeArreasAmount = 0;
                            lyearBal.OverPaymentAmount = 0;

                            lyearBal.IsCompleted = false;
                            lyearBal.UpdatedBy = obj.CreatedBy;
                            lyearBal.UpdatedAt = DateTime.Now;

                            //------[Start: fields for Report]-------
                            lyearBal.LYFine = 0;
                            lyearBal.LYArreas = 0;
                            lyearBal.TYFine = 0;
                            lyearBal.TYArreas = 0;

                            bal.CurrentMonthNewFine = obj.LastYearFineAmount;
                            lyearBal.TYLYServiceChargeArreas = 0;

                            lyearBal.CurrentServiceChargeAmount = shop.ServiceCharge;
                            lyearBal.PaidCurrentServiceChargeAmount = shop.ServiceCharge; //because service charge arreas amount is set in TY Record 

                            lyearBal.CurrentRentalAmount = shop.Rental;
                            lyearBal.PaidCurrentRentalAmount = (shop.Rental - obj.LastYearArrearsAmount > 0) ? shop.Rental - obj.LastYearArrearsAmount : 0;
                            //------[End: fields for Report]---------
                            //--------[End -update - last year record]---------------------


                            //--------[Start - update - this year record]---------------------
                            //#2 this year record
                            //-------------------------------------------------------
                            bal.OfficeId = obj.OfficeId;
                            bal.SabhaId = obj.SabhaId;

                            bal.Year = obj.Year; //Current year
                            bal.Month = obj.MonthId;   //current month
                            bal.FromDate = new DateOnly(obj.Year, obj.MonthId, 1);

                            bal.ArrearsAmount = obj.ThisYearArrearsAmount; //this year Arreas amount
                            bal.FineAmount = obj.ThisYearFineAmount; //this year Arreas amount
                            bal.ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount;
                            bal.OverPaymentAmount = 0;
                            //-------------------------------------------------------

                            bal.IsCompleted = false;
                            bal.UpdatedBy = obj.CreatedBy;
                            bal.UpdatedAt = DateTime.Now;

                            //------[Start: fields for Report]-------
                            bal.LYFine = obj.LastYearFineAmount;
                            bal.LYArreas = obj.LastYearArrearsAmount;
                            bal.TYFine = 0;
                            bal.TYArreas = 0;

                            bal.CurrentMonthNewFine = obj.ThisYearFineAmount;
                            bal.TYLYServiceChargeArreas = 0;

                            bal.CurrentServiceChargeAmount = shop.ServiceCharge;
                            bal.PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0;

                            bal.CurrentRentalAmount = shop.Rental;
                            bal.PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0;
                            //------[End: fields for Report]---------
                            //--------[End - update - this year record]---------------------
                        }
                    }
                    else
                    {
                        //case : 2 (prev- TY) | (updateReq - TY + LY record)

                        //# first step - remove TY balance record
                        _unitOfWork.ShopRentalBalance.Remove(bal);
                        await _unitOfWork.CommitAsync();

                        //# second step - then create LY + TY Record again
                        if (obj.LastYearArrearsAmount > 0 || obj.LastYearFineAmount > 0)
                        {
                            //lastYear record + this year record will be created

                            //Last Year Record
                            var lastYearBalance = new ShopRentalBalance
                            {
                                //-------------------------------------------------------
                                PropertyId = obj.PropertyId,
                                ShopId = obj.ShopId,
                                OfficeId = obj.OfficeId,
                                SabhaId = obj.SabhaId,

                                Year = (obj.Year - 1), //last year
                                Month = 12, //last year last month = 12
                                FromDate = new DateOnly((obj.Year - 1), 1, 1),
                                ToDate = null,
                                //-------------------------------------------------------


                                //-------------------------------------------------------
                                ArrearsAmount = obj.LastYearArrearsAmount,
                                FineAmount = obj.LastYearFineAmount,
                                ServiceChargeArreasAmount = 0,
                                OverPaymentAmount = 0,
                                //-------------------------------------------------------

                                IsCompleted = false,
                                IsProcessed = false,
                                NoOfPayments = 0,
                                CreatedBy = obj.CreatedBy,
                                HasTransaction = false,

                                //------[Start: fields for Report]-------
                                LYFine = 0,
                                LYArreas = 0,
                                TYFine = 0,
                                TYArreas = 0,

                                CurrentMonthNewFine = obj.LastYearFineAmount,
                                TYLYServiceChargeArreas = 0,

                                CurrentServiceChargeAmount = shop.ServiceCharge,
                                PaidCurrentServiceChargeAmount = shop.ServiceCharge, //because service charge arreas amount is set in TY Record 

                                CurrentRentalAmount = shop.Rental,
                                PaidCurrentRentalAmount = (shop.Rental - obj.LastYearArrearsAmount > 0) ? shop.Rental - obj.LastYearArrearsAmount : 0,
                                //------[End: fields for Report]---------
                            };

                            //This Year Record
                            var thisYearBalance = new ShopRentalBalance
                            {
                                //-------------------------------------------------------
                                PropertyId = obj.PropertyId,
                                ShopId = obj.ShopId,
                                OfficeId = obj.OfficeId,
                                SabhaId = obj.SabhaId,

                                Year = obj.Year, //Current year
                                Month = obj.MonthId,   //current month
                                FromDate = new DateOnly(obj.Year, obj.MonthId, 1),
                                ToDate = null,
                                //-------------------------------------------------------


                                //-------------------------------------------------------
                                ArrearsAmount = obj.ThisYearArrearsAmount, //this year Arreas amount
                                FineAmount = obj.ThisYearFineAmount, //this year Arreas amount
                                ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount,
                                OverPaymentAmount = 0,
                                //-------------------------------------------------------

                                IsCompleted = false,
                                IsProcessed = false,
                                NoOfPayments = 0,
                                CreatedBy = obj.CreatedBy,
                                HasTransaction = false,

                                //------[Start: fields for Report]-------
                                LYFine = obj.LastYearFineAmount,
                                LYArreas = obj.LastYearArrearsAmount,
                                TYFine = 0,
                                TYArreas = 0,

                                CurrentMonthNewFine = obj.ThisYearFineAmount,
                                TYLYServiceChargeArreas = 0,

                                CurrentServiceChargeAmount = shop.ServiceCharge,
                                PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0,

                                CurrentRentalAmount = shop.Rental,
                                PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0,
                                //------[End: fields for Report]---------
                            };

                            await _unitOfWork.ShopRentalBalance.AddAsync(lastYearBalance);
                            await _unitOfWork.ShopRentalBalance.AddAsync(thisYearBalance);
                            await _unitOfWork.CommitAsync();

                            obj.BalanceIdForLastYearArrears = lastYearBalance.Id;
                            obj.BalanceIdForCurrentBalance = thisYearBalance.Id;
                        }
                        else if (obj.LastYearArrearsAmount == 0 && obj.LastYearFineAmount == 0)
                        {
                            //this year record will be the only one created
                            var thisYearBalance = new ShopRentalBalance
                            {
                                PropertyId = obj.PropertyId,
                                ShopId = obj.ShopId,
                                OfficeId = obj.OfficeId,
                                SabhaId = obj.SabhaId,

                                Year = obj.Year, //Current year
                                Month = obj.MonthId,   //current month
                                FromDate = new DateOnly(obj.Year, obj.MonthId, 1),

                                ToDate = null,
                                //-------------------------------------------------------


                                //-------------------------------------------------------
                                ArrearsAmount = obj.ThisYearArrearsAmount, //this year Arreas amount
                                FineAmount = obj.ThisYearFineAmount, //this year Arreas amount
                                ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount,
                                OverPaymentAmount = obj.OverPaymentAmount,
                                //-------------------------------------------------------

                                IsCompleted = (obj.ThisYearArrearsAmount == 0 && obj.ThisYearFineAmount == 0 && obj.ServiceChargeArreasAmount == 0 && obj.OverPaymentAmount >= 0) ? true : false,
                                //IsCompleted = false,
                                IsProcessed = false,
                                NoOfPayments = 0,
                                CreatedBy = obj.CreatedBy,
                                HasTransaction = false,

                                //------[Start: fields for Report]-------
                                LYFine = obj.LastYearFineAmount,
                                LYArreas = obj.LastYearArrearsAmount,
                                TYFine = 0,
                                TYArreas = 0,

                                CurrentMonthNewFine = obj.ThisYearFineAmount,
                                TYLYServiceChargeArreas = 0,

                                CurrentServiceChargeAmount = shop.ServiceCharge,
                                PaidCurrentServiceChargeAmount = (shop.ServiceCharge - obj.ServiceChargeArreasAmount > 0) ? shop.ServiceCharge - obj.ServiceChargeArreasAmount : 0,

                                CurrentRentalAmount = shop.Rental,
                                PaidCurrentRentalAmount = (shop.Rental - obj.ThisYearArrearsAmount > 0) ? shop.Rental - obj.ThisYearArrearsAmount : 0,
                                //------[End: fields for Report]---------
                            };

                            await _unitOfWork.ShopRentalBalance.AddAsync(thisYearBalance);
                            await _unitOfWork.CommitAsync();

                            obj.BalanceIdForCurrentBalance = thisYearBalance.Id;
                        }
                        else
                        {
                            throw new Exception("Input values cannot be null!");
                        }
                    }
                    //------------------ [ End : update balance table record ] ------------------------------



                    //------------------ [ Start : update opening balance table record ] ------------------------------
                    objToBeUpdated.Year = obj.Year;
                    objToBeUpdated.MonthId = obj.MonthId;
                    objToBeUpdated.LastYearArrearsAmount = obj.LastYearArrearsAmount;
                    objToBeUpdated.ThisYearArrearsAmount = obj.ThisYearArrearsAmount;
                    objToBeUpdated.LastYearFineAmount = obj.LastYearFineAmount;
                    objToBeUpdated.ThisYearFineAmount = obj.ThisYearFineAmount;
                    objToBeUpdated.ServiceChargeArreasAmount = obj.ServiceChargeArreasAmount;
                    objToBeUpdated.OverPaymentAmount = obj.OverPaymentAmount;
                    objToBeUpdated.ApproveStatus = 0; //0-pending 

                    objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                    objToBeUpdated.UpdatedAt = DateTime.Now;

                    objToBeUpdated.BalanceIdForLastYearArrears = obj.BalanceIdForLastYearArrears;
                    objToBeUpdated.BalanceIdForCurrentBalance  = obj.BalanceIdForCurrentBalance;

                    await _unitOfWork.CommitAsync();
                    //------------------ [ End   : update opening balance table record ] ------------------------------
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

            }
        }


        public async Task<IEnumerable<OpeningBalance>> GetOpeningBalancesByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetOpeningBalancesByShopIds(shopKeyIds);
        }


        public async Task<IEnumerable<OpeningBalance>> GetAprovalPendingOpeningBalancesByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetAprovalPendingOpeningBalancesByShopIds(shopKeyIds);
        }

        public async Task<IEnumerable<OpeningBalance>> GetAprovalRejectedOpeningBalancesByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetAprovalRejectedOpeningBalancesByShopIds(shopKeyIds);
        }

        public async Task<IEnumerable<OpeningBalance>> GetAprovalApprovedOpeningBalancesByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetAprovalApprovedOpeningBalancesByShopIds(shopKeyIds);
        }

        public async Task RejectShopRentalOpeningBalance(OpeningBalance objToBeUpdated, OpeningBalance obj)
        {
            objToBeUpdated.ApprovedBy = obj.ApprovedBy;
            objToBeUpdated.ApprovedAt = DateTime.Now;
            objToBeUpdated.ApproveComment = obj.ApproveComment;
            objToBeUpdated.ApproveStatus = 2; // 2-rejected

            await _unitOfWork.CommitAsync();
        }

        public async Task<(bool,string?)> ApproveShopRentalOpeningBalance(List<int?> openingBalanceIds, int approvedby,HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

              
                try
                {
                    var approvedOBL = await _unitOfWork.ShopRentalOpeningBalance.GetAllByOpeningBalanceIds(openingBalanceIds);
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    if (session != null)
                    {

                        foreach (var obl in approvedOBL)
                        {
                            obl.ApprovedBy = approvedby;
                            obl.ApprovedAt = DateTime.Now;
                            obl.ApproveStatus = 1; // 1-approved
                        }

                        if (token.IsFinalAccountsEnabled==1)
                        {
                            await UpdateLedgerAccounts(approvedOBL.Select(m => m.ShopId).ToList(), session, token);
                        }

                        await _unitOfWork.CommitAsync();
                        transaction.Commit();
                        return (true, "Approved Successfully");
                    }
                    else
                    {
                        throw new GeneralException("No Active Session");
                    }
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
                }
               
        }

        public async Task IsProcessedStatusUpdate(OpeningBalance objToBeStatusUpdated)
        {
            objToBeStatusUpdated.IsProcessed = true;
            await _unitOfWork.CommitAsync();
        }

        //---
        public async Task<IEnumerable<OpeningBalance>> GetAllByPropertyIdShopId(int propertyId, int shopId)
        {
            return await _unitOfWork.ShopRentalOpeningBalance.GetAllByPropertyIdShopId(propertyId, shopId);
        }
        //---



        public async Task<bool> UpdateLedgerAccounts(List<int> shopIds,Session  session, HTokenClaim token)
        {
            
                try
                {



                 
                    if (session != null)
                    {

                        var shops = await _unitOfWork.Shops.GetYearEndProcessOpenBalForFinalAccount(shopIds);



                        var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                        var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

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


                            var voteBalanceLYFine = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.LastYearFineAmountVoteDetailId, token.sabhaId, session.StartAt.Year);

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



                            var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.LastYearArreasAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


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

                            var voteBalanceTYFine = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearFineAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


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

                            var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ThisYearArrearsAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


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

                            var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.OverPaymentAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


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

                            var voteBalanceServiceArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(shop.VoteAssign!.ServiceChargeArreasAmountVoteDetailId, token.sabhaId, session.StartAt.Year);


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
                                voteBalanceServiceArrears.ExchangedAmount = (decimal)shop.OpeningBalance.ServiceChargeArreasAmount!;




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
                        throw new FinalAccountException("Session Not Found");
                    }

                return true;
                }
                catch (Exception ex)
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
