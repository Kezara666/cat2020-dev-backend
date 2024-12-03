using CAT20.Core.DTO.Assessment.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentService
    {
        Task<bool> ISAssessmentNoExist(string assessmentNo,int streetId, HTokenClaim token);
        Task<Assessment> GetById(int? id);

        Task<Assessment> Create(Assessment obj);

        Task<bool> InitBulkCreate(int sabhaId, HTokenClaim token);
        Task<bool> InitNextYearBalance(int? streetId, int? propertyId, int? assessmentId, HTokenClaim token);
        Task<bool> updatePartner(int sabhaId);
        Task<(int, bool)> SaveUpdateAssessmentPartner(Partner partner, int assessmentId);
        Task<IEnumerable<Assessment>> BulkCreate(IEnumerable<Assessment> objs);
        Task<(bool, string)> CreateAssessmentForNextYear(List<Assessment> assessments, HTokenClaim token);
        Task Update(Assessment objToBeUpdated, Assessment obj);
        Task Delete(Assessment obj);

        Task<(bool, string?)> UpdateAssessmentOrder(List<SaveAssessmentOrderNo> newOrderList,HTokenClaim token);
        Task<(bool,string?)> UpdateAssessmentOrderByPattern(int sabhaid);

        Task<List<int?>> GetAllKFormIdsFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<List<string?>> GetAllAssessmentNoFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<Assessment> getAssessmentForUpdate(string assessmentNo, Nullable<int> assessmentId, HTokenClaim token);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAll(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExclude(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, string? nic, string? name, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExcludeForWarrant(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, Nullable<int> quarter, int pageNo, int pageSize);
        Task<(int totalCount, IEnumerable<Assessment> list)> GetAllWarrantedList(List<int?> excludedIds, Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, Nullable<int> quarter, int pageNo, int pageSize);

        Task<IEnumerable<Assessment>> GetAllForOffice(int officeid);
        Task<IEnumerable<Assessment>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Assessment>> GetAllForCustomerId(int customerid);
        Task<IEnumerable<Assessment>> GetAllForWard(int wardid);
        Task<IEnumerable<Assessment>> GetAllForStreet(int streetid);
        Task<IEnumerable<Assessment>> GetAllForStreetWithOrdering(int streetId,HTokenClaim token);


        Task<Assessment> GetAssessmentForCustomize(int sabhaId,int assessmentId);
        Task<bool> CustomizeAssessmentRequest(int assessmentId, decimal newAllocation, int newPropertyId, int newDescriptionId, string requestNote, HTokenClaim token);
        Task<bool> ApproveDisapproveCustomization(int assessmentId, string actionNote, int state, HTokenClaim token);

        Task<IEnumerable<Assessment>> PendingCustomizationRequests(int sabhaid);
        Task<IEnumerable<Assessment>> PendingUpdateDescriptionForSabha(int sabhaid);
        Task<IEnumerable<Assessment>> PendingUpdatePropertyTypeForSabha(int sabhaid);
        
    }
}
