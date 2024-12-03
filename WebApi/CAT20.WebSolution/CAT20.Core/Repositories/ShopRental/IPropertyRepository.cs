using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<Property> GetById(int id);
        Task<IEnumerable<Property>> GetAll();
        Task<IEnumerable<Property>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Property>> GetAllForOffice(int officeid);
        Task<IEnumerable<Property>> GetAllByPropertyTypeAndSabha(int propertyType, int sabhaId);
        Task<IEnumerable<Property>> GetAllByPropertyTypeAndOffice(int propertyType, int officeId);
        Task<IEnumerable<Property>> GetAllByPropertyNature(int propertyNature);
        Task<IEnumerable<Property>> GetAllByPropertyType(int propertyType);
        Task<IEnumerable<Property>> GetAllByPropertyNatureAndSabha(int propertyNature, int sabhaId);
        Task<IEnumerable<Property>> GetAllByPropertyNatureAndOffice(int propertyNature, int officeId);
        Task<Property> GetByPropertyNo(string propertyNo);
        //Task<IEnumerable<Property>> GetAllForRentalPlace(int rentalPlaceId);
        Task<IEnumerable<Property>> GetAllForFloor(int floorId);

        //---
        Task<IEnumerable<Property>> GetAllZeroOpeningBlannceForFloor(int floorId);
        //---

        //---
        Task<IEnumerable<Property>> GetAllZeroVotesForFloor(int floorId);
        //---

        //---
        Task<IEnumerable<Property>> GetAllZeroShopsForFloor(int floorId);
        //---

        //---
        Task<IEnumerable<Property>> GetByIds(List<int?> propertyKeyIds);
        //---
    }
}
