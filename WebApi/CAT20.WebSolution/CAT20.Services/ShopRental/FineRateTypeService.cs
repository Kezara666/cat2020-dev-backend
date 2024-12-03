using CAT20.Core;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.ShopRental
{
    public class FineRateTypeService : IFineRateTypeService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public FineRateTypeService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FineRateType>> GetAll()
        {
            return await _unitOfWork.FineRateType.GetAll();
        }
    }
}
