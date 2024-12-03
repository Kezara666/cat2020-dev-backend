using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ISabhaService
    {
        Task<IEnumerable<Sabha>> GetAllSabhas();
        Task<Sabha> GetSabhaById(int id);
        Task<IEnumerable<Sabha>> GetSabhaByDistrictId(int districtID);
        Task<Sabha> CreateSabha(Sabha newSabha);
        Task UpdateSabha(Sabha sabhaToBeUpdated, Sabha sabha);
        Task DeleteSabha(Sabha sabha);
    }
}

