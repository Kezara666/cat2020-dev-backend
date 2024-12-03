using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllDistricts();
        Task<District> GetDistrictById(int id);
        Task<District> CreateDistrict(District newDistrict);
        Task UpdateDistrict(District districtToBeUpdated, District district);
        Task DeleteDistrict(District district);
    }
}

