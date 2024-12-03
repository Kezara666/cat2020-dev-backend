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
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Gender>> GetAllWithGenderAsync()
        {
            return await controlDbContext.Genders
                .ToListAsync();
        }

        public async Task<Gender> GetWithGenderByIdAsync(int id)
        {
            return await controlDbContext.Genders
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Gender>> GetAllWithGenderByGenderIdAsync(int genderId)
        {
            return await controlDbContext.Genders
                .Where(m => m.ID == genderId)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}