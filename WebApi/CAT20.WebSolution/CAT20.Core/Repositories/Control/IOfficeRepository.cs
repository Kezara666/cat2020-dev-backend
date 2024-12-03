using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Task<IEnumerable<Office>> GetAllWithOfficeAsync();
        // Task<Office> GetByIdAsync(int? officeId);
        Task<Office> GetWithOfficeByIdAsync(int id);
        Task<Office> GetOfficeByIdWithSabhaDetails(int id);
        Task<IEnumerable<Office>> GetAllWithOfficeBySabhaIdAsync(int Id);
        Task<List<int?>> GetAllWithOfficeIdsBySabhaIdAsync(int sabhaId);
        Task<IEnumerable<Office>> GetAllWithOfficeBySabhaIdAndOfficeTypeAsync(int Id, int otype);
    }
}
