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
    public class ShopRentalRecievableIncomeVoteAssignService : IShopRentalReceivableIncomeVoteAssignService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;

        public ShopRentalRecievableIncomeVoteAssignService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<ShopRentalRecievableIncomeVoteAssign> Create(ShopRentalRecievableIncomeVoteAssign newVoteAssign)
        {
            try
            {
                await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.AddAsync(newVoteAssign);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newVoteAssign;
        }


        public async Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> CreateMultiple(List<ShopRentalRecievableIncomeVoteAssign> objs)
        {
            try
            {
                await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.AddRangeAsync(objs);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                return new List<ShopRentalRecievableIncomeVoteAssign>();
            }
            return objs;
        }

        public async Task Delete(ShopRentalRecievableIncomeVoteAssign obj)
        {

            _unitOfWork.ShopRentalReceivableIncomeVoteAssign.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForOffice(int officeid)
        {
            try
            {
                return await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.GetAllForOffice(officeid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForSabha(int sabhaid)
        {
            try
            {
                return await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.GetAllForSabha(sabhaid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ShopRentalRecievableIncomeVoteAssign> GetById(int id)
        {
            return await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.GetByIdAsync(id);
        }

        public async Task<ShopRentalRecievableIncomeVoteAssign> GetByShopId(int shopId)
        {
            return await _unitOfWork.ShopRentalReceivableIncomeVoteAssign.GetByShopId(shopId);
        }

        public async Task Update(ShopRentalRecievableIncomeVoteAssign objToBeUpdated, ShopRentalRecievableIncomeVoteAssign obj)
        {
            //---------------- [Start - vote detail id] -------------------------
            objToBeUpdated.PropertyRentalIncomeVoteId           = obj.PropertyRentalIncomeVoteId;
            objToBeUpdated.PropertyServiceChargeIncomeVoteId    = obj.PropertyServiceChargeIncomeVoteId;
            objToBeUpdated.PropertyFineIncomeVoteId             = obj.PropertyFineIncomeVoteId;
            //---------------- [End - vote detail id] -------------------------

            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();
        }
    }
}
