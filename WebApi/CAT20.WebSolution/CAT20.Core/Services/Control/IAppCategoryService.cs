using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IAppCategoryService
    {
        Task<IEnumerable<AppCategory>> GetAllAppCategorys();
        Task<AppCategory> GetAppCategoryById(int id);
        Task<AppCategory> CreateAppCategory(AppCategory newAppCategory);
        Task UpdateAppCategory(AppCategory appCategoryToBeUpdated, AppCategory appCategory);
        Task DeleteAppCategory(AppCategory appCategory);
    }
}

