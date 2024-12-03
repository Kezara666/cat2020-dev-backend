using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;


namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentBalanceService
    {
        Task<AssessmentBalance> GetById(int id);
        Task<AssessmentBalance> Create(AssessmentBalance obj);
        Task<IEnumerable<AssessmentBalance>> BulkCreate(IEnumerable<AssessmentBalance> objs);
        Task Update(AssessmentBalance objToBeUpdated, AssessmentBalance obj);
        Task Delete(AssessmentBalance obj);
        Task<IEnumerable<AssessmentBalance>> GetAll();
        Task<IEnumerable<AssessmentBalance>> GetAllForOffice(int officeid);
        Task<IEnumerable<AssessmentBalance>> GetAllForSabha(int sabhaid);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentId(int assessementid);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYear(int assessementid, int year);
        Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYearAndQuarter(int assessementid, int year, int quarter);





        //balance calculation service


        HPerBalance PerCalculator(AssessmentBalance balance, AssessmentRates rates, (bool, bool, bool, bool) checkBoxes, int month);
        (HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, decimal) CalculatePaymentBalance(AssessmentBalance balance, AssessmentRates rates, decimal amount, int month, bool isPayament);

    }
}
