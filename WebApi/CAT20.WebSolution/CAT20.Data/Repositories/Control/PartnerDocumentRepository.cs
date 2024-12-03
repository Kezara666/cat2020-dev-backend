using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Control;

public class PartnerDocumentRepository : Repository<PartnerDocument>, IPartnerDocumentRepository
{
    public PartnerDocumentRepository(DbContext context) : base(context)
    {
    }

    private ControlDbContext controlDbContext
    {
        get { return Context as ControlDbContext; }
    }
}