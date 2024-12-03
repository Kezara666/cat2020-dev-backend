using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IPaymentVatService
    {
        Task<IEnumerable<PaymentVat>> GetAll();
        Task<PaymentVat> GetById(int id);
    }
}

