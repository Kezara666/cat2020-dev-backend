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
    public class PaymentNbtRepository : Repository<PaymentNbt>, IPaymentNbtRepository
    {
        public PaymentNbtRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<PaymentNbt>> GetAllAsync()
        {
            return await controlDbContext.PaymentNbt
                .ToListAsync();
        }

        public async Task<PaymentNbt> GetByIdAsync(int id)
        {
            return await controlDbContext.PaymentNbt
                 .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}