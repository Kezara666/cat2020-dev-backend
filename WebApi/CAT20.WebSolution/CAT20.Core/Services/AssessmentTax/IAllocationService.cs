using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAllocationService
    {
        Task<Allocation> GetById(int id);
        Task<Allocation> GetForAssessment(int assessementid);
        Task<Allocation> Create(Allocation obj);
        Task<IEnumerable<Allocation>> BulkCreate(IEnumerable<Allocation> objs);
        Task Update(Allocation objToBeUpdated, Allocation obj);
        Task Delete(Allocation obj);
        Task<IEnumerable<Allocation>> GetAll();
        Task<IEnumerable<Allocation>> GetAllForOffice(int officeid);
        Task<IEnumerable<Allocation>> GetAllForSabha(int sabhaid);

        Task<(bool, string?)> UpdateNextYearAllocations(int sabhaId);
    }
}
