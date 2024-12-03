using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IPaymentVatRepository : IRepository<PaymentVat>
    {
        Task<IEnumerable<PaymentVat>> GetAllAsync();
        Task<PaymentVat> GetByIdAsync(int id);
    }
}
