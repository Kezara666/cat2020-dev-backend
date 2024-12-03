using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IDescriptionService
    {
        Task<Description> GetById(int id);
        Task<Description> Create(Description obj);
        Task<bool> BulkCreate(IEnumerable<Description> objs);
        Task Update(Description objToBeUpdated, Description obj);
        Task<bool> Delete(Description obj);
        Task<IEnumerable<Description>> GetAll();
        Task<IEnumerable<Description>> GetAllForSabha(int sabhaid);
    }
}
