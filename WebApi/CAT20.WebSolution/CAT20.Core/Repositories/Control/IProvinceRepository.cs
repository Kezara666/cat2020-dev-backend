using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IProvinceRepository : IRepository<Province>
    {
        Task<IEnumerable<Province>> GetAllWithProvinceAsync();
        Task<Province> GetWithProvinceByIdAsync(int id);
        Task<IEnumerable<Province>> GetAllWithProvinceByProvinceIdAsync(int provinceId);
    }
}
