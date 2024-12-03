using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Services.TradeTax;
using CAT20.Core.Models.Mixin;

namespace CAT20.Services.TradeTax
{
    public class BusinessNatureService : IBusinessNatureService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public BusinessNatureService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BusinessNature> CreateBusinessNature(BusinessNature newBusinessNature)
        {
            try
            {
                await _unitOfWork.BusinessNatures
               .AddAsync(newBusinessNature);
                await _unitOfWork.CommitAsync();
            }
           catch(Exception ex) {
                return null;
            }
            return newBusinessNature;

        }
        public async Task DeleteBusinessNature(BusinessNature businessNature)
        {
            //businessNature.ActiveStatus = 0;
            //await _unitOfWork.CommitAsync();
            _unitOfWork.BusinessNatures.Remove(businessNature);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BusinessNature>> GetAllBusinessNatures()
        {
            return await _unitOfWork.BusinessNatures.GetAllAsync();
        }
        public async Task<BusinessNature> GetBusinessNatureById(int id)
        {
            return await _unitOfWork.BusinessNatures.GetByIdAsync(id);
        }
        public async Task UpdateBusinessNature(BusinessNature businessNatureToBeUpdated, BusinessNature businessNature)
        {
            businessNatureToBeUpdated.BusinessNatureName = businessNature.BusinessNatureName;
            businessNatureToBeUpdated.UpdatedBy = businessNature.UpdatedBy;
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<BusinessNature>> GetAllWithBusinessNatureByBusinessNatureId(int Id)
        {
            return await _unitOfWork.BusinessNatures.GetAllWithBusinessNatureByBusinessNatureIdAsync(Id);
        }

        public async Task<IEnumerable<BusinessNature>> GetAllBusinessNatureBySabhaId(int Id)
        {
            return await _unitOfWork.BusinessNatures.GetAllWithBusinessNatureBySabhaIdAsync(Id);
        }
    }
}