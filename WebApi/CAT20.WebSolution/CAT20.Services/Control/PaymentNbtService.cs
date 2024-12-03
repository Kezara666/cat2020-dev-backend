using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class PaymentNbtService : IPaymentNbtService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public PaymentNbtService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PaymentNbt>> GetAll()
        {
            return await _unitOfWork.PaymentNbts.GetAllAsync();
        }
        public async Task<PaymentNbt> GetById(int id)
        {
            return await _unitOfWork.PaymentNbts.GetByIdAsync(id);
        }
    }
}