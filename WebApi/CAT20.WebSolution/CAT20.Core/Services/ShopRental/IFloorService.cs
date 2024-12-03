using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Services.ShopRental
{
    public interface IFloorService 
    {
        Task<Floor> GetById(int id);
        Task<Floor> Create(Floor newFloor);
        Task Update(Floor floorToBeUpdated, Floor floor);
        Task Delete(Floor obj);
        Task<IEnumerable<Floor>> GetAll();
        Task<IEnumerable<Floor>> GetAllForRentalPlace(int rentalPlaceId);
    }
}
