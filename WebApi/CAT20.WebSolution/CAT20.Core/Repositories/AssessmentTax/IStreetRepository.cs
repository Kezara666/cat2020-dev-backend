using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IStreetRepository : IRepository<Street>
    {
        Task<Street> GetById(int id);
        Task<IEnumerable<Street>> GetAll();
        Task<IEnumerable<Street>> GetAllForOffice(int officeid);
        Task<IEnumerable<Street>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Street>> GetAllForWard(int wardid);

        Task<bool> IsRelationshipsExist(int streetId);
    }
}
