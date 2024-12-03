using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class SubProjectRepository : Repository<SubProject>, ISubProjectRepository
    {
        public SubProjectRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectAsync()
        {
            return await voteAccDbContext.SubProjects
                .Include(x => x.project)
                .Where(m => m.Status == 1 && m.ProjectID == m.project.ID)
                .ToListAsync();
        }

        public async Task<SubProject> GetWithSubProjectByIdAsync(int id)
        {
            return await voteAccDbContext.SubProjects
                .Where(m => m.Status == 1)
                .SingleAsync(m => m.ID == id); ;
        }

        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectBySubProjectIdAsync(int Id)
        {
            return await voteAccDbContext.SubProjects
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdAsync(int Id)
        {
            return await voteAccDbContext.SubProjects.Where(m => m.ProjectID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdandSabhaIdAsync(int ProjectId, int SabhaId)
        {
            return await voteAccDbContext.SubProjects.Where(m => m.ProjectID == ProjectId && m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubProject>> GetAllSubProjectsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.SubProjects.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<SubProject>> GetAllSubProjectsForProgrammeIdAsync(int Id)
        {
            return await voteAccDbContext.SubProjects.Where(m => m.ProgrammeID == Id && m.Status == 1).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}