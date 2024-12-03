using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentPropertyTypeRepository : IRepository<AssessmentPropertyType>
    {
        Task<AssessmentPropertyType> GetById(int id);
        Task<IEnumerable<AssessmentPropertyType>> GetAll();
        Task<IEnumerable<AssessmentPropertyType>> GetAllForSabha(int sabhaid);

        Task<bool> IsRelationshipsExist(int id);

    }
}
