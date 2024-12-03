using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Services.TradeTax;

namespace CAT20.Services.TradeTax
{
    public class TaxValueService : ITaxValueService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public TaxValueService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TaxValue> CreateTaxValue(TaxValue newTaxValue)
        {
            await _unitOfWork.TaxValues
                .AddAsync(newTaxValue);
            await _unitOfWork.CommitAsync();

            return newTaxValue;
        }
        public async Task DeleteTaxValue(TaxValue taxValue)
        {
            //taxValue.ActiveStatus = 0;
            //await _unitOfWork.CommitAsync();
            _unitOfWork.TaxValues.Remove(taxValue);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllTaxValues()
        {
            return await _unitOfWork.TaxValues.GetAllAsync();
        }
        public async Task<TaxValue> GetTaxValueById(int id)
        {
            return await _unitOfWork.TaxValues.GetByIdAsync(id);
        }
        public async Task UpdateTaxValue(TaxValue taxValueToBeUpdated, TaxValue taxValue)
        {
            taxValueToBeUpdated.TaxTypeID = taxValue.TaxTypeID;
            taxValueToBeUpdated.AnnualValueMinimum = taxValue.AnnualValueMinimum;
            taxValueToBeUpdated.AnnualValueMaximum = taxValue.AnnualValueMaximum;
            taxValueToBeUpdated.TaxAmount = taxValue.TaxAmount;
            taxValueToBeUpdated.TaxValueStatus = taxValue.TaxValueStatus;
            taxValueToBeUpdated.UpdatedBy = taxValue.UpdatedBy;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxValueId(int Id)
        {
            return await _unitOfWork.TaxValues.GetAllWithTaxValueByTaxValueIdAsync(Id);
        }
        public async Task<IEnumerable<TaxValue>> GetAllWithTaxValueByTaxTypeIdandSabhaId(int TaxValueId, int SabhaId)
        {
            return await _unitOfWork.TaxValues.GetAllWithTaxValueByTaxTypeIdandSabhaIdAsync(TaxValueId, SabhaId);
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaId(int SabhaId)
        {
            return await _unitOfWork.TaxValues.GetAllTaxValuesForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeID(int TaxTypeID)
        {
            try
            {
                return await _unitOfWork.TaxValues.GetAllTaxValuesForTaxTypeIDAsync(TaxTypeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<TaxValue> bnature = new List<TaxValue>();
                return bnature;
            }
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForTaxTypeIDAndSabhaID(int TaxTypeId, int sabhaid)
        {
            try
            {
                return await _unitOfWork.TaxValues.GetAllTaxValuesForTaxTypeIDAndSabhaIDAsync(TaxTypeId, sabhaid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<TaxValue> bnature = new List<TaxValue>();
                return bnature;
            }
        }

        public async Task<IEnumerable<TaxValue>> GetAllTaxValuesForSabhaID(int sabhaid)
        {
            try
            {
                return await _unitOfWork.TaxValues.GetAllTaxValuesForSabhaIDAsync(sabhaid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<TaxValue> bnature = new List<TaxValue>();
                return bnature;
            }
        }

    }
}