namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentEndSessionService
    {

        Task<bool> EndSessionDisableTransaction(int officeId);
    }
}
