using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment;

public class DisputeRepository: Repository<Dispute> , IDisputeRepository
{
    public DisputeRepository(DbContext context) : base(context)
    {
    }
    
    private OnlinePaymentDbContext onlinePaymentDbContext
    {
        get { return Context as OnlinePaymentDbContext; }
    }
}