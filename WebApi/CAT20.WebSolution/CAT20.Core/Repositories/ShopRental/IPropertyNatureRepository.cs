using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IPropertyNatureRepository : IRepository<PropertyNature>
    {
        Task<PropertyNature> GetById(int id);
        Task<IEnumerable<PropertyNature>> GetAll();
        Task<IEnumerable<PropertyNature>> GetAllForSabha(int sabhaid);
    }
}
