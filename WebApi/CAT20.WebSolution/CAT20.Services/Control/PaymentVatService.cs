using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class PaymentVatService : IPaymentVatService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public PaymentVatService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PaymentVat>> GetAll()
        {
            return await _unitOfWork.PaymentVats.GetAllAsync();
        }
        public async Task<PaymentVat> GetById(int id)
        {
            return await _unitOfWork.PaymentVats.GetByIdAsync(id);
        }
    }
}