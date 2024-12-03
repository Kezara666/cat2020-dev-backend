using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class PaymentVatRepository : Repository<PaymentVat>, IPaymentVatRepository 
    {
        public PaymentVatRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<PaymentVat>> GetAllAsync()
        {
            return await controlDbContext.PaymentVat
                .ToListAsync();
        }

        public async Task<PaymentVat> GetByIdAsync(int id)
        {
            return await controlDbContext.PaymentVat
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}