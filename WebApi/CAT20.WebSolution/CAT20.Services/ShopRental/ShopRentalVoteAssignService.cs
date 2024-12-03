using CAT20.Core;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.ShopRental
{
    public class ShopRentalVoteAssignService : IShopRentalVoteAssignService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;

        public ShopRentalVoteAssignService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<ShopRentalVoteAssign> Create(ShopRentalVoteAssign newVoteAssign)
        {
            try
            {
                await _unitOfWork.ShopRentalVoteAssign.AddAsync(newVoteAssign);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newVoteAssign;
        }

        public async Task<IEnumerable<ShopRentalVoteAssign>> GetAllVoteAssigns()
        {
            return await _unitOfWork.ShopRentalVoteAssign.GetAllAsync();
        }

        public async Task<ShopRentalVoteAssign> GetById(int id)
        {
            return await _unitOfWork.ShopRentalVoteAssign.GetByIdAsync(id);
        }

        public async Task Update(ShopRentalVoteAssign objToBeUpdated, ShopRentalVoteAssign obj)
        {
            //---------------- [Start - vote assignment detail id] -------------------------
            objToBeUpdated.PropertyRentalVoteId             = obj.PropertyRentalVoteId;
            objToBeUpdated.LastYearArreasAmountVoteId       = obj.LastYearArreasAmountVoteId;
            objToBeUpdated.ThisYearArrearsAmountVoteId      = obj.ThisYearArrearsAmountVoteId;
            objToBeUpdated.LastYearFineAmountVoteId         = obj.LastYearFineAmountVoteId;
            objToBeUpdated.ThisYearFineAmountVoteId         = obj.ThisYearFineAmountVoteId;
            objToBeUpdated.ServiceChargeArreasAmountVoteId  = obj.ServiceChargeArreasAmountVoteId;
            objToBeUpdated.ServiceChargeAmountVoteId        = obj.ServiceChargeAmountVoteId;
            objToBeUpdated.OverPaymentAmountVoteId          = obj.OverPaymentAmountVoteId;
            //---------------- [End - vote assignment detail id] --------------------------


            //--------- [Start - vote detail id fields] ------------
            objToBeUpdated.PropertyRentalVoteDetailId = obj.PropertyRentalVoteDetailId;
            objToBeUpdated.LastYearArreasAmountVoteDetailId = obj.LastYearArreasAmountVoteDetailId;
            objToBeUpdated.ThisYearArrearsAmountVoteDetailId = obj.ThisYearArrearsAmountVoteDetailId;
            objToBeUpdated.LastYearFineAmountVoteDetailId = obj.LastYearFineAmountVoteDetailId;
            objToBeUpdated.ThisYearFineAmountVoteDetailId = obj.ThisYearFineAmountVoteDetailId;
            objToBeUpdated.ServiceChargeArreasAmountVoteDetailId = obj.ServiceChargeArreasAmountVoteDetailId;
            objToBeUpdated.ServiceChargeAmountVoteDetailId = obj.ServiceChargeAmountVoteDetailId;
            objToBeUpdated.OverPaymentAmountVoteDetailId = obj.OverPaymentAmountVoteDetailId;
            //--------- [End - vote detail id fields] --------------

            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ShopRentalVoteAssign>> CreateMultiple(List<ShopRentalVoteAssign> objs)
        {
            try
            {
                await _unitOfWork.ShopRentalVoteAssign.AddRangeAsync(objs);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                return new List<ShopRentalVoteAssign>();
            }
            return objs;
        }

        //custom query -- should be writteen in the repository
        public async Task<IEnumerable<ShopRentalVoteAssign>> GetAllForOffice(int officeid)
        {
            try
            {
                return await _unitOfWork.ShopRentalVoteAssign.GetAllForOffice(officeid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopRentalVoteAssign>> GetAllForSabha(int sabhaid)
        {
            try
            {
                return await _unitOfWork.ShopRentalVoteAssign.GetAllForSabha(sabhaid);
            }
            catch (Exception ex)
            {
                return null;
            }  
        }

        //public async Task<ShopRentalVoteAssign> GetByPropertyId(int propertyId)
        //{
        //    return await _unitOfWork.ShopRentalVoteAssign.GetByPropertyIdAsync(propertyId);
        //}

        public async Task<ShopRentalVoteAssign> GetByShopId(int shopId)
        {
            return await _unitOfWork.ShopRentalVoteAssign.GetByShopId(shopId);
        }

        //----
        public async Task Delete(ShopRentalVoteAssign obj)
        {
            _unitOfWork.ShopRentalVoteAssign.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
        //----
    }
}
