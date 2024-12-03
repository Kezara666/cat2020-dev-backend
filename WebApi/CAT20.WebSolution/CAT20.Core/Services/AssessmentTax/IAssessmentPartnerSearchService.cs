using CAT20.Core.HelperModels;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentPartnerSearchService
    {

        Task<IEnumerable<HPartnerNIC>> GetPartnerSearchByNics(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);
        Task<IEnumerable<HPartnerName>> GetPartnerSearchByNames(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query);

    }
}
