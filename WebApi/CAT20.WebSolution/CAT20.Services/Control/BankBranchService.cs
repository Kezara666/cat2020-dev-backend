using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CAT20.Services.Control
{
    public class BankBranchService : IBankBranchService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public BankBranchService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BankBranch> SaveBankBranch(BankBranch newBankBranch)
        {
            await _unitOfWork.BankBranches
                .AddAsync(newBankBranch);
            await _unitOfWork.CommitAsync();

            return newBankBranch;
        }
        public async Task DeleteBankBranch(BankBranch BankBranch)
        {
            _unitOfWork.BankBranches.Remove(BankBranch);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BankBranch>> GetAllBankBranches()
        {
            return await _unitOfWork.BankBranches.GetAllBankBranches();
        }
        public async Task<BankBranch> GetBankBranchById(int id)
        {
            return await _unitOfWork.BankBranches.GetBankBranchById(id);
        }
        public async Task <BankBranch> GetBankBranchByBankCodeAndBranchCode(int bankcode, int branchcode)
        {
            return await _unitOfWork.BankBranches.GetBankBranchByBankCodeAndBranchCode(bankcode, branchcode);
        }
        public async Task<IEnumerable<BankBranch>> GetAllBankBranchesForBankCode(int bankcode)
        {
            return await _unitOfWork.BankBranches.GetAllBankBranchesForBankCode(bankcode);
        }

        public Task<BankBranch> GetBankBranchWithBankById(int branchId)
        {
           return _unitOfWork.BankBranches.GetBankBranchWithBankById(branchId);
        }
    }
}