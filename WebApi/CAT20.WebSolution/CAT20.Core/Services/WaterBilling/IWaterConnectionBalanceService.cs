using CAT20.Core.HelperModels;
using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionBalanceService
    {

        Task<IEnumerable<WaterConnection>> GetToBillProcess(int year, int month, List<int> subraodIds);
        Task<bool> ProcessBills(int year, int month, List<int> subraodIds, int userId, HTokenClaim token);
        Task<IEnumerable<WaterConnection>> GetProcessedBills(int year, int month, List<int> subroadIds);
        Task<IEnumerable<WaterConnection>> GetPreviousBills(int year, int month, List<int> subroadIds);
        Task<WaterConnection> getPreviousBillForWaterConnection(int year, int month, int wcId);


        /*get for add reading*/

        Task<WaterConnection> GetForAddReadingByBarCode(int officeId,string barcode);
        Task<WaterConnection> GetForAddReadingByConnectionId(int officeId, string connectionId);
        Task<WaterConnection> GetForAddReadingByConnectionNo(int officeId, string connectionNo);
        Task<WaterConnection> GetForAddReadingById(int wcId);


        Task<bool> AddMeterReading(WaterConnectionBalance wc,HTokenClaim token);

        /*get for payments*/
        Task<WaterConnection> GetForPaymentsByBarCode(int officeId, string barcode);
        Task<WaterConnection> GetForPaymentsByConnectionId(int officeId,string connectionId);
        Task<WaterConnection> GetForPaymentsByConnectionNo(int officeId, string connectionNo);
        Task<WaterConnection> GetForPaymentsById(int wcPrimaryId);


        Task<IEnumerable<WaterConnectionBalance>> GetForPrintBills(int year, int month, List<int> subroadIds);
        //Task<IEnumerable<WaterConnectionBalance>> CalculatePayments(int wcPrimaryId, decimal amount, bool isPayment);
        Task<(decimal? runningOverPay,IEnumerable<WaterConnectionBalance> WaterConnectionBalances, HWaterBillBalance hWaterBillBalance)> CalculatePayments(int wcPrimaryId,DateTime sessionData, decimal amount, bool isPayment,bool isProcess, int userId);


        Task<(int totalCount, IEnumerable<WaterConnectionBalance> list)> GetPreviousBills(int wcId, int pageNo);
    }
}
