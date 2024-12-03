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
    public class FineChargingMethodService : IFineChargingMethodService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public FineChargingMethodService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<FineChargingMethod>> GetAll()
        {
            return await _unitOfWork.FineChargingMethod.GetAll();
        }
    }
}
