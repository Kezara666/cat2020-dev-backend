using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment;

public class PaymentDetailBackUpRepository:Repository<PaymentDetailBackUp>, IPaymentDetailBackUpRepository
{
    public PaymentDetailBackUpRepository(DbContext context) : base(context)
    {
    }
    
    private OnlinePaymentDbContext onlinePaymentDbContext
    {
        get { return Context as OnlinePaymentDbContext; }
    }
}