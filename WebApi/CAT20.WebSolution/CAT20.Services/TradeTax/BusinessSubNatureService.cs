using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Services.TradeTax;

namespace CAT20.Services.TradeTax
{
    public class BusinessSubNatureService : IBusinessSubNatureService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public BusinessSubNatureService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BusinessSubNature> CreateBusinessSubNature(BusinessSubNature newBusinessSubNature)
        {
            try { 
            await _unitOfWork.BusinessSubNatures
                .AddAsync(newBusinessSubNature);
            await _unitOfWork.CommitAsync();

            return newBusinessSubNature;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task DeleteBusinessSubNature(BusinessSubNature businessSubNature)
        {
            //businessSubNature.ActiveStatus = 0;
            //await _unitOfWork.CommitAsync();
            _unitOfWork.BusinessSubNatures.Remove(businessSubNature);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNatures()
        {
            return await _unitOfWork.BusinessSubNatures.GetAllAsync();
        }
        public async Task<BusinessSubNature> GetBusinessSubNatureById(int id)
        {
            return await _unitOfWork.BusinessSubNatures.GetByIdAsync(id);
        }
        public async Task UpdateBusinessSubNature(BusinessSubNature businessSubNatureToBeUpdated, BusinessSubNature businessSubNature)
        {
            businessSubNatureToBeUpdated.BusinessNatureID = businessSubNature.BusinessNatureID;
            businessSubNatureToBeUpdated.BusinessSubNatureName = businessSubNature.BusinessSubNatureName;
            businessSubNatureToBeUpdated.OtherCharges = businessSubNature.OtherCharges;
            businessSubNatureToBeUpdated.TaxAmount = businessSubNature.TaxAmount;
            businessSubNatureToBeUpdated.BusinessSubNatureStatus = businessSubNature.BusinessSubNatureStatus;
            businessSubNatureToBeUpdated.UpdatedBy = businessSubNature.UpdatedBy;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessSubNatureId(int Id)
        {
            return await _unitOfWork.BusinessSubNatures.GetAllWithBusinessSubNatureByBusinessSubNatureIdAsync(Id);
        }
        public async Task<IEnumerable<BusinessSubNature>> GetAllWithBusinessSubNatureByBusinessNatureIdandSabhaId(int BusinessSubNatureId, int SabhaId)
        {
            return await _unitOfWork.BusinessSubNatures.GetAllWithBusinessSubNatureByBusinessNatureIdandSabhaIdAsync(BusinessSubNatureId, SabhaId);
        }

        public async Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForSabhaId(int SabhaId)
        {
            return await _unitOfWork.BusinessSubNatures.GetAllBusinessSubNaturesForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<BusinessSubNature>> GetAllBusinessSubNaturesForBusinessNatureID(int NatureID)
        {
            try
            {
                return await _unitOfWork.BusinessSubNatures.GetAllBusinessSubNaturesForBusinessNatureIDAsync(NatureID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<BusinessSubNature> bnature = new List<BusinessSubNature>();
                return bnature;
            }
        }
    }
}