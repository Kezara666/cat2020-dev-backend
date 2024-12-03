using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalProcessConfigarationService
    {
        Task<ShopRentalProcessConfigaration> GetById(int id);
        Task<ShopRentalProcessConfigaration> Create(ShopRentalProcessConfigaration newProcessConfig);
        Task Update(ShopRentalProcessConfigaration processConfigToBeUpdated, ShopRentalProcessConfigaration processConfig);
        Task Delete(ShopRentalProcessConfigaration obj);
        Task<IEnumerable<ShopRentalProcessConfigaration>> GetAllForSabha(int sabhaId);

        Task<ShopRentalProcessConfigaration> GetByProcessConfigId(int processConfigId);
    }
}
