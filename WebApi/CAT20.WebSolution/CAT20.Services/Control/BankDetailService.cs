using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class BankDetailService : IBankDetailService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public BankDetailService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BankDetail> CreateBankDetail(BankDetail newBankDetail)
        {
            await _unitOfWork.BankDetails
                .AddAsync(newBankDetail);
            await _unitOfWork.CommitAsync();

            return newBankDetail;
        }
        public async Task DeleteBankDetail(BankDetail bankDetail)
        {
            _unitOfWork.BankDetails.Remove(bankDetail);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BankDetail>> GetAllBankDetails()
        {
            return await _unitOfWork.BankDetails.GetAllAsync();
        }
        public async Task<BankDetail> GetBankDetailById(int id)
        {
            return await _unitOfWork.BankDetails.GetByIdAsync(id);
        }
        public async Task UpdateBankDetail(BankDetail bankDetailToBeUpdated, BankDetail bankDetail)
        {
            //bankDetailToBeUpdated.Name = bankDetail.t;

            await _unitOfWork.CommitAsync();
        }
    }
}