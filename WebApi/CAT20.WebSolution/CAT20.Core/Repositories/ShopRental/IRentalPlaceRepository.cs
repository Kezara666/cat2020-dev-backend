using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IRentalPlaceRepository : IRepository<RentalPlace>
    {
        Task<RentalPlace> GetById(int id);
        Task<IEnumerable<RentalPlace>> GetAll();
        Task<IEnumerable<RentalPlace>> GetAllForOffice(int officeid);
        Task<IEnumerable<RentalPlace>> GetAllForSabha(int sabhaid);
    }
}
