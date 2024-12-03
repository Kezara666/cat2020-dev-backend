using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IPaymentNbtRepository : IRepository<PaymentNbt>
    {
        Task<IEnumerable<PaymentNbt>> GetAllAsync();
        Task<PaymentNbt> GetByIdAsync(int id);
    }
}
