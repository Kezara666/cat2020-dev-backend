using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.Services.ShopRental;

namespace CAT20.Services.ShopRental
{
    public class ShopAgreementActivityLogService : IShopAgreementActivityLogService
    {

        private readonly IShopRentalUnitOfWork _unitOfWork;

        public ShopAgreementActivityLogService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
