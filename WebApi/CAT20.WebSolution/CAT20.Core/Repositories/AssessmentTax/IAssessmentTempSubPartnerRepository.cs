namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentTempSubPartnerRepository : IRepository<Models.AssessmentTax.AssessmentTempSubPartner>
    {
        Task<Models.AssessmentTax.AssessmentTempSubPartner> GetById(int id);
        Task<IEnumerable<Models.AssessmentTax.AssessmentTempSubPartner>> GetAll();
        Task<IEnumerable<Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForOffice(int officeid);
        Task<IEnumerable<Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<Models.AssessmentTax.AssessmentTempSubPartner>> GetAllForAssessmentId(int assessmentid);
    }
}
