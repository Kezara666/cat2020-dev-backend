using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Final;

namespace CAT20.Core.Repositories.Control
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<IEnumerable<Partner>> GetAllAsync();
        Task<Partner> GetByIdAsync(int id);
        Task<Partner> GetWithDocumentsByIdAsync(int id);
        Partner GetByIdSynchronously(int id);

        Task<Partner> GetByIdWithDetails(int id);

        Task<Partner> GetByNICAsync(string NIC);
        Task<Partner> GetByPassportNoAsync(string passport);
        Task<Partner> GetByPhoneNoAsync(string PhoneNo);
        Task<Partner> GetByEmailAsync(string email);
        Task<IEnumerable<Partner>> GetAllForPartnerTypeAsync(int Id);
        Task<IEnumerable<Partner>> GetAllForPartnersByIds(List<int?> ids);
        Task<IEnumerable<Partner>> GetAllForPartnersByIdsThenSearchNic(List<int?> ids, string? query);
        Task<IEnumerable<Partner>> GetAllForPartnersByIdsThenSearchName(List<int?> ids, string? query);
        Task<IEnumerable<Partner>> GetAllForSabhaAsync(int sabhaid);
        Task<IEnumerable<Partner>> GetAllBusinessesForSabhaAsync(int sabhaid);
        Task<IEnumerable<Partner>> GetAllCustomersForSabhaAsync(int sabhaid);
        Task<IEnumerable<Partner>> GetAllBusinessOwnersForSabhaAsync(int sabhaid);
        Task<IEnumerable<Partner>> GetAllBusinessOwnersForOfficeAsync(int sabhaid);
        Task<IEnumerable<Partner>> GetAllForPartnerTypeAndSabhaAsync(int Id, int sabhaid);

        Task<bool> IsBusinessRegNoExist(string businessRegNo);
        Task<Partner> GetBusinessByRegNo(string regNo);
        Task<Partner> GetBusinessByPhoneNo(string phoneNo);

        Task<(int totalCount, IEnumerable<Partner> list)> GetAllForPartnersBySearchQuery(int?sabhaId, List<int?> includedIds, string? query ,int pageNo, int pageSize);
    }
}
