using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class ApplicationForConnectionRepository : Repository<ApplicationForConnection>, IApplicationForConnectionRepository
    {
        public ApplicationForConnectionRepository(DbContext context) : base(context)
        {
        }

        public async Task<ApplicationForConnection> GetById(string applicationNo)
        {
            return await waterBillingDbContext.ApplicationForConnections.FirstOrDefaultAsync(afc => afc.ApplicationNo == applicationNo);
        }

        public async Task<ApplicationForConnection> GetInfoById(string applicationNo)
        {
            return await waterBillingDbContext.ApplicationForConnections.Include(afc => afc.SubRoad).ThenInclude(sr => sr.WaterProject)
                .Include(afc=>afc.Nature)
                .Include(afc => afc.SubRoad.MainRoad).Include(afc => afc.SubmittedDocuments).FirstOrDefaultAsync(afc => afc.ApplicationNo == applicationNo);
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value)).Include(wc=>wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.Status==1).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllByApplicationInfoByOfficeId(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.Status==1 ).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();

          
        }

        public Task<IEnumerable<ApplicationForConnection>> GetAllByApplicationIds()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllPendingApprovalApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == null && wc.RequestedConnectionId == null  && wc.Status==1).Include(afc=>afc.SubRoad!.MainRoad).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();

        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllByApplicationIds(List<string> applicationIds)
        {
            return await waterBillingDbContext.ApplicationForConnections.Include(ap => ap.SubmittedDocuments).Where(ap=> applicationIds.Contains(ap.ApplicationNo)).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }

        //--
        public async Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == true && wc.IsAssigned==false && wc.RequestedConnectionId == null).Include(wc => wc.Nature).OrderByDescending(ap => ap.ApprovedAt).ToListAsync();
        }


        //---
        public async Task<IEnumerable<ApplicationForConnection>> GetAllNewlyApprovedApplicationsForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == true && wc.IsAssigned == false && wc.RequestedConnectionId == null)
                .Include(wc => wc.Nature)
                .Include(ap => ap.SubmittedDocuments)
                .Include(ap=>ap.Nature)
                .OrderByDescending(ap => ap.ApprovedAt).ToListAsync();
        }


        public async Task<ApplicationForConnection> GetApprovedApplicationInfoById(string applicationNo)
        {
            return await waterBillingDbContext.ApplicationForConnections.Include(afc => afc.SubRoad).ThenInclude(sr => sr.WaterProject).Include(afc => afc.SubRoad.MainRoad).Include(afc => afc.SubmittedDocuments).FirstOrDefaultAsync(afc => afc.ApplicationNo == applicationNo && afc.IsApproved == true);
        }

        public async Task<ApplicationForConnection> IsDeleteble(string applicationNo)
        {
            return await waterBillingDbContext.ApplicationForConnections.FirstOrDefaultAsync(afc => afc.ApplicationNo == applicationNo && afc.IsApproved == null);
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
            
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == false && wc.Status == 1).Include(afc=>afc.SubRoad!.MainRoad).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();


        }


        public async Task<IEnumerable<ApplicationForConnection>> GetAllRejectedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == false && wc.Status == 1).Include(wc => wc.Nature).ToListAsync();
        }

        //----- [Start - getAllApprovedApplicationsForOffice/{office id}] ---------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == true && wc.Status == 1).Include(afc => afc.SubRoad!.MainRoad).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }
        //----- [End - getAllApprovedApplicationsForOffice/{office id}] -----------------


        //----- [Start - getAllApprovedApplicationForSabha/{sabha Id}] -------------
        public async Task<IEnumerable<ApplicationForConnection>> GetAllApprovedApplicationsForSabha(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == true && wc.Status == 1).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationForConnection>> GetAllPendingCustomerChangeRequest(List<int> officeIdListOfSabha)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.ApplicationForConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.IsApproved == null && wc.RequestedConnectionId!=null && wc.Status == 1).Include(afc => afc.SubRoad!.MainRoad).Include(wc => wc.Nature).Include(ap => ap.SubmittedDocuments).ToListAsync();
        }
        //----- [End - getAllApprovedApplicationForSabha/{sabha Id}] ---------------
        //--------------------------------------------------------------------------------------------------
    }
}
