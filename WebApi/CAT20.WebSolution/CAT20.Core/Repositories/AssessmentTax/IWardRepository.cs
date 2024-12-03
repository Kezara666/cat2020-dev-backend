using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IWardRepository : IRepository<Ward>
    {
        Task<Ward> GetById(int id);
        Task<IEnumerable<Ward>> GetAll();
        Task<IEnumerable<Ward>> GetAllForOffice(int officeid);
        Task<IEnumerable<Ward>> GetAllForSabha(int sabhaid);

        Task<bool> IsRelationshipsExist(int wardId);

    }
}
