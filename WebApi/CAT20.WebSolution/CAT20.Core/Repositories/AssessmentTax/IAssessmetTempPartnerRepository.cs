using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmetTempPartnerRepository : IRepository<AssessmentTempPartner>
    {
        Task<AssessmentTempPartner> GetById(int id);
        //Task<IEnumerable<AssessmetTempPartner>> GetAll();
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForOffice(int officeid);
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForSabha(int sabhaid);
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForWard(int wardid);
    }
}
