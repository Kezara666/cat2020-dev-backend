using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmetTempPartnerService 
    {
        Task<AssessmentTempPartner> GetById(int id);
        Task<AssessmentTempPartner> Create(AssessmentTempPartner obj);
        Task Update(AssessmentTempPartner objToBeUpdated, AssessmentTempPartner obj);
        Task Delete(AssessmentTempPartner obj);
        //Task<IEnumerable<AssessmetTempPartner>> GetAll();
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForOffice(int officeid);
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForSabha(int sabhaid);
        //Task<IEnumerable<AssessmetTempPartner>> GetAllForWard(int wardid);
    }
}
