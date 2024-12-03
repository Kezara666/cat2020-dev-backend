using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Control
{
    public class OfficeTypeRepository : Repository<OfficeType>, IOfficeTypeRepository
    {
        public OfficeTypeRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<OfficeType>> GetAllWithOfficeTypeAsync()
        {
            return await controlDbContext.OfficeTypes
                .ToListAsync();
        }

        public async Task<OfficeType> GetWithOfficeTypeByIdAsync(int id)
        {
            return await controlDbContext.OfficeTypes
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OfficeType>> GetAllWithOfficeTypeByOfficeTypeIdAsync(int officeTypeId)
        {
            return await controlDbContext.OfficeTypes
                .Where(m => m.ID == officeTypeId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}