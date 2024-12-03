using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IWardService
    {
        Task<Ward> GetById(int id);
        Task<Ward> Create(Ward obj);
        Task<bool> BulkCreate(IEnumerable<Ward> objs);
        Task Update(Ward objToBeUpdated, Ward obj);
        Task<bool> Delete(Ward obj);
        Task<IEnumerable<Ward>> GetAll();
        Task<IEnumerable<Ward>> GetAllForOffice(int officeid);
        Task<IEnumerable<Ward>> GetAllForSabha(int sabhaid);
    }
}
