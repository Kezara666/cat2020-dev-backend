using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IFineRateTypeService
    {
       Task<IEnumerable<FineRateType>> GetAll();
    }
}
