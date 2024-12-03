using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class AppCategoryService : IAppCategoryService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public AppCategoryService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AppCategory> CreateAppCategory(AppCategory newAppCategory)
        {
            await _unitOfWork.AppCategories
                .AddAsync(newAppCategory);
            await _unitOfWork.CommitAsync();

            return newAppCategory;
        }
        public async Task DeleteAppCategory(AppCategory appCategory)
        {
            _unitOfWork.AppCategories.Remove(appCategory);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<AppCategory>> GetAllAppCategorys()
        {
            return await _unitOfWork.AppCategories.GetAllAsync();
        }
        public async Task<AppCategory> GetAppCategoryById(int id)
        {
            return await _unitOfWork.AppCategories.GetByIdAsync(id);
        }
        public async Task UpdateAppCategory(AppCategory appCategoryToBeUpdated, AppCategory appCategory)
        {
            //appCategoryToBeUpdated.Name = appCategory.t;

            await _unitOfWork.CommitAsync();
        }
    }
}