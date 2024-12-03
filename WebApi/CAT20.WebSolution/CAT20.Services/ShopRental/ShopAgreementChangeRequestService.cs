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
    public class ShopAgreementChangeRequestService : IShopAgreementChangeRequestService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;

        public ShopAgreementChangeRequestService(IShopRentalUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public async Task<ShopAgreementChangeRequest> GetById(int id)
        {
           return await _unitOfWork.ShopAgreementChangeRequest.GetByIdAsync(id);
        }

        public async Task<ShopAgreementChangeRequest> Create(ShopAgreementChangeRequest newObj)
        {
            try
            {
                var shopAgreementChangeRequest = await _unitOfWork.ShopAgreementChangeRequest.GetAgreementChangeRequestByShopId(newObj.ShopId);

                if ( shopAgreementChangeRequest !=null && shopAgreementChangeRequest.Id != null )
                {
                    // Update existing record
                    shopAgreementChangeRequest.AgreementExtendEndDate = newObj.AgreementExtendEndDate;
                    shopAgreementChangeRequest.AgreementCloseDate = newObj.AgreementCloseDate;
                    shopAgreementChangeRequest.Requestedstatus = newObj.Requestedstatus;
                    shopAgreementChangeRequest.AgreementChangeReason = newObj.AgreementChangeReason;
                    shopAgreementChangeRequest.ApproveStatus = 0;
                    shopAgreementChangeRequest.UpdatedBy = newObj.UpdatedBy;
                    shopAgreementChangeRequest.UpdatedAt = DateTime.Now;

                    // Return the updated object, not the input object
                    await _unitOfWork.CommitAsync();
                    return shopAgreementChangeRequest;
                }
                else
                {
                    // Add new record
                    await _unitOfWork.ShopAgreementChangeRequest.AddAsync(newObj);
                    await _unitOfWork.CommitAsync();
                    return newObj;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalApprovedShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopAgreementChangeRequest.GetAprovalApprovedShopAgreementChangeRequestByShopIds(shopKeyIds);
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalPendingShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopAgreementChangeRequest.GetAprovalPendingShopAgreementChangeRequestByShopIds(shopKeyIds);
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAprovalRejectedShopAgreementChangeRequestByShopIds(List<int?> shopKeyIds)
        {
            return await _unitOfWork.ShopAgreementChangeRequest.GetAprovalRejectedShopAgreementChangeRequestByShopIds(shopKeyIds);
        }

        public async Task<ShopAgreementChangeRequest> GetShopAgreementChangeRequestByShopIds(int shopKeyIds)
        {
            return await _unitOfWork.ShopAgreementChangeRequest.GetAgreementChangeRequestByShopId(shopKeyIds);
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> ApproveShopAgreementChangeRequest(List<int?> ShopAgreementChangeRequestIds, int approvedby)
        {
            var approvedOBL = await _unitOfWork.ShopAgreementChangeRequest.GetAllByShopAgreementChangeRequestIds(ShopAgreementChangeRequestIds);

            try
            {
                foreach (var obl in approvedOBL)
                {
                    obl.ApprovedBy = approvedby;
                    obl.ApprovedAt = DateTime.Now;
                    obl.ApproveStatus = 1; // 1-approved
                }

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                return approvedOBL.DefaultIfEmpty();
            }

            return approvedOBL;
        }
       
        public async Task RejectShopAgreementChangeRequest(ShopAgreementChangeRequest objToBeUpdated, ShopAgreementChangeRequest obj)
        {
            objToBeUpdated.ApprovedBy = obj.ApprovedBy;
            objToBeUpdated.ApprovedAt = DateTime.Now;
            objToBeUpdated.ApproveComment = obj.ApproveComment;
            objToBeUpdated.ApproveStatus = 2; // 2-rejected

            await _unitOfWork.CommitAsync(); 
        }

        public async Task Update(ShopAgreementChangeRequest objToBeUpdated, ShopAgreementChangeRequest obj)
        {
            objToBeUpdated.Requestedstatus      = obj.Requestedstatus;
            objToBeUpdated.AgreementCloseDate   = obj.AgreementCloseDate;
            objToBeUpdated.UpdatedBy            = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt            = DateTime.Now;
            objToBeUpdated.ApproveStatus        = 0; // 0-pending

            objToBeUpdated.AgreementExtendEndDate = obj.AgreementExtendEndDate;
            objToBeUpdated.RequestType = obj.RequestType;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ShopAgreementChangeRequest>> GetAllByShopAgreementChangeRequestIds(List<int?> ShopAgreementChangeRequestIds)
        {
            return await _unitOfWork.ShopAgreementChangeRequest.GetAllByShopAgreementChangeRequestIds(ShopAgreementChangeRequestIds);
        }
    }
}
