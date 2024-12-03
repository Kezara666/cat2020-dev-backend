using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment;

public class PaymentGatewayRepository:Repository<PaymentGateway>, IPaymentGatewayRepository
{
    public PaymentGatewayRepository(DbContext context) : base(context)
    {
    }
    
    private OnlinePaymentDbContext onlinePaymentDbContext
    {
        get { return Context as OnlinePaymentDbContext; }
    }

    public async Task<PaymentGateway> GetBySabhaId(int? id)
    {
        return await onlinePaymentDbContext.PaymentGateway
            .Where(p => p.SabhaId == id && p.Status == 1)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PaymentGateway>> getAll()
    {
        return await onlinePaymentDbContext.PaymentGateway.ToListAsync();
    }
    public async Task<IEnumerable<PaymentGateway>> getAllByProvinceId(int provinceId)
    {
        return await onlinePaymentDbContext.PaymentGateway.Where(p=>p.ProvinceId == provinceId && p.Status==1).ToListAsync();
    }
}