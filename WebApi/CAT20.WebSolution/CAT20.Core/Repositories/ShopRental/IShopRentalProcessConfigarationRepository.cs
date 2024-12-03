using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalProcessConfigarationRepository: IRepository<ShopRentalProcessConfigaration>
    {
        Task<ShopRentalProcessConfigaration> GetById(int id);

        Task<IEnumerable<ShopRentalProcessConfigaration>> GetAllForSabha(int sabhaId);

        Task<ShopRentalProcessConfigaration> GetByProcessConfigId(int processConfigId);
    }
}
