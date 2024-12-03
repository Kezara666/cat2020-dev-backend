using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IDescriptionRepository : IRepository<Description>
    {
        Task<Description> GetById(int id);
        Task<IEnumerable<Description>> GetAll();
        Task<IEnumerable<Description>> GetAllForSabha(int sabhaid);

        Task<bool> IsRelationshipsExist(int id);
    }
}
