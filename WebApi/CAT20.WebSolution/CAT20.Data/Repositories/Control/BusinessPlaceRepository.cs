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
    public class BusinessPlaceRepository : Repository<BusinessPlace>, IBusinessPlaceRepository
    {
        public BusinessPlaceRepository(DbContext context) : base(context)
        {
        }
        public async Task<BusinessPlace> GetByIdAsync(int id)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m => m.Id == id && m.Status == 1)
                .FirstOrDefaultAsync();
        }


        public async Task<BusinessPlace> GetByAssessmentNoAsync(string AssessmentNO)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m => m.Status == 1  && m.AssessmentNo == AssessmentNO.Trim()).FirstOrDefaultAsync();
        }

        public async Task<BusinessPlace> GetByBusinessIdAsync(int BusinessId)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m => m.Status == 1 && m.BusinessId == BusinessId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BusinessPlace>> GetAllAsync(int Id)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m=> m.Status == 1)
                .ToListAsync();
        }
        
        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

        //public async Task<IEnumerable<BusinessPlace>> GetAllForBusinessPlaceSubNatureAndSabhaAsync(int SubNatureId, int sabhaId)
        //{
        //    return await controlDbContext.BusinessPlaces.Where(m =>  m.Status == 1 && m.SabhaId == sabhaId && m.BusinessPlaceSubNature == SubNatureId).ToListAsync();
        //}

        //public async Task<IEnumerable<BusinessPlace>> GetAllForBusinessPlaceNatureAndSabhaAsync(int NatureId, int sabhaId)
        //{
        //    return await controlDbContext.BusinessPlaces.Where(m => m.Status == 1 && m.SabhaId == sabhaId && m.BusinessPlaceNature == NatureId).ToListAsync();
        //}

        public async Task<IEnumerable<BusinessPlace>> GetAllForSabhaAsync(int sabhaId)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m => m.Status == 1 && m.SabhaId == sabhaId)
                .ToListAsync();
        }
            public async Task<IEnumerable<BusinessPlace>> GetAllForPropertyOwnerIdAsync(int PropertyOwnerId)
        {
            return await controlDbContext.BusinessPlaces
                .Where(m => m.Status == 1 && m.PropertyOwnerId == PropertyOwnerId)
                .ToListAsync();
        }
        
    }
}