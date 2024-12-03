using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class DistrictService : IDistrictService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public DistrictService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<District> CreateDistrict(District newDistrict)
        {
            await _unitOfWork.Districts
                .AddAsync(newDistrict);
            await _unitOfWork.CommitAsync();

            return newDistrict;
        }
        public async Task DeleteDistrict(District district)
        {
            _unitOfWork.Districts.Remove(district);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<District>> GetAllDistricts()
        {
            return await _unitOfWork.Districts.GetAllAsync();
        }
        public async Task<District> GetDistrictById(int id)
        {
            return await _unitOfWork.Districts.GetByIdAsync(id);
        }
        public async Task UpdateDistrict(District districtToBeUpdated, District district)
        {
            //districtToBeUpdated.Name = district.t;

            await _unitOfWork.CommitAsync();
        }
    }
}