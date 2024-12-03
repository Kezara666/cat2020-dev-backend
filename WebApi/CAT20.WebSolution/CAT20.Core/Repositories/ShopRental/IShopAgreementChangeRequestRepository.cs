using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopAgreementChangeRequestRepository : IRepository<ShopAgreementChangeRequest>
    {
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalPendingShopAgreementChangeRequestByShopIds(List<int?> shopIds);
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalRejectedShopAgreementChangeRequestByShopIds(List<int?> shopIds);
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalApprovedShopAgreementChangeRequestByShopIds(List<int?> shopIds);
        Task<ShopAgreementChangeRequest> GetAgreementChangeRequestByShopId(int shopAgreementChangeRequestShopId);
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAllByShopAgreementChangeRequestIds(List<int?> shopAgreementChangeRequestIds); //Aprove bulk
    }
}
