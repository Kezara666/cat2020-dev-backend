using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment;

public class LogInRepository:Repository<LogInDetails>, ILogInRepository
{
    public LogInRepository(DbContext context) : base(context)
    {
    }
    
    private OnlinePaymentDbContext onlinePaymentDbContext
    {
        get { return Context as OnlinePaymentDbContext; }
    }
}