using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopAgreementActivityLogRepository : Repository<ShopAgreementActivityLog> , IShopAgreementActivityLogRepository
    {
        public ShopAgreementActivityLogRepository(DbContext context) : base(context)
        {

        }
    }
}
