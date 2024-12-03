using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentPropertyTypeService
    {
        Task<AssessmentPropertyType> GetById(int id);
        Task<AssessmentPropertyType> Create(AssessmentPropertyType obj);
        Task<bool> BulkCreate(IEnumerable<AssessmentPropertyType> objs);
        Task<bool> Update(AssessmentPropertyType obj, HTokenClaim token);
        Task<bool> Delete(AssessmentPropertyType obj);
        Task<IEnumerable<AssessmentPropertyType>> GetAll();
        Task<IEnumerable<AssessmentPropertyType>> GetAllForSabha(int sabhaid);
    }
}
