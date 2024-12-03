using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Project>> GetAllWithProjectAsync()
        {
            return await voteAccDbContext.Projects
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<Project> GetWithProjectByIdAsync(int id)
        {
            return await voteAccDbContext.Projects
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Project>> GetAllWithProjectByProjectIdAsync(int Id)
        {
            return await voteAccDbContext.Projects
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdAsync(int Id)
        {
            return await voteAccDbContext.Projects.Where(m => m.ProgrammeID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdandSabhaIdAsync(int programmeId, int sabhaId)
        {
            return await voteAccDbContext.Projects.Where(m => m.ProgrammeID == programmeId && m.SabhaID == sabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.Projects.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}