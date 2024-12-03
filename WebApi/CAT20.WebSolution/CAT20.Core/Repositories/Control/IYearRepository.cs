using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IYearRepository : IRepository<Year>
    {
        Task<IEnumerable<Year>> GetAllWithYearAsync();
        Task<Year> GetWithYearByIdAsync(int id);
        Task<IEnumerable<Year>> GetAllWithYearByYearIdAsync(int provinceId);
    }
}
