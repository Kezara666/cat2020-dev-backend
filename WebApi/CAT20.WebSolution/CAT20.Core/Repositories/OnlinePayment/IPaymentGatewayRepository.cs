using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment;

public interface IPaymentGatewayRepository:IRepository<PaymentGateway>
{
    Task<PaymentGateway> GetBySabhaId(int? id);
    Task<IEnumerable<PaymentGateway>> getAll();
    Task<IEnumerable<PaymentGateway>> getAllByProvinceId(int provinceId);
}