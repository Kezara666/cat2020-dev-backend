using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class ProvinceService : IProvinceService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public ProvinceService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Province> CreateProvince(Province newProvince)
        {
            await _unitOfWork.Provinces
                .AddAsync(newProvince);
            await _unitOfWork.CommitAsync();

            return newProvince;
        }
        public async Task DeleteProvince(Province province)
        {
            _unitOfWork.Provinces.Remove(province);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Province>> GetAllProvinces()
        {
            return await _unitOfWork.Provinces.GetAllAsync();
        }
        public async Task<Province> GetProvinceById(int id)
        {
            return await _unitOfWork.Provinces.GetByIdAsync(id);
        }
        public async Task UpdateProvince(Province provinceToBeUpdated, Province province)
        {
            //provinceToBeUpdated.Name = province.t;

            await _unitOfWork.CommitAsync();
        }
    }
}