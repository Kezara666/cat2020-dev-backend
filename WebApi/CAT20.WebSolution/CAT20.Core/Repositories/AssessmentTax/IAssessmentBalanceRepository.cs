using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentBalanceRepository : IRepository<AssessmentBalance>
    {
        Task<AssessmentBalance> GetById(int id);
        Task<IEnumerable<AssessmentBalance>> GetAll();
        Task<IEnumerable<AssessmentBalance>> GetAllForOffice(int officeid);
        Task<IEnumerable<AssessmentBalance>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentId(int assessementid);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYear(int assessementid, int year);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYearAndQuarter(int assessementid, int year, int quarter);

        //Transactions

        Task<IEnumerable<AssessmentBalance>> GetForOrderTransaction(List<int?> assessmentIds);
        Task<AssessmentBalance> GetForOrderTransaction(int assessmentIds);



        // Process Repositories

        Task<AssessmentBalance?> GetByIdToProcessPayment(int assessmentId);
        Task<IEnumerable<AssessmentBalance>> GetAllForSabhaToProcess(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetInitProcessForSabha(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetJanuaryEndProcessForSabha(int sabhaId);

        Task<IEnumerable<AssessmentBalance>> GetQ1EndProcessForSabha(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetQ2EndProcessForSabha(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetQ3EndProcessForSabha(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetQ4EndProcessForSabha(int sabhaId);
        Task<IEnumerable<AssessmentBalance>> GetForBackup(int sabhaId);
        Task<AssessmentBalance> GetForJournal(int assessmentId);

    }
}
