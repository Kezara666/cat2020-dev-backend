using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Services.ShopRental
{
    public interface IPropertyNatureService 
    {
        Task<PropertyNature> GetById(int id);
        Task<PropertyNature> Create(PropertyNature obj);
        Task Update(PropertyNature objToBeUpdated, PropertyNature obj);
        Task Delete(PropertyNature obj);
        Task<IEnumerable<PropertyNature>> GetAll();
        Task<IEnumerable<PropertyNature>> GetAllForSabha(int sabhaid);
    }
}
