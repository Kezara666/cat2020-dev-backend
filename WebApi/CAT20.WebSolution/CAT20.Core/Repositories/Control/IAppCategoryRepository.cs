using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IAppCategoryRepository : IRepository<AppCategory>
    {
        Task<IEnumerable<AppCategory>> GetAllWithAppCategoryAsync();
        Task<AppCategory> GetWithAppCategoryByIdAsync(int id);
        Task<IEnumerable<AppCategory>> GetAllWithAppCategoryByAppCategoryIdAsync(int Id);
    }
}
