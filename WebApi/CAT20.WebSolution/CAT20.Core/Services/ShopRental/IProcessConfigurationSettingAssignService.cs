using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IProcessConfigurationSettingAssignService
    {
        Task<ProcessConfigurationSettingAssign> GetById(int id);
        Task<IEnumerable<ProcessConfigurationSettingAssign>> CreateMultiple(List<ProcessConfigurationSettingAssign> objs);
        Task Update(ProcessConfigurationSettingAssign objToBeUpdated, ProcessConfigurationSettingAssign obj);
        Task<IEnumerable<ProcessConfigurationSettingAssign>> GetAllForSabha(int sabhaid);
        Task<ProcessConfigurationSettingAssign> GetByShopId(int shopId);

        Task<ProcessConfigurationSettingAssign> Create(ProcessConfigurationSettingAssign obj);
    }
}
