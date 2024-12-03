using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IFineCalTypeRepository: IRepository<FineCalType>
    {
        Task<IEnumerable<FineCalType>> GetAll();
    }
}
