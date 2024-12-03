using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IOfficeTypeRepository : IRepository<OfficeType>
    {
        Task<IEnumerable<OfficeType>> GetAllWithOfficeTypeAsync();
        Task<OfficeType> GetWithOfficeTypeByIdAsync(int id);
        Task<IEnumerable<OfficeType>> GetAllWithOfficeTypeByOfficeTypeIdAsync(int Id);
    }
}
