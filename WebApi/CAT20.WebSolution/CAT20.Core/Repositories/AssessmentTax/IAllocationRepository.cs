using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAllocationRepository : IRepository<Allocation>
    {
        Task<Allocation> GetById(int id);
        Task<IEnumerable<Allocation>> GetAll();
        Task<IEnumerable<Allocation>> GetAllForOffice(int officeid);
        Task<IEnumerable<Allocation>> GetAllForSabha(int sabhaid);
        Task<Allocation> GetForAssessment(int assessmentid);
        Task<IEnumerable<Allocation>> GetForNextYearAllocationsUpdate(int sabhaId);

        
    }
}
