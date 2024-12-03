using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IFloorRepository : IRepository<Floor>
    {
        Task<Floor> GetById(int id);
        Task<IEnumerable<Floor>> GetAll();
        Task<IEnumerable<Floor>> GetAllForRentalPlace(int rentalPlaceId);
    }
}
