using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(DbContext context) : base(context)
        {
        }

        public async Task<Assessment?> GetById(int? id)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(m => m.Street).ThenInclude(st => st.Ward)
                .Include(m => m.AssessmentPropertyType)
                .Include(m => m.Description)
                .Include(m => m.AssessmentTempPartner)
                .Include(m => m.AssessmentTempSubPartner)
                .Include(m => m.AssessmentBalance)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Include(m => m.Allocation)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Assessment> GetForAmalgamationOrSubdivision(int? id,HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               //.Include(a => a.AssessmentBalance.Q1)
               //.Include(a => a.AssessmentBalance.Q2)
               //.Include(a => a.AssessmentBalance.Q3)
               //.Include(a => a.AssessmentBalance.Q4)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .FirstOrDefaultAsync(a => a.Id == id && a.SabhaId == token.sabhaId);
        }

        public async Task<Assessment> GetForFirstForInit(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a=>a.AssessmentBalance)
                .FirstOrDefaultAsync(a => a.SabhaId == sabhaId && a.AssessmentStatus == AssessmentStatus.Active);
        }

        public async Task<Assessment> GetForJournal(int? id)
        {
            return await assessmentTaxDbContext.Assessments
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Assessment> GetForAssetschnage(int? id)
        {
            return await assessmentTaxDbContext.Assessments
              //.Include(m => m.Street).ThenInclude(st => st.Ward)
              //.Include(m => m.AssessmentPropertyType)
              //.Include(m => m.Description)
              .Include(m => m.AssessmentTempPartner)
              .Include(m => m.AssessmentTempSubPartner)
              //.Include(m => m.AssessmentBalance)
              //.Include(a => a.AssessmentBalance.Q1)
              //.Include(a => a.AssessmentBalance.Q2)
              //.Include(a => a.AssessmentBalance.Q3)
              //.Include(a => a.AssessmentBalance.Q4)
              //.Include(m => m.Allocation)
              .Where(a => a.Id == id)
              .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Assessment>> GetForPendingJournalRequest(int? sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
               .AsNoTracking()
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .Include(a => a.AssessmentJournals.OrderByDescending(j => j.Id))
               .Where(a => a.SabhaId == sabhaId && a.HasJournalRequest == true)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<Assessment> GetAssessmentForJournal(int? sabhaId, int? kFormId)
        {
            return await assessmentTaxDbContext.Assessments
              .Include(m => m.Street).ThenInclude(st => st.Ward)
              .Include(m => m.AssessmentPropertyType)
              .Include(m => m.Description)
              .Include(m => m.AssessmentTempPartner)
              .Include(m => m.AssessmentTempSubPartner)
              .Include(m => m.AssessmentBalance)
              .Include(a => a.AssessmentBalance.Q1)
              .Include(a => a.AssessmentBalance.Q2)
              .Include(a => a.AssessmentBalance.Q3)
              .Include(a => a.AssessmentBalance.Q4)
              .Include(m => m.Allocation)
              .Where(a => a.Id == kFormId && a.SabhaId == sabhaId )
             .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
              .FirstOrDefaultAsync();

        }


        public async Task<Assessment> GetAssessmentForAssets(int? sabhaId, int? kFormId)
        {
            return await assessmentTaxDbContext.Assessments
              .Include(m => m.Street).ThenInclude(st => st.Ward)
              .Include(m => m.AssessmentPropertyType)
              .Include(m => m.Description)
              .Include(m => m.AssessmentTempPartner)
              .Include(m => m.AssessmentTempSubPartner)
              .Include(m => m.AssessmentBalance)
              .Include(a => a.AssessmentBalance.Q1)
              .Include(a => a.AssessmentBalance.Q2)
              .Include(a => a.AssessmentBalance.Q3)
              .Include(a => a.AssessmentBalance.Q4)
              .Include(m => m.Allocation)
              .Where(a => a.Id == kFormId && a.SabhaId == sabhaId)
              .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
              .FirstOrDefaultAsync();

        }


        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAll(Nullable<int> sabhaId,
            Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType,
            Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId,
            int pageNo, int pageSize)
        {
            var query = assessmentTaxDbContext.Assessments.Include(a => a.Street).ThenInclude(m => m.Ward)
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                .Where(a => assessmentId.HasValue ? a.Id == assessmentId.Value : true)
                .Where(a => streetId.HasValue ? a.Street.Id == streetId.Value : true)
                .Where(a => wardId.HasValue ? a.Street.Ward.Id == wardId.Value : true)
                .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                .Where(a => propertyDescription.HasValue ? a.DescriptionId == propertyDescription.Value : true)
                .Where(a => partnerId.HasValue
                    ? (a.PartnerId == partnerId.Value || a.SubPartnerId == partnerId.Value)
                    : true)
                .Where(a => !string.IsNullOrEmpty(assessmentNo) ? a.AssessmentNo == assessmentNo : true)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);


            // Implement your logic to calculate totalCount

            int totalCount = await query.CountAsync();

            var assessmentList = await query.Skip(pageNo - 1).Take(pageSize).ToListAsync();

            return (totalCount: totalCount, list: assessmentList);
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExclude(List<int?> excludedIds,
            int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription,
            string assessmentNo, int? assessmentId, int? partnerId, string? nic, string? name, int pageNo, int pageSize)
        {
            var query = assessmentTaxDbContext.Assessments.Include(a => a.Street).ThenInclude(m => m.Ward)
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => !excludedIds.Contains(a.Id))
                .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                .Where(a => assessmentId.HasValue ? a.Id == assessmentId.Value : true)
                .Where(a => streetId.HasValue ? a.Street.Id == streetId.Value : true)
                .Where(a => wardId.HasValue ? a.Street.Ward.Id == wardId.Value : true)
                .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                .Where(a => propertyDescription.HasValue ? a.DescriptionId == propertyDescription.Value : true)
                .Where(a => !string.IsNullOrWhiteSpace(nic) ? a.AssessmentTempPartner!.NICNumber == nic : true)
                .Where(a => !string.IsNullOrWhiteSpace(name) ? a.AssessmentTempPartner!.Name == name : true)
                .Where(a => partnerId.HasValue
                    ? (a.PartnerId == partnerId.Value || a.SubPartnerId == partnerId.Value)
                    : true)
                .Where(a => !string.IsNullOrEmpty(assessmentNo) ? a.AssessmentNo == assessmentNo : true)
                 .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);


            // Implement your logic to calculate totalCount

            int totalCount = await query.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

            return (totalCount: totalCount, list: assessmentList);
        }

        public async Task<IEnumerable<Assessment>> GetAll()
        {
            //return
            //await assessmentTaxDbContext.Assessments.Take(1)
            //    //.Include(m => m.ward)
            //    //.Include(m => m.Street)
            //    //.Include(m => m.AssessmentPropertyType)
            //    //.Include(m => m.Description)
            //    //.Include(m => m.AssessmentTempPartner)
            //    //.Include(m => m.SubOwner)
            //    //.Include(m => m.QuarterOpeningBalance)
            //    .Include(m => m.Allocation)
            //    //.OrderBy(m => m.AssessmentOrder)
            //    .ToListAsync();

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Assessment>> GetAllForOffice(int officeId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.OfficeId == officeId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo).Skip(0).Take(10)
                //.Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.SabhaId == sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForWard(int wardid)
        {
            //return
            //    await assessmentTaxDbContext.Assessments
            //    //.Include(m => m.ward)
            //    .Include(m => m.Street)
            //    .Include(m => m.AssessmentPropertyType)
            //    .Include(m => m.Description)
            //    .Include(m => m.AssessmentTempPartner)
            //    .Include(m => m.AssessmentTempSubPartner)
            //    //.Include(m => m.QuarterOpeningBalance)
            //    .Include(m => m.Allocation)
            //    //.Where(m => m.WardId == wardid)
            //    .OrderBy(m => m.OrderNo)
            //    .ToListAsync();


            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.Street.Ward.Id == wardid)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                //.Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForStreet(int streetid)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.Street.Id == streetid)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                //.Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForStreetWithOrdering(int streetId,HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                //.Include(a => a.Street).ThenInclude(st => st.Ward)
                //.Include(a => a.Allocation)
                //.Include(a => a.AssessmentPropertyType)
                //.Include(a => a.Description)
                //.Include(a => a.AssessmentTempPartner)
                //.Include(a => a.AssessmentTempSubPartner)
                //.Include(a => a.AssessmentBalance.Q1)
                //.Include(a => a.AssessmentBalance.Q2)
                //.Include(a => a.AssessmentBalance.Q3)
                //.Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.StreetId == streetId && a.SabhaId == token.sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForCustomerId(int customerid)
        {
            // return
            //     await assessmentTaxDbContext.Assessments
            //     //.Include(m => m.ward)
            //     .Include(m => m.Street)
            //     .Include(m => m.AssessmentPropertyType)
            //     .Include(m => m.Description)
            //     // .Include(m => m.AssessmentTempPartner)
            //     // .Include(m => m.AssessmentTempSubPartner)
            //     //.Include(m => m.QuarterOpeningBalance)
            //     .Include(m => m.Allocation)
            //     .Include(m => m.AssessmentBalance)
            //     .Where(m => m.PartnerId == customerid)
            //     .OrderBy(m => m.OrderNo)
            //     .ToListAsync();
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.PartnerId == customerid)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                //.Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForCustomerIdAndSabhaId(int customerid, int sabhaId)
        {
            return
                await assessmentTaxDbContext.Assessments.AsNoTracking()
                    .Include(m => m.Street).ThenInclude(m => m.Ward)
                    .Include(m => m.AssessmentPropertyType)
                    .Include(m => m.Description)
                     .Include(m => m.AssessmentTempPartner)
                    // .Include(m => m.AssessmentTempSubPartner)
                    //.Include(m => m.QuarterOpeningBalance)
                    .Include(m => m.Allocation)
                    .Include(m => m.AssessmentBalance!.Q1)
                    .Include(m => m.AssessmentBalance!.Q2)
                    .Include(m => m.AssessmentBalance!.Q3)
                    .Include(m => m.AssessmentBalance!.Q4)
                    .Where(m => m.PartnerId == customerid)
                    .Where(m => m.SabhaId == sabhaId)
                    .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                    .OrderBy(m => m.OrderNo)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForSabhaToProcess(int sabhaId)
        {
            var x = await assessmentTaxDbContext.Assessments
                //.AsNoTracking() // Avoids tracking entities
                //.Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                //.Include(a => a.Description)
                //.Include(a => a.AssessmentTempPartner)
                //.Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                //.OrderBy(m => m.OrderNo)
                //.Take(10)
                .ToListAsync();

            return x;
        }

        public async Task<IEnumerable<Assessment>> GetInitProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance)
                .Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.AnnualAmount == null)
                .Where(a => a.AssessmentStatus == AssessmentStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetPartnerUpdateForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.AssessmentTempPartner)
                .Where(a => a.SabhaId == sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetInitNextYearProcessForSabha(int? streetId, int? propertyId, int? assessmentId, HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                //.Include(a => a.AssessmentBalance)
                .Where(a => a.SabhaId == token.sabhaId && a.AssessmentBalance!.AnnualAmount == null)
                .Where(a => streetId.HasValue? a.StreetId == streetId:true)
                .Where(a => propertyId.HasValue? a.PropertyTypeId == propertyId:true)
                .Where(a => assessmentId.HasValue? a.Id == assessmentId:true)
                .Where(a => a.AssessmentStatus == AssessmentStatus.NextYearActive)
                .ToListAsync();
        }


        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExcludeForWarrant(List<int?> excludedIds,
            int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription,
            string assessmentNo, int? assessmentId, int? partnerId, Nullable<int> quarter, int pageNo, int pageSize)
        {
            var query = assessmentTaxDbContext.Assessments.Include(a => a.Street).ThenInclude(m => m.Ward)
                .AsNoTracking() // Avoids tracking entities
                                .Include(a => a.Street).ThenInclude(st => st.Ward)
                                .Include(a => a.Allocation)
                                .Include(a => a.AssessmentPropertyType)
                                .Include(a => a.Description)
                                .Include(a => a.AssessmentTempPartner)
                                .Include(a => a.AssessmentTempSubPartner)

                //.Include(a => a.AssessmentBalance.Q1)
                //.Include(a => a.AssessmentBalance.Q2)
                //.Include(a => a.AssessmentBalance.Q3)
                //.Include(a => a.AssessmentBalance.Q4)

                .Where(a => !excludedIds.Contains(a.Id))
                .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                .Where(a => assessmentId.HasValue ? a.Id == assessmentId.Value : true)
                .Where(a => streetId.HasValue ? a.Street.Id == streetId.Value : true)
                .Where(a => wardId.HasValue ? a.Street.Ward.Id == wardId.Value : true)
                .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                .Where(a => propertyDescription.HasValue ? a.DescriptionId == propertyDescription.Value : true)
                .Where(a => partnerId.HasValue
                    ? (a.PartnerId == partnerId.Value || a.SubPartnerId == partnerId.Value)
                    : true)
                .Where(a => !string.IsNullOrEmpty(assessmentNo) ? a.AssessmentNo == assessmentNo : true)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);

            if (quarter == 1)
            {
                query = query.Include(a => a.AssessmentBalance!.Q1)
                    .Where(a => a.AssessmentBalance!.Q1!.IsCompleted == false && a.AssessmentBalance.Q1.IsOver == true && a.AssessmentBalance.Q1.Warrant == 0 && a.AssessmentBalance.Q1.WarrantMethod == null)
                    .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 2)
            {
                query = query.Include(a => a.AssessmentBalance!.Q2)
                 .Where(a => a.AssessmentBalance!.Q2!.IsCompleted == false && a.AssessmentBalance.Q2.IsOver == true && a.AssessmentBalance!.Q2.Warrant == 0 && a.AssessmentBalance.Q2.WarrantMethod == null)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 3)
            {
                query = query.Include(a => a.AssessmentBalance!.Q3)
                     .Where(a => a.AssessmentBalance!.Q3!.IsCompleted == false && a.AssessmentBalance.Q3.IsOver == true && a.AssessmentBalance!.Q3.Warrant == 0 && a.AssessmentBalance.Q3.WarrantMethod == null)
                    .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 4)
            {

                query = query
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH1)
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH2)
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH3)
                    .Include(a => a.AssessmentBalanceHistories!.OrderByDescending(b => b.Id))
                     .ThenInclude(bh => bh.QH4)
                    .Where(a => a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.IsCompleted == false && a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.Warrant == 0 && a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.WarrantMethod == null)
                   .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);



            }


            // Implement your logic to calculate totalCount

            int totalCount = await query.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

            return (totalCount: totalCount, list: assessmentList);
        }


        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllWarrantedList(List<int?> excludedIds,
           int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription,
           string assessmentNo, int? assessmentId, int? partnerId, Nullable<int> quarter, int pageNo, int pageSize)
        {
            var query = assessmentTaxDbContext.Assessments.Include(a => a.Street).ThenInclude(m => m.Ward)
                .AsNoTracking() // Avoids tracking entities
                                .Include(a => a.Street).ThenInclude(st => st.Ward)
                                .Include(a => a.Allocation)
                                .Include(a => a.AssessmentPropertyType)
                                .Include(a => a.Description)
                                .Include(a => a.AssessmentTempPartner)
                                .Include(a => a.AssessmentTempSubPartner)

                //.Include(a => a.AssessmentBalance.Q1)
                //.Include(a => a.AssessmentBalance.Q2)
                //.Include(a => a.AssessmentBalance.Q3)
                //.Include(a => a.AssessmentBalance.Q4)

                .Where(a => !excludedIds.Contains(a.Id))
                .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                .Where(a => assessmentId.HasValue ? a.Id == assessmentId.Value : true)
                .Where(a => streetId.HasValue ? a.Street.Id == streetId.Value : true)
                .Where(a => wardId.HasValue ? a.Street.Ward.Id == wardId.Value : true)
                .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                .Where(a => propertyDescription.HasValue ? a.DescriptionId == propertyDescription.Value : true)
                .Where(a => partnerId.HasValue
                    ? (a.PartnerId == partnerId.Value || a.SubPartnerId == partnerId.Value)
                    : true)
                .Where(a => !string.IsNullOrEmpty(assessmentNo) ? a.AssessmentNo == assessmentNo : true)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);

            if (quarter == 1)
            {
                query = query.Include(a => a.AssessmentBalance!.Q1)
                    .Where(a => a.AssessmentBalance!.Q1!.IsCompleted == false && a.AssessmentBalance.Q1.IsOver == true && a.AssessmentBalance.Q1.WarrantMethod != null)
                    .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 2)
            {
                query = query.Include(a => a.AssessmentBalance!.Q2)
                 .Where(a => a.AssessmentBalance!.Q2!.IsCompleted == false && a.AssessmentBalance.Q2.IsOver == true && a.AssessmentBalance!.Q2.WarrantMethod != null)
                 .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 3)
            {
                query = query.Include(a => a.AssessmentBalance!.Q3)
                     .Where(a => a.AssessmentBalance!.Q3!.IsCompleted == false && a.AssessmentBalance.Q3.IsOver == true && a.AssessmentBalance!.Q3.Warrant != null)
                     .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);
            }

            if (quarter == 4)
            {

                query = query
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH1)
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH2)
                    //.Include(a => a.AssessmentBalanceHistories!).ThenInclude(bh => bh!.QH3)
                    .Include(a => a.AssessmentBalanceHistories!.OrderByDescending(b => b.Id))
                     .ThenInclude(bh => bh.QH4)
                    .Where(a => a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.IsCompleted == false && a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.WarrantMethod != null)
                    .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive);



            }


            // Implement your logic to calculate totalCount

            int totalCount = await query.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

            return (totalCount: totalCount, list: assessmentList);
        }

        public async Task<IEnumerable<Assessment>> GetQ1Warranting(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaId &&
                            a.AssessmentBalance!.Q1!.IsOver == true && a.AssessmentBalance!.Q1!.IsCompleted == false && a.AssessmentBalance!.Q1!.Warrant == 0)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetQ2Warranting(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaId &&
                            a.AssessmentBalance!.Q2!.IsOver == true && a.AssessmentBalance!.Q2!.IsCompleted == false && a.AssessmentBalance!.Q2!.Warrant == 0)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetQ3Warranting(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaId &&
                            a.AssessmentBalance!.Q3!.IsOver == true && a.AssessmentBalance!.Q3!.IsCompleted == false && a.AssessmentBalance!.Q3!.Warrant == 0)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetQ4Warranting(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Include(a => a.AssessmentBalance)
                .Include(a => a.AssessmentBalanceHistories!.OrderByDescending(b => b.Id))
                     .ThenInclude(bh => bh.QH4)
                    .Where(a => a.SabhaId == sabhaId && a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.IsCompleted == false && a.AssessmentBalanceHistories!.FirstOrDefault()!.QH4!.Warrant == 0)
                  .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                    .ToListAsync();
        }

        public async Task<Assessment> GetWarrantAdjustment(int assessmentId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance)
                .Where(a => a.Id == assessmentId &&
                            a.AssessmentBalance!.Q3!.IsOver == true && a.AssessmentBalance!.Q3!.IsCompleted == false && a.AssessmentBalance!.Q3!.Warrant == 0)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Assessment>> GetYearEndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaId)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetYearEndProcessForFinalAccount(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Include(a=>a.Transactions)
                .Where(a => a.SabhaId == sabhaId)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }


        public async Task<IEnumerable<Assessment>> GetForInitProcessForFinalAccount(List<int?> assessmentIds)
        {
            return await assessmentTaxDbContext.Assessments
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.AssessmentBalance!.Q1)
                .Include(a => a.AssessmentBalance!.Q2)
                .Include(a => a.AssessmentBalance!.Q3)
                .Include(a => a.AssessmentBalance!.Q4)
                .Include(a => a.Transactions)
                .Where(a =>  assessmentIds.Contains(a.Id))
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> PendingCustomizationRequests(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                //.Include(a => a.AssessmentTempPartner)
                //.Include(a => a.AssessmentTempSubPartner)
                //.Include(a => a.AssessmentBalance.Q1)
                //.Include(a => a.AssessmentBalance.Q2)
                //.Include(a => a.AssessmentBalance.Q3)
                //.Include(a => a.AssessmentBalance.Q4)
                .Include(a=>a.NewAllocationRequest)
                    .Include(a => a.AssessmentDescriptionLogs.OrderByDescending(l => l.Id)).ThenInclude(log => log.Description)
                    .Include(a => a.AssessmentPropertyTypeLogs.OrderByDescending(log => log.Id)).ThenInclude(log => log.PropertyType)
                .Where(a => a.SabhaId == sabhaId && (a.DescriptionChangeRequest == true || a.AllocationChangeRequest || a.PropertyTypeChangeRequest))
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> PendingUpdateDescriptionForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.SabhaId == sabhaId && a.DescriptionChangeRequest == true)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> PendingUpdatePropertyTypeForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() // Avoids tracking entities
                .Include(a => a.Street).ThenInclude(st => st.Ward)
                .Include(a => a.Allocation)
                .Include(a => a.AssessmentPropertyType)
                .Include(a => a.Description)
                .Include(a => a.AssessmentTempPartner)
                .Include(a => a.AssessmentTempSubPartner)
                .Include(a => a.AssessmentBalance.Q1)
                .Include(a => a.AssessmentBalance.Q2)
                .Include(a => a.AssessmentBalance.Q3)
                .Include(a => a.AssessmentBalance.Q4)
                .Where(a => a.SabhaId == sabhaId && a.PropertyTypeChangeRequest == true)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
            .ToListAsync();
        }

        public async Task<List<int?>> GetAllKFormIdsFor(Nullable<int> sabhaId,
            Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            //var results = assessmentTaxDbContext.Assessments
            //     .AsNoTracking()
            //     .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
            //     .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
            //     .Where(a => streetId.HasValue ? a.Street!.Id == streetId.Value : true)
            //     .Where(a => wardId.HasValue ? a.Street!.Ward!.Id == wardId.Value : true)
            //     .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
            //        //.Where(a => EF.Functions.Like(a.Id.ToString()!, "%" + "499" + "%"));
            //        .Where(a => EF.Functions.Like(a.Id.ToString()!, "%" + query + "%"));



            //return await results.Select(a => a.Id)
            //           .Take(200)
            //          .ToListAsync();

            return await assessmentTaxDbContext.Assessments
                 .AsNoTracking()
                 .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                 .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                 .Where(a => streetId.HasValue ? a.Street!.Id == streetId.Value : true)
                 .Where(a => wardId.HasValue ? a.Street!.Ward!.Id == wardId.Value : true)
                 .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                 .Where(a => EF.Functions.Like(a.Id.ToString()!, "%" + query + "%"))
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                 .Select(a => a.Id)
                 .Take(200)
                 .ToListAsync();


        }



        public async Task<List<string?>> GetAllAssessmentNoFor(int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, string query)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking()
                .Where(a => sabhaId.HasValue ? a.SabhaId == sabhaId.Value : true)
                .Where(a => officeId.HasValue ? a.OfficeId == officeId.Value : true)
                .Where(a => streetId.HasValue ? a.Street.Id == streetId.Value : true)
                .Where(a => wardId.HasValue ? a.Street.Ward.Id == wardId.Value : true)
                .Where(a => propertyType.HasValue ? a.PropertyTypeId == propertyType.Value : true)
                .Where(a => EF.Functions.Like(a.AssessmentNo!, "%" + query + "%"))
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .Select(a => a.AssessmentNo)
                .Take(200)
                .ToListAsync();
        }
        public async Task<Assessment> getAssessmentForUpdate(string assessmentNo, Nullable<int> assessmentId, HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking()
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.Allocation)
               .Include(m => m.AssessmentBalance)
               .Include(m => m.AssessmentTempPartner)
               .Where(a => assessmentId.HasValue ? a.Id == assessmentId.Value : !string.IsNullOrEmpty(assessmentNo) ? a.AssessmentNo == assessmentNo : false)
               .Where(a => a.SabhaId == token.sabhaId)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)

               .FirstOrDefaultAsync();
        }
        public async Task<List<int?>> GetAllPartnerUpdatedPartnerIdsForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            return await assessmentTaxDbContext.Assessments
                .Where(a => a.SabhaId == sabhaId && a.IsPartnerUpdated == true)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .Select(a => a.PartnerId)
                .ToListAsync();
        }



        public async Task<IEnumerable<AssessmentTempPartner>> GetAllTempPartnersNicsForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            return await assessmentTaxDbContext.Assessments
                .Where(a => a.SabhaId == sabhaId && a.IsPartnerUpdated == false)
                .Where(a => a.AssessmentTempPartner!.NICNumber != null)
                .Where(a => EF.Functions.Like(a.AssessmentTempPartner!.NICNumber!, "%" + query + "%"))
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                 .Select(a => a.AssessmentTempPartner!)
                 .Distinct()
                 .Take(400)
                    .ToListAsync();

        }

        public async Task<IEnumerable<AssessmentTempPartner>> GetAllTempPartnersNamesForSabha(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            return await assessmentTaxDbContext.Assessments
                .Where(a => a.SabhaId == sabhaId && a.IsPartnerUpdated == false)
                .Where(a => a.AssessmentTempPartner!.Name != null)
                 //.Where(a => EF.Functions.Like(a.AssessmentTempPartner!.Name!, "%" + query + "%"))
                 .Where(a => EF.Functions.Like(a.AssessmentTempPartner!.Name!.Replace(".", ""), "%" + query + "%"))
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                 .Select(a => a.AssessmentTempPartner!)
                 .Take(400)
                 .ToListAsync();

        }

        public async Task<IEnumerable<AssessmentBalance>> GetForEndSessionToDisableTransaction(int officeId)
        {
            return await assessmentTaxDbContext.AssessmentBalances.Where(b => b.Assessment!.OfficeId == officeId && b.HasTransaction == true).ToListAsync();
        }


        public async Task<bool> HasAssessmentForSabha(int sabhaId)
        {
            return assessmentTaxDbContext.Assessments.Any(a => a.SabhaId == sabhaId);
        }

        public async Task<bool> HasAssessmentForOffice(int officeId)
        {
            return assessmentTaxDbContext.Assessments.Any(a => a.OfficeId == officeId);
        }

        public async Task<bool> ISKFormExist(int id, HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments.AnyAsync(a => a.Id == id && a.SabhaId == token.sabhaId && a.AssessmentStatus != AssessmentStatus.Delete);
        }
        public async Task<bool> ISAssessmentNoExist(string assessmentNo, int streetId, HTokenClaim token)
        {
            return await assessmentTaxDbContext.Assessments.AnyAsync(a => a.AssessmentNo == assessmentNo && a.StreetId == streetId && a.SabhaId== token.sabhaId && a.AssessmentStatus != AssessmentStatus.Delete);
        }


        public async Task<Assessment> GetAssessmentForCustomize(int sabhaId, int assessmentId)
        {
            return  await assessmentTaxDbContext.Assessments
               .AsNoTracking() // Avoids tracking entities
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .FirstOrDefaultAsync(a => a.Id == assessmentId && a.SabhaId== sabhaId);

           
        }

        public async Task<Assessment> GetForCustomize(int? id)
        {
            return await assessmentTaxDbContext.Assessments
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               //.Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .Include(m=>m.NewAllocationRequest)
                .Include(m => m.AssessmentDescriptionLogs.OrderByDescending(log => log.Id).Take(1))
                .Include(m => m.AssessmentPropertyTypeLogs.OrderByDescending(log => log.Id).Take(1))
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentOrderByPattern(int sabhaId,int streetId)
        {
            var query = @"
        SELECT * 
        FROM cat_twenty_assmt.assmt_assessments 
        WHERE assmt_sabha_id = {0} AND assmt_street_id = {1}
        ORDER BY 
            CAST(SUBSTRING_INDEX(assmt_no, '/', 1) AS UNSIGNED),
            LENGTH(SUBSTRING_INDEX(assmt_no, '/', 2)),
            SUBSTRING_INDEX(SUBSTRING_INDEX(assmt_no, '/', 2), '/', -1) + 0,
            SUBSTRING_INDEX(assmt_no, '/', -1)";

            var sortedAssessments = await assessmentTaxDbContext.Assessments
                .FromSqlRaw(query, sabhaId, streetId)
                .ToListAsync();

            return sortedAssessments;
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentForRenewal(int sabhaid)
        {
            return await assessmentTaxDbContext.Assessments.AsNoTracking()
                .Include(m => m.Street).ThenInclude(m => m.Ward)
                .Include(m => m.AssessmentPropertyType)
                .Include(m => m.AssessmentTempPartner)
                .Include(m => m.Description)
                .Include(m => m.Allocation)
                //.Include(m => m.AssessmentBalance!.Q1)
                //.Include(m => m.AssessmentBalance!.Q2)
                //.Include(m => m.AssessmentBalance!.Q3)
                //.Include(m => m.AssessmentBalance!.Q4)
                .Where(a => a.SabhaId == sabhaid)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.StreetId).ThenBy(m => m.OrderNo)
                .ToListAsync();
        }
        public async Task<IEnumerable<Assessment>> GetAssessmentForRenewal(HTokenClaim token, int? propertyTypeId)
        {
            return await assessmentTaxDbContext.Assessments
                //.AsNoTracking()
                //.Include(m => m.Street).ThenInclude(m => m.Ward)
                .Include(m => m.AssessmentPropertyType)
                //.Include(m => m.AssessmentTempPartner)
                //.Include(m => m.Description)
                .Include(m => m.Allocation)
                .Include(m => m.AssessmentBalance)
                .Include(m => m.AssessmentBalance!.NQ1)
                .Include(m => m.AssessmentBalance!.NQ2)
                .Include(m => m.AssessmentBalance!.NQ3)
                .Include(m => m.AssessmentBalance!.NQ4)
                .Where(a => propertyTypeId.HasValue ? a.NextYearPropertyTypeId.HasValue ? a.NextYearPropertyTypeId == propertyTypeId : a.PropertyTypeId == propertyTypeId: true)
                .Where(a => a.SabhaId == token.sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearActive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation 
                        //a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        //a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive
                        )
                //.OrderBy(m => m.StreetId).ThenBy(m => m.OrderNo)
                //.Take(10)
                .OrderBy(m => m.StreetId).ThenBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForIds(List<int> assessmentIds)
        {
            return await assessmentTaxDbContext.Assessments.AsNoTracking()
                .Include(m => m.Street).ThenInclude(m => m.Ward)
                .Include(m => m.AssessmentPropertyType)
                .Include(m => m.AssessmentTempPartner)
                .Include(m => m.Description)
                .Include(m => m.Allocation)
                .Include(m => m.AssessmentBalance!.Q1)
                .Include(m => m.AssessmentBalance!.Q2)
                .Include(m => m.AssessmentBalance!.Q3)
                .Include(m => m.AssessmentBalance!.Q4)
                .Where(m => assessmentIds.Contains(m.Id.Value)) // Filter by IDs
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .OrderBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetAllForIdsAndSabha(List<int> assessmentIds, int sabhaid)
        {
            return await assessmentTaxDbContext.Assessments.AsNoTracking()
                .Include(m => m.Street).ThenInclude(m => m.Ward)
                .Include(m => m.AssessmentPropertyType)
                .Include(m => m.AssessmentTempPartner)
                .Include(m => m.Description)
                .Include(m => m.Allocation)
                .Include(m => m.AssessmentBalance!.Q1)
                .Include(m => m.AssessmentBalance!.Q2)
                .Include(m => m.AssessmentBalance!.Q3)
                .Include(m => m.AssessmentBalance!.Q4)
                .Where(m => assessmentIds.Contains(m.Id.Value)) // Filter by IDs
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .Where(a => a.SabhaId == sabhaid)
                .OrderBy(m => m.OrderNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetForFinalAccountInit(int sabhaId)
        {
            return await assessmentTaxDbContext.Assessments
                .AsNoTracking() 
                .Include(a => a.AssessmentBalance)
                //.Include(a => a.AssessmentBalance!.Q1)
                //.Include(a => a.AssessmentBalance!.Q2)
                //.Include(a => a.AssessmentBalance!.Q3)
                //.Include(a => a.AssessmentBalance!.Q4)
                .Include(a => a.Transactions!.Where(t => t.Type == AssessmentTransactionsType.Init || t.Type == AssessmentTransactionsType.JournalAdjustment || t.Type == AssessmentTransactionsType.SystemAdjustment))
                .Where(a => a.SabhaId == sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)

                .ToListAsync();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}