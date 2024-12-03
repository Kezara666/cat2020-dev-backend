using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRepository : IRepository<Shop>
    {
        Task<Shop> GetById(int id);
        Task<IEnumerable<Shop>> GetAll();
        Task<IEnumerable<Shop>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Shop>> GetAllForOffice(int officeid);
        Task<Shop> GetByPropertyNo(string propertyNo);
        //Task<Shop> GetByPropertyId(int propertyId);
        Task<Shop> GetByAgreementNo(string agreementNo);
        Task<IEnumerable<Shop>> GetAllByPropertyTypeAndSabha(int propertyType, int sabhaId);
        Task<IEnumerable<Shop>> GetAllByPropertyTypeAndOffice(int propertyType, int officeId);
        Task<IEnumerable<Shop>> GetAllByPropertyNatureAndSabha(int propertyNature, int sabhaId);
        Task<IEnumerable<Shop>> GetAllByPropertyNatureAndOffice(int propertyNature, int officeId);
        Task<IEnumerable<Shop>> GetAllForProperty(int propertyId);

        //Task<IEnumerable<Shop>> GetAllForRentalPlace(int rentalPlaceId);
        Task<IEnumerable<Shop>> GetAllForFloor(int floorId);

        //----
        Task<IEnumerable<Shop>> GetAllZeroOpeningBlannceForFloor(int floorId);
        Task<IEnumerable<Shop>> GetAllZeroVotesForFloor(int floorId);

        Task<IEnumerable<Shop>> GetAllActiveShopForFloor(List<int?> propKeyIds, int floorId);
        //----


        Task<IEnumerable<Shop>> GetAllNotActiveShopsForSabha(int sabhaid);
        //----
        Task<IEnumerable<Shop>> GetAllActiveShopsForSabha(int sabhaid);
        Task<IEnumerable<Shop>> GetAllActiveShopsForOffice(int officeid);
        //----

        Task<IEnumerable<Shop>> GetAllShopWithoutActiveStatusForFloor(int floorId);

        //---
        Task<IEnumerable<Shop>> GetAllShopsForMonthendProcessBySabhaId(int sabhaId);
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

        //--
        Task<IEnumerable<Shop>> GetAllShopsByProcessConfigId(int processConfigId);
        //--

        Task<IEnumerable<Shop>> getAllShopsForSabhaAndPartnerId(int partnerId, int sabhaId);

        Task<IEnumerable<Shop>> GetAllZeroExpectedIncomeVotesForFloor(int floorId);


        Task<IEnumerable<Shop>> GetForUpdateVoteAssignForFinalAccounting(int sabhaId);
        Task<IEnumerable<Shop>> GetYearEndProcessForFinalAccount(int sabhaId);
        Task<IEnumerable<Shop>> GetYearEndProcessOpenBalForFinalAccount( List<int> shopIds);
        Task<Shop> GetMonthlyProcessForFinalAccount( int shopIds);
    }
}
