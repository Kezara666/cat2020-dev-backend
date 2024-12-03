using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAT20.Data.Repositories.Control
{
    public class GnDivisionsRepository : Repository<GnDivisions>, IGnDivisionsRepository
    {
        public GnDivisionsRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<GnDivisions>> GetAllAsync()
        {
            return await controlDbContext.GnDivisions
                .ToListAsync();
        }

        public async Task<IEnumerable<GnDivisions>> GetAllForOfficeAsync(int officeid)
        {
            return await controlDbContext.GnDivisions
                .Where(m=> m.OfficeId==officeid)
                .ToListAsync();
        }

        public async Task<GnDivisions> GetByIdAsync(int id)
        {
            return await controlDbContext.GnDivisions
                .Where(m=> m.Id==id)
                .FirstOrDefaultAsync();
        }

        //linking repository
        public async Task<IEnumerable<GnDivisions>> GetAllForListAsync(List<int> gnDivisionIdList)
        {
            return await controlDbContext.GnDivisions.Where(gnd => gnDivisionIdList.Contains(gnd.Id.Value)).ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        public async Task<IEnumerable<GnDivisions>> SearchAsync(int officeid, string description, string? code)
        {
            IQueryable<GnDivisions> query = controlDbContext.GnDivisions;

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(e => e.OfficeId==officeid && (e.Description.Contains(description)
                            || e.Code.Contains(code)));
            }

            if (code != null)
            {
                query = query.Where(e => e.Code == code && e.OfficeId == officeid);
            }

            return await query.ToListAsync();
            //return await controlDbContext.GnDivisions
            //    .Where(m => m.OfficeId == officeid)
            //    .ToListAsync();
        }
    }
}