using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> GetAllProvinces();
        Task<Province> GetProvinceById(int id);
        Task<Province> CreateProvince(Province newProvince);
        Task UpdateProvince(Province provinceToBeUpdated, Province province);
        Task DeleteProvince(Province province);
    }
}

