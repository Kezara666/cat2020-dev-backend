using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class AccountDetailService : IAccountDetailService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public AccountDetailService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AccountDetail> CreateAccountDetail(AccountDetail newAccountDetail)
        {
            await _unitOfWork.AccountDetails
                .AddAsync(newAccountDetail);
            await _unitOfWork.CommitAsync();

            return newAccountDetail;
        }
        public async Task DeleteAccountDetail(AccountDetail accountDetail)
        {
            accountDetail.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<AccountDetail>> GetAllAccountDetails()
        {
            return await _unitOfWork.AccountDetails.GetAllAsync();
        }
        public async Task<AccountDetail> GetAccountDetailById(int id)
        {
            return await _unitOfWork.AccountDetails.GetByIdAsync(id);
        }
        public async Task UpdateAccountDetail(AccountDetail accountDetailToBeUpdated, AccountDetail accountDetail)
        {
            //accountDetailToBeUpdated.Name = accountDetail.t;
            accountDetailToBeUpdated.AccountNo = accountDetail.AccountNo;
            accountDetailToBeUpdated.NameEnglish = accountDetail.NameEnglish;
            accountDetailToBeUpdated.NameSinhala = accountDetail.NameSinhala;
            accountDetailToBeUpdated.NameTamil = accountDetail.NameTamil;
            accountDetailToBeUpdated.BankCode = accountDetail.BankCode;
            accountDetailToBeUpdated.BranchCode = accountDetail.BranchCode;
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankId(int Id)
        {
            return await _unitOfWork.AccountDetails.GetAllWithAccountDetailByBankIdAsync(Id);
        }

        public async Task<IEnumerable<AccountDetail>> GetAllAccountDetailByOfficeId(int OfficeId)
        {
            try { 
            return await _unitOfWork.AccountDetails.GetAllWithAccountDetailByOfficeIdAsync(OfficeId);
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdandOfficeId(int BankId, int OfficeId)
        {
            return await _unitOfWork.AccountDetails.GetAllWithAccountDetailByBankIdandOfficeIdAsync(BankId, OfficeId);
        }
    }
}