using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Common.Envelop;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Services.ShopRental
{
    public interface IRentalPlaceService 
    {
        Task<RentalPlace> GetById(int id);
        Task<IEnumerable<RentalPlace>> GetAll();
        Task<IEnumerable<RentalPlace>> GetAllForOffice(int officeid);
        Task<IEnumerable<RentalPlace>> GetAllForSabha(int sabhaid);
        Task<RentalPlace> Create(RentalPlace obj);
        Task Update(RentalPlace objToBeUpdated, RentalPlace obj);
        Task Delete(RentalPlace obj);
        Task<TransferObject<RentalPlace>> Save(RentalPlace rentalPlace);
    }
}
