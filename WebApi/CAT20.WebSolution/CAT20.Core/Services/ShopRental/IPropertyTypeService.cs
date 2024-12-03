using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Services.ShopRental
{
    public interface IPropertyTypeService 
    {
        Task<PropertyType> GetById(int id);
        Task<PropertyType> Create(PropertyType obj);
        Task Delete(PropertyType obj);
        Task Update(PropertyType objToBeUpdated, PropertyType obj);
        Task<IEnumerable<PropertyType>> GetAll();
    }
}
