using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Repositories.TradeTax
{
    public interface IBusinessNatureRepository : IRepository<BusinessNature>
    {
        Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureAsync();
        Task<BusinessNature> GetWithBusinessNatureByIdAsync(int id);
        Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureByBusinessNatureIdAsync(int Id);
        Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureBySabhaIdAsync(int Id);
        //Task DeleteBusinessNature(BookingProperty bookingProperty);
    }
}
