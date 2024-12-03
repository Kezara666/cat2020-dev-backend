using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterConnectionBalanceRepository : IRepository<WaterConnectionBalance>
    {


        /*for add meter reading*/

        Task<WaterConnection> GetForAddReadingByBarCode(int officeId,string barcode);
        Task<WaterConnection> GetForAddReadingByConnectionId(int officeId,string connectionId);
        Task<WaterConnection> GetForAddReadingByConnectionNo(int officeId, string connectionNo);
        Task<WaterConnection> GetForAddReadingById(int wcPrimaryId);


        /*get for payments*/
        Task<WaterConnection> GetForPaymentsByBarCode(int officeId,string barcode);
        Task<WaterConnection> GetForPaymentsByConnectionId(int officeId, string connectionNo);
        Task<WaterConnection> GetForPaymentsByConnectionNo(int officeId, string connectionNo);
        Task<WaterConnection> GetForPaymentsById(int wcPrimaryId);

        Task<WaterConnection> CalculatePayments(int wcPrimaryId);
        Task<IEnumerable<WaterConnectionBalance>> ReversePayments(int wcPrimaryId,decimal? amount);

        Task<IEnumerable<WaterConnection>> GetForPayments(List<int> partnerId);
        Task<IEnumerable<WaterConnection>> getWaterBill(int partnerId, int sabhaId);
        

        Task<IEnumerable<WaterConnection>> getWaterConnection(int partnerId);
        Task<IEnumerable<WaterConnection>> GetToBillProcess(int year, int month, List<int> subroadIds);

        Task<IEnumerable<WaterConnection>> GetProcessedBills(int year, int month, List<int> subroadIds);
        Task<IEnumerable<WaterConnection>> GetPreviousBills(int year, int month, List<int> subroadIds);
        Task<WaterConnection> getPreviousBillForWaterConnection(int year, int month, int wcId);

        Task<IEnumerable<WaterConnectionBalance>> GetForPrintBills(int year, int month, List<int> subroadIds);

        Task<(int totalCount, IEnumerable<WaterConnectionBalance> list)> GetPreviousBills(int wcId, int pageNo);
    }
}
