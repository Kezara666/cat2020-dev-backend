using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<IEnumerable<District>> GetAllWithDistrictAsync();
        Task<District> GetWithDistrictByIdAsync(int id);
        Task<IEnumerable<District>> GetAllWithDistrictByDistrictIdAsync(int Id);
    }
}
