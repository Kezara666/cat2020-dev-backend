using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IProcessConfigurationSettingAssignRepository: IRepository<ProcessConfigurationSettingAssign>
    {
        Task<IEnumerable<ProcessConfigurationSettingAssign>> GetAllForSabha(int sabhaid);

        Task<ProcessConfigurationSettingAssign> GetByShopId(int shopId);
    }
}
