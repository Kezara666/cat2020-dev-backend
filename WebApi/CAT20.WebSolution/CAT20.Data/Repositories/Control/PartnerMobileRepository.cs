using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Control;

public class PartnerMobileRepository: Repository<PartnerMobile>, IPartnerMobileRepository
{
    public PartnerMobileRepository(DbContext context) : base(context)
    {
    }
    
    private ControlDbContext controlDbContext
    {
        get { return Context as ControlDbContext; }
    }
}