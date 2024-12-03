using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IStreetService
    {
        Task<Street> GetById(int id);
        Task<Street> Create(Street obj);
        Task<bool> BulkCreate(IEnumerable<Street> objs);
        Task Update(Street objToBeUpdated, Street obj);
        Task<bool> Delete(Street obj);
        Task<IEnumerable<Street>> GetAll();
        Task<IEnumerable<Street>> GetAllForOffice(int officeid);
        Task<IEnumerable<Street>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Street>> GetAllForWard(int wardid);
    }
}
