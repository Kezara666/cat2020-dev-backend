using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Data;
using CAT20.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public class AmalgamationSubDivisionRepository : Repository<AmalgamationSubDivision>, IAmalgamationSubDivisionRepository
    {
        public AmalgamationSubDivisionRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";


            var result = assessmentTaxDbContext.AmalgamationSubDivision
                 .Include(m => m.Amalgamations)
                .Include(m => m.SubDivisions)
                  .Include(m => m.AmalgamationSubDivisionActions)
                  .Where(m => m.Status != 0 && m.SabhaId == token.sabhaId)
                 //.Where(a => EF.Functions.Like(a.Assessment.AssessmentNo!, "%" + filterKeyword + "%"))
                 .OrderBy(a => a.Id);




            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllPendingAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";


            var result = assessmentTaxDbContext.AmalgamationSubDivision
                .Include(m=>m.Amalgamations)
                .Include(m=>m.SubDivisions)
                .Include(m=>m.AmalgamationSubDivisionActions)
                  .Where(m => m.Status == 1 && m.SabhaId == token.sabhaId)
                 //.Where(a => EF.Functions.Like(a.Assessment.AssessmentNo!, "%" + filterKeyword + "%"))
                 .OrderByDescending(a => a.Id);




            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public Task<AmalgamationSubDivision> GetById(int id, HTokenClaim token)
        {
            return assessmentTaxDbContext.AmalgamationSubDivision
                .Include(m => m.Amalgamations)
                .Include(m => m.SubDivisions)
                .Include(m => m.AmalgamationSubDivisionActions)
                  .Where(m => m.Id == id  && m.SabhaId == token.sabhaId)
                 .FirstOrDefaultAsync();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}
