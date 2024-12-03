using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IMonthRepository : IRepository<Month>
    {
        Task<IEnumerable<Month>> GetAllWithMonthAsync();
        Task<Month> GetWithMonthByIdAsync(int id);
        Task<IEnumerable<Month>> GetAllWithMonthByMonthIdAsync(int Id);
    }
}
