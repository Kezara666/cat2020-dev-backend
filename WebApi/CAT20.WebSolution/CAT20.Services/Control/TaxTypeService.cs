using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class TaxTypeService : ITaxTypeService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public TaxTypeService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TaxType> CreateTaxType(TaxType newTaxType)
        {
            await _unitOfWork.TaxTypes
                .AddAsync(newTaxType);
            await _unitOfWork.CommitAsync();

            return newTaxType;
        }
        public async Task DeleteTaxType(TaxType taxtype)
        {
            _unitOfWork.TaxTypes.Remove(taxtype);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<TaxType>> GetAllTaxTypes()
        {
            return await _unitOfWork.TaxTypes.GetAllAsync();
        }
        public async Task<IEnumerable<TaxType>> GetAllBasicTaxTypes()
        {
            return await _unitOfWork.TaxTypes.GetAllBasicTaxTypesAsync();
        }
        public async Task<TaxType> GetTaxTypeById(int id)
        {
            return await _unitOfWork.TaxTypes.GetByIdAsync(id);
        }
        public async Task UpdateTaxType(TaxType taxtypeToBeUpdated, TaxType taxtype)
        {
            //taxtypeToBeUpdated.Name = taxtype.t;

            await _unitOfWork.CommitAsync();
        }
    }
}