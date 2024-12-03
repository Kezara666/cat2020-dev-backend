using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopService 
    {
        Task<Shop> GetById(int id);
        Task<Shop> Create(Shop newShop);
        Task Update(Shop shopToBeUpdated, Shop shop);
        Task Delete(Shop obj);
        Task<IEnumerable<Shop>> GetAll();
        Task<IEnumerable<Shop>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Shop>> GetAllForOffice(int officeid);
        Task<Shop> GetByPropertyNo(string propertyNo);
        Task<IEnumerable<Shop>> GetAllForProperty(int propertyId);
        Task<Shop> GetByAgreementNo(string agreementNo);
        Task<IEnumerable<Shop>> GetAllByPropertyTypeAndSabha(int propertyType, int sabhaId);
        Task<IEnumerable<Shop>> GetAllByPropertyTypeAndOffice(int propertyType, int officeId);
        Task<IEnumerable<Shop>> GetAllByPropertyNatureAndSabha(int propertyNature, int sabhaId);
        Task<IEnumerable<Shop>> GetAllByPropertyNatureAndOffice(int propertyNature, int officeId);
        //Task<IEnumerable<Shop>> GetAllForRentalPlace(int rentalPlaceId);
        Task<IEnumerable<Shop>> GetAllForFloor(int floorId);

        //------
        Task<IEnumerable<Shop>> GetAllZeroOpeningBlannceForFloor(int floorId);

        Task<IEnumerable<Shop>> GetAllZeroVotesForFloor(int floorId);

        Task<IEnumerable<Shop>> GetAllActiveShopForFloor(List<int?> propKeyIds, int floorId);
        //------

        //------
        Task ShopStatusChangeUpdate(int shopId, ShopStatus shopStatusNeedToChange, DateOnly agreementCloseDate);

        Task ShopAgreementEndDateExtendUpdate(int shopId, DateOnly agreementExtendEndDate);
        //------



        //----
        Task<IEnumerable<Shop>> GetAllNotActiveShopsForSabha(int sabhaid);
        Task<IEnumerable<Shop>> GetAllActiveShopsForSabha(int sabhaid);
        Task<IEnumerable<Shop>> GetAllActiveShopsForOffice(int officeid);
        //----


        Task<IEnumerable<Shop>> GetAllShopWithoutActiveStatusForFloor(int floorId);


        //---
        Task<IEnumerable<Shop>> GetAllShopsForMonthendProcessBySabhaId(int SabhaId);
        //---

        //---
        Task<IEnumerable<Shop>> GetAllShopsForFineProcessBySabha(int sabhaId, int processConfigId, int fineRateTypeId);
        //---

        //---
        Task<IEnumerable<Shop>> GetAllSabhaForProcessConfigSettingAssignment(int sabhaId);
        //---


        //---
        Task<IEnumerable<Shop>> GetAllForPropertyForPaymentProcess(int propertyId);
        //---

        Task<IEnumerable<Shop>> GetAllAgreementEndedShopsForOfficeAndDate(int officeId, DateOnly enddate);

        //---
        Task<IEnumerable<Shop>> GetAllShopsByProcessConfigId(int processConfigId);
        //---

        Task<IEnumerable<Shop>> getAllShopsForSabhaAndPartnerId(int partnerId, int sabhaId);

        Task<IEnumerable<Shop>> GetAllZeroExpectedIncomeVotesForFloor(int floorId);
    }
}
