using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentTempSubPartnerService
    {
        Task<AssessmentTempSubPartner> GetById(int id);
        Task<AssessmentTempSubPartner> Create(AssessmentTempSubPartner obj);
        Task<IEnumerable<AssessmentTempSubPartner>> BulkCreate(IEnumerable<AssessmentTempSubPartner> objs);
        Task Update(AssessmentTempSubPartner objToBeUpdated, AssessmentTempSubPartner obj);
        Task Delete(AssessmentTempSubPartner obj);
        Task<IEnumerable<AssessmentTempSubPartner>> GetAll();
        Task<IEnumerable<AssessmentTempSubPartner>> GetAllForOffice(int officeid);
        Task<IEnumerable<AssessmentTempSubPartner>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<AssessmentTempSubPartner>> GetAllForAssessmentId(int assessmentid);
    }
}
