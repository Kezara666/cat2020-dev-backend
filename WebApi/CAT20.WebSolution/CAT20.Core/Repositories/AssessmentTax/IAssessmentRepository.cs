using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentRepository : IRepository<Assessment>
    {
        Task<Assessment> GetById(int? id);
        Task<Assessment> GetForAmalgamationOrSubdivision(int? id,HTokenClaim token);
        Task<Assessment> GetForJournal(int? id);
        Task<Assessment> GetForCustomize(int? id);
        Task<Assessment> GetForAssetschnage(int? id);
        Task<IEnumerable<Assessment>> GetForPendingJournalRequest(int? sabhaId);
        Task<Assessment> GetAssessmentForJournal(int? sabhaId, int? kFormId);
       
        Task<Assessment> GetAssessmentForAssets(int? sabhaId, int? kFormId);



        Task<List<int?>> GetAllKFormIdsFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<List<string?>> GetAllAssessmentNoFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<Assessment>  getAssessmentForUpdate(string assessmentNo, Nullable<int> assessmentId, HTokenClaim token);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAll(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExclude(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, string? nic, string? name, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExcludeForWarrant(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, Nullable<int> quarter, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllWarrantedList(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, Nullable<int> quarter, int pageNo, int pageSize);
        Task<List<int?>> GetAllPartnerUpdatedPartnerIdsForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<IEnumerable<AssessmentTempPartner>> GetAllTempPartnersNicsForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<IEnumerable<AssessmentTempPartner>> GetAllTempPartnersNamesForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<IEnumerable<Assessment>> GetAllForOffice(int officeid);
        Task<IEnumerable<Assessment>> GetAllForCustomerId(int customerid);
        Task<IEnumerable<Assessment>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Assessment>> GetAllForWard(int wardid);
        Task<IEnumerable<Assessment>> GetAllForStreet(int streetid);
        Task<IEnumerable<Assessment>> GetAllForStreetWithOrdering(int streetId, HTokenClaim token);
        Task<IEnumerable<Assessment>> GetAllForCustomerIdAndSabhaId(int customerid, int sabhaId);
        Task<IEnumerable<Assessment>> GetAllForIds(List<int> assessmentIds);
        Task<IEnumerable<Assessment>> GetAllForIdsAndSabha(List<int> assessmentIds, int sabhaid);



        Task<IEnumerable<Assessment>> PendingCustomizationRequests(int sabhaid);
        Task<IEnumerable<Assessment>> PendingUpdateDescriptionForSabha(int sabhaid);
        Task<IEnumerable<Assessment>> PendingUpdatePropertyTypeForSabha(int sabhaid);



        // Process Repositories
        Task<IEnumerable<Assessment>> GetAllForSabhaToProcess(int sabhaId);
        Task<IEnumerable<Assessment>> GetInitProcessForSabha(int sabhaId);
        Task<IEnumerable<Assessment>> GetPartnerUpdateForSabha(int sabhaId);

        Task<IEnumerable<Assessment>> GetInitNextYearProcessForSabha(int? streetId, int? propertyId, int? assessmentId, HTokenClaim token);
        Task<Assessment> GetForFirstForInit(int sabhaId);

        Task<IEnumerable<Assessment>> GetQ1Warranting(int sabhaId);
        Task<IEnumerable<Assessment>> GetQ2Warranting(int sabhaId);
        Task<IEnumerable<Assessment>> GetQ3Warranting(int sabhaId);
        Task<IEnumerable<Assessment>> GetQ4Warranting(int sabhaId);
        Task<Assessment> GetWarrantAdjustment(int assessmentId);


        Task<IEnumerable<Assessment>> GetYearEndProcessForSabha(int sabhaId);
        Task<IEnumerable<Assessment>> GetYearEndProcessForFinalAccount(int sabhaId);
        Task<IEnumerable<Assessment>> GetForInitProcessForFinalAccount(List<int?> assessmentIds);


        Task<IEnumerable<AssessmentBalance>> GetForEndSessionToDisableTransaction(int officeId);




        Task<bool> HasAssessmentForSabha(int sabhaId);
        Task<bool> HasAssessmentForOffice(int officeId);
        Task<bool> ISKFormExist(int id, HTokenClaim token);
        Task<bool> ISAssessmentNoExist(string assessmentNo, int streetId, HTokenClaim token);



        Task<Assessment> GetAssessmentForCustomize(int sabhaId, int assessmentId);
        Task<IEnumerable<Assessment>> GetAssessmentOrderByPattern(int sabhaid, int streetId);
        Task<IEnumerable<Assessment>> GetAssessmentForRenewal(int sabhaid);
        Task<IEnumerable<Assessment>> GetAssessmentForRenewal(HTokenClaim token,int? propertyTypeId);


        /*for final account */

        Task<IEnumerable<Assessment>> GetForFinalAccountInit(int sabhaId);
    }
}
