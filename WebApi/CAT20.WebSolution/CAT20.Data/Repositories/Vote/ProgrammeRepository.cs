using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class ProgrammeRepository : Repository<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Programme>> GetAllWithProgrammeAsync()
        {
            return await voteAccDbContext.Programmes
                .Where(m=> m.Status == 1)
                .ToListAsync();
        }

        public async Task<Programme> GetWithProgrammeByIdAsync(int id)
        {
            return await voteAccDbContext.Programmes
                .Where(m => m.ID == id &&  m.Status == 1)
                .SingleAsync(); 
        }

        public async Task<IEnumerable<Programme>> GetAllWithProgrammeByProgrammeIdAsync(int Id)
        {
            return await voteAccDbContext.Programmes
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        public async Task<IEnumerable<Programme>> GetAllProgrammesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.Programmes.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }
    }
}