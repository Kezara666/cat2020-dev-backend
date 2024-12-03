using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopAgreementChangeRequestService
    {
   
        Task<ShopAgreementChangeRequest> GetById(int id);

        Task<ShopAgreementChangeRequest> Create(ShopAgreementChangeRequest obj);

        Task Update(ShopAgreementChangeRequest objToBeUpdated, ShopAgreementChangeRequest obj);

        Task RejectShopAgreementChangeRequest(ShopAgreementChangeRequest objToBeUpdated, ShopAgreementChangeRequest obj);

        Task<IEnumerable<ShopAgreementChangeRequest>> ApproveShopAgreementChangeRequest(List<int?> ShopAgreementChangeRequestIds, int approvedby);

        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalPendingShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds);
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalRejectedShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds);
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalApprovedShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds);

        Task<ShopAgreementChangeRequest> GetShopAgreementChangeRequestByShopIds(int shopKeyIds);
        //----
        Task<IEnumerable<ShopAgreementChangeRequest>> GetAllByShopAgreementChangeRequestIds(List<int?> ShopAgreementChangeRequestIds);
        //----
    }
}
