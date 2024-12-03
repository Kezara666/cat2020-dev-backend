using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IPropertyTypeRepository : IRepository<PropertyType>
    {
        Task<PropertyType> GetById(int id);
        Task<IEnumerable<PropertyType>> GetAll();
    }
}
