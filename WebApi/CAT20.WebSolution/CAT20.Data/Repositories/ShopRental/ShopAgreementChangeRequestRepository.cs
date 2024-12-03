using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopAgreementChangeRequestRepository : Repository<ShopAgreementChangeRequest>, IShopAgreementChangeRequestRepository
    {
        public ShopAgreementChangeRequestRepository(DbContext context) : base(context)
        {
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAllByShopAgreementChangeRequestIds(List<int?> shopAgreementChangeRequestIds)
        {
            return await shopRentalDbContext.ShopAgreementChangeRequest.Where(obl => shopAgreementChangeRequestIds.Contains(obl.Id)).ToListAsync();
        }



        public async Task<ShopAgreementChangeRequest> GetAgreementChangeRequestByShopId(int shopAgreementChangeRequestShopId)
        {
            return await shopRentalDbContext.ShopAgreementChangeRequest
                                            .Where(obl => obl.ShopId == shopAgreementChangeRequestShopId)
                                            .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalApprovedShopAgreementChangeRequestByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.ShopAgreementChangeRequest
                .Include(s => s.Shop)
                .ThenInclude(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(sacr => shopIds.Contains(sacr.ShopId) && sacr.ApproveStatus == 1).OrderByDescending(sacr => sacr.ApprovedAt).ToListAsync();
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalPendingShopAgreementChangeRequestByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.ShopAgreementChangeRequest
                .Include(s => s.Shop)
                .ThenInclude(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(sacr => shopIds.Contains(sacr.ShopId) && sacr.ApproveStatus == 0).OrderByDescending(sacr => sacr.ApprovedAt).ToListAsync();
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalRejectedShopAgreementChangeRequestByShopIds(List<int?> shopIds)
        {
            return await shopRentalDbContext.ShopAgreementChangeRequest
                .Include(s => s.Shop)
                .ThenInclude(p => p.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(sacr => shopIds.Contains(sacr.ShopId) && sacr.ApproveStatus == 2).OrderByDescending(sacr => sacr.ApprovedAt).ToListAsync();
        }
    }
}
