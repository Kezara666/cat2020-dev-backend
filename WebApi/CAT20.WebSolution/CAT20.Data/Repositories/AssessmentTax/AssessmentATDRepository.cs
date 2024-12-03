using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentATDRepository : Repository<AssessmentATD>, IAssessmentATDRepository
    {
        public AssessmentATDRepository(DbContext context) : base(context)
        {
        }

        public async Task<AssessmentATD> GetATDById(int Id)
        {
            try
            {

                var query = await assessmentTaxDbContext.AssessmentATDs
                    .Include(j => j.Assessment)
                    .Include(j => j.AssessmentATDOwnerslogs)
                    .Where(a => a.Status == 1 && a.Id == Id)
                    .OrderByDescending(a => a.Id).FirstOrDefaultAsync();

                    return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForAssessment(int assessmentId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentATDs
                    .Include(j => j.Assessment)
                    .Include(j => j.AssessmentATDOwnerslogs)
                    .Where(a => a.Status==1 && a.AssessmentId == assessmentId)
                    .OrderByDescending(a => a.Id); ;

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                if (pageNo != 0)
                {
                    var list = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
                    return (totalCount: totalCount, list: list);
                }
                else
                {
                    return (totalCount: totalCount, list: query);
                }
            }
            catch (Exception ex)
            {
                return (totalCount: 0, list: null);
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForOffice(int? officeId, ATDRequestStatus atdStatus, int pageNo)
        {
            try
            {
                var query = assessmentTaxDbContext.AssessmentATDs
                    .Include(j => j.Assessment)
                    .Include(j => j.AssessmentATDOwnerslogs)
                    .Where(a => a.Status==1 && a.Assessment!.OfficeId == officeId && a.ATDRequestStatus==atdStatus)
                    .OrderByDescending(a => a.Id); ;

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                if (pageNo != 0)
                {
                    var list = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
                    return (totalCount: totalCount, list: list);
                }
                else
                {
                    return (totalCount: totalCount, list: query);
                }
            }
            catch (Exception ex)
            {
                return (totalCount: 0, list: null);
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForSabha(int? sabhaId, ATDRequestStatus atdStatus, int pageNo)
        {
            try
            {
                var query = assessmentTaxDbContext.AssessmentATDs
                    .Include(j => j.Assessment)
                    .Include(j => j.AssessmentATDOwnerslogs)
                    .Where(a => a.Status == 1 && a.Assessment!.SabhaId == sabhaId && a.ATDRequestStatus == atdStatus)
                    .OrderByDescending(a => a.Id); ;

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;
                if (pageNo != 0)
                {
                    var list = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
                    return (totalCount: totalCount, list: list);
                }
                else
                {
                    return (totalCount: totalCount, list: query);
                }
            }
            catch (Exception ex)
            {
                return (totalCount: 0, list: null);
            }
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}
