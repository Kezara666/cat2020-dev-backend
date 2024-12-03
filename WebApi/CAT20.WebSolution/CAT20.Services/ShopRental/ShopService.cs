using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Resources;
using System.Transactions;
using CAT20.Common.Envelop;
using CAT20.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CAT20.Services.ShopRental
{

    public class ShopService : IShopService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public ShopService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Shop> GetById(int id)
        {
            return await _unitOfWork.Shops.GetById(id);
        }

        public async Task<IEnumerable<Shop>> GetAll()
        {
            return await _unitOfWork.Shops.GetAll();
        }

        public async Task<IEnumerable<Shop>> GetAllForSabha(int sabhaId)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllForSabha(sabhaId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllForOffice(int officeId)
        {
            return await _unitOfWork.Shops.GetAllForOffice(officeId);
        }

        //public async Task<Shop> GetByPropertyId(int propertyId)
        //{
        //    return await _unitOfWork.Shops.GetByPropertyId(propertyId);
        //}

        public async Task<Shop> GetByPropertyNo(string propertyNo)
        {
            return await _unitOfWork.Shops.GetByPropertyNo(propertyNo);
        }

        public async Task<Shop> GetByAgreementNo(string agreementNo)
        {
            return await _unitOfWork.Shops.GetByAgreementNo(agreementNo);
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyTypeAndSabha(int propertyType, int sabhaId)
        {
            return await _unitOfWork.Shops.GetAllByPropertyTypeAndSabha(propertyType, sabhaId);
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyTypeAndOffice(int propertyType, int officeId)
        {
            return await _unitOfWork.Shops.GetAllByPropertyTypeAndOffice(propertyType, officeId);
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyNatureAndSabha(int propertyNature, int sabhaId)
        {
            return await _unitOfWork.Shops.GetAllByPropertyNatureAndSabha(propertyNature, sabhaId);
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyNatureAndOffice(int propertyNature, int officeId)
        {
            return await _unitOfWork.Shops.GetAllByPropertyNatureAndOffice(propertyNature, officeId);
        }

        //public async Task<IEnumerable<Shop>> GetAllForRentalPlace(int rentalPlaceId)
        //{
        //    return await _unitOfWork.Shops.GetAllForRentalPlace(rentalPlaceId);
        //}

        public async Task<IEnumerable<Shop>> GetAllForFloor(int floorId)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllForFloor(floorId);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Shop>> GetAllForProperty(int propertyid)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllForProperty(propertyid);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<Shop> Create(Shop newShop)
        {
            try
            {
                await _unitOfWork.Shops
                .AddAsync(newShop);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newShop.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newShop.ID,
                //    TransactionName = "Shop",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
            }

            return newShop;
        }


        public async Task Update(Shop shopBeUpdated, Shop shop)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (rentalPlaceToBeUpdated.NameEnglish != rentalPlace.NameEnglish)
                //    note.Append(" English Name :" + rentalPlaceToBeUpdated.NameEnglish + " Change to " + rentalPlace.NameEnglish);
                //if (rentalPlaceToBeUpdated.NameSinhala != rentalPlace.NameSinhala)
                //    note.Append(" Sinhala Name :" + rentalPlaceToBeUpdated.NameSinhala + " Change to " + rentalPlace.NameSinhala);
                //if (rentalPlaceToBeUpdated.NameTamil != rentalPlace.NameTamil)
                //    note.Append(" Tamil Name :" + rentalPlaceToBeUpdated.NameTamil + " Change to " + rentalPlace.NameTamil);
                //if (rentalPlaceToBeUpdated.Code != rentalPlace.Code)
                //    note.Append(" Code :" + rentalPlaceToBeUpdated.Code + " Change to " + rentalPlace.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = rentalPlaceToBeUpdated.ID,
                //    TransactionName = "Shop",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                shopBeUpdated.BusinessName = shop.BusinessName;
                shopBeUpdated.BusinessNature = shop.BusinessNature;
                shopBeUpdated.BusinessRegistrationNo = shop.BusinessRegistrationNo;
                shopBeUpdated.CustomerDesigntion = shop.CustomerDesigntion;
                shopBeUpdated.AgreementNo = shop.AgreementNo;
                shopBeUpdated.AgreementStartDate = shop.AgreementStartDate;
                shopBeUpdated.AgreementEndDate = shop.AgreementEndDate;
                shopBeUpdated.Rental = shop.Rental;
                shopBeUpdated.KeyMoney = shop.KeyMoney;
                shopBeUpdated.SecurityDeposit = shop.SecurityDeposit;
                shopBeUpdated.ServiceCharge = shop.ServiceCharge;
                shopBeUpdated.Status = shop.Status;
                shopBeUpdated.IsApproved = shop.IsApproved;
                shopBeUpdated.ApprovedBy = shop.ApprovedBy;
                shopBeUpdated.ApprovedAt = shop.ApprovedAt;
                shopBeUpdated.UpdatedAt = shop.UpdatedAt;
                shopBeUpdated.UpdatedBy = shop.UpdatedBy;
                shopBeUpdated.PropertyId = shop.PropertyId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(Shop obj)
        {
            _unitOfWork.Shops.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        //------
        public async Task<IEnumerable<Shop>> GetAllZeroOpeningBlannceForFloor(int floorId)
        {
            return await _unitOfWork.Shops.GetAllZeroOpeningBlannceForFloor(floorId);
        }

        public async Task<IEnumerable<Shop>> GetAllZeroVotesForFloor(int floorId)
        {
            return await _unitOfWork.Shops.GetAllZeroVotesForFloor(floorId);
        }

        public async Task<IEnumerable<Shop>> GetAllActiveShopForFloor(List<int?> propKeyIds, int floorId)
        {
            return await _unitOfWork.Shops.GetAllActiveShopForFloor(propKeyIds, floorId);
        }
        //------


        //----
        public async Task ShopStatusChangeUpdate(int shopId, ShopStatus shopStatusNeedToChange, DateOnly agreementCloseDate)
        {
            //get shop by id
            var shopBeUpdated = await _unitOfWork.Shops.GetById(shopId);
            try
            {
                shopBeUpdated.AgreementCloseDate = agreementCloseDate;
                shopBeUpdated.Status = shopStatusNeedToChange;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }
        //----
        //----
        public async Task ShopAgreementEndDateExtendUpdate(int shopId, DateOnly agreementExtendEndDate)
        {
            using (var dbtransaction = _unitOfWork.BeginTransaction())
            {
                //get shop by id
                var shopBeUpdated = await _unitOfWork.Shops.GetById(shopId);
                try
                {
                    shopBeUpdated.AgreementEndDate = agreementExtendEndDate;
                    //shopBeUpdated.Status = ShopStatus.Active;

                    ShopAgreementActivityLog shopAgreementActivityLog = new ShopAgreementActivityLog(); 
                    shopAgreementActivityLog.Id = null;
                    shopAgreementActivityLog.AgreementExtendEndDate = agreementExtendEndDate;
                    shopAgreementActivityLog.CurrentAgreementEnddate = shopBeUpdated.AgreementEndDate;
                    shopAgreementActivityLog.ShopId = shopId;
                    shopAgreementActivityLog.SabhaId = shopBeUpdated.SabhaId;
                    shopAgreementActivityLog.OfficeId = shopBeUpdated.OfficeId;
                    shopAgreementActivityLog.CreatedAt = DateTime.Now;

                   await _unitOfWork.ShopAgreementActivityLog.AddAsync(shopAgreementActivityLog);

                    await _unitOfWork.CommitAsync();
                    dbtransaction.Commit();
                }

                catch (Exception)
                {
                    dbtransaction.Rollback();
                    throw;
                }
            }
        }
        //----
        //----
        public async Task<IEnumerable<Shop>> GetAllActiveShopsForSabha(int sabhaId)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllActiveShopsForSabha(sabhaId);
            }
            catch (Exception ex) { 
                return null;
            }
        }
        //----

        public async Task<IEnumerable<Shop>> GetAllNotActiveShopsForSabha(int sabhaId)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllNotActiveShopsForSabha(sabhaId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //----

        public async Task<IEnumerable<Shop>> GetAllActiveShopsForOffice(int officeId)
        {
            return await _unitOfWork.Shops.GetAllActiveShopsForOffice(officeId);
        }

        public async Task<IEnumerable<Shop>> GetAllShopWithoutActiveStatusForFloor(int floorId)
        {
            return await _unitOfWork.Shops.GetAllShopWithoutActiveStatusForFloor(floorId);
        }

        public async Task<IEnumerable<Shop>> GetAllShopsForMonthendProcessBySabhaId(int SabhaId)
        {
            try
            {
                return await _unitOfWork.Shops.GetAllShopsForMonthendProcessBySabhaId(SabhaId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllShopsForFineProcessBySabha(int sabhaId, int processConfigId, int fineRateTypeId)
        {
            return await _unitOfWork.Shops.GetAllShopsForFineProcessBySabha(sabhaId, processConfigId, fineRateTypeId);
        }
        //----

        //---
        public async Task<IEnumerable<Shop>> GetAllSabhaForProcessConfigSettingAssignment(int sabhaId)
        {
            return await _unitOfWork.Shops.GetAllSabhaForProcessConfigSettingAssignment(sabhaId);
        }
        //---

        //---
        public async Task<IEnumerable<Shop>> GetAllForPropertyForPaymentProcess(int propertyId)
        {
            return await _unitOfWork.Shops.GetAllForPropertyForPaymentProcess(propertyId);
        }
        //---

        public async Task<IEnumerable<Shop>> GetAllAgreementEndedShopsForOfficeAndDate(int officeId, DateOnly enddate)
        {
            return await _unitOfWork.Shops.GetAllAgreementEndedShopsForOfficeAndDate(officeId, enddate);
        }

        public async Task<IEnumerable<Shop>> GetAllShopsByProcessConfigId(int processConfigId)
        {
            return await _unitOfWork.Shops.GetAllShopsByProcessConfigId(processConfigId);
        }
        public async Task<IEnumerable<Shop>> getAllShopsForSabhaAndPartnerId(int partnerId, int sabhaId)
        {
            return await _unitOfWork.Shops.getAllShopsForSabhaAndPartnerId(partnerId, sabhaId);
        }

        public async Task<IEnumerable<Shop>> GetAllZeroExpectedIncomeVotesForFloor(int floorId)
        {
            return await _unitOfWork.Shops.GetAllZeroExpectedIncomeVotesForFloor(floorId);
        }
    }
}