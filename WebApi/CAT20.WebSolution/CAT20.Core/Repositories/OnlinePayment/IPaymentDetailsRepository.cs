using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment;

public interface IPaymentDetailsRepository:IRepository<PaymentDetails>
{
    Task<PaymentDetails> GetById(int? id);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetAllPaymentDetails(int officeId,int? pageNumber, int? pageSize,
        string? filterKeyword );
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetOtherPaymentDetails(int officeId,int? pageNumber, int? pageSize, string? filterKeyword );
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> getDisputes(int officeId,int? pageNumber, int? pageSize, string? filterKeyword );
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistoryForOffice(int officeId,int? pageNumber, int? pageSize,
        string? filterKeyword );
    Task<IEnumerable<PaymentDetails>> getPartnerPaymentHistory(int partnerId,int? pageNumber,int? pageSize );
    Task<IEnumerable<PaymentDetails>> getPartnerDisputes(int partnerId,int? pageNumber,int? pageSize );
    Task<IEnumerable<PaymentDetails>> paymentDetailSheduler();

    Task Save(PaymentDetails paymentDetails);
}