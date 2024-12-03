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
    public class DocumentSequenceNumberRepository : Repository<DocumentSequenceNumber>, IDocumentSequenceNumberRepository
    {
        public DocumentSequenceNumberRepository(DbContext context) : base(context)
        {
        }
        public async Task<DocumentSequenceNumber> GetNextSequenceNumberForYearOfficePrefixAsync(int year, int? officeid, string prefix)
        {
            return await controlDbContext.DocumentSequenceNumber
                .Where(m => m.Year == year && m.OfficeId==officeid && m.Prefix==prefix)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<DocumentSequenceNumber>> GetAllForOfficeAsync(int officeid)
        {
            return await controlDbContext.DocumentSequenceNumber
                .Where(m=> m.OfficeId == officeid)
                .ToListAsync();
        }

 

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}