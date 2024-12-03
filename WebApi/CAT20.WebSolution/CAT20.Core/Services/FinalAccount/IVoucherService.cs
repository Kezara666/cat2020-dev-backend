using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.Final;

namespace CAT20.Core.Services.FinalAccount;

public interface IVoucherService
{
    Task<(bool,string?, Voucher)> CreateVoucher(SaveVoucherResource voucherResource, HTokenClaim token);
    Task<(bool,string)> CreateDepositVoucher(SaveDepositVoucherResource depositVoucher, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateSubImprestVoucher(SubImprest subImprest,bool IsNew,Session session, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateAccountTransferVoucher(AccountTransfer accountTransfer,Session session, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateAccountRefundingVoucher(AccountTransfer accountTransfer, AccountTransferRefunding refunding,Session session, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateSettlementVoucher(SubImprest subImprest,Session session, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateOrderRePaymentVoucher(SaveRepaymentVoucher voucherResource, HTokenClaim token);
    Task<(bool,string?,Voucher)> AdvancedBVoucher(AdvanceB loan, Session session, HTokenClaim token);
    Task<(bool,string,Voucher)> CreateSalaryVoucher(SaveSalaryVoucher voucherResource, HTokenClaim token);


    Task<(int totalCount,IEnumerable<VoucherResource> list)> getVoucherForApproval(int sabhaId, List<int?> excludedIds, int? category, int stage, int pageNo, int  pageSize, string? filterKeyWord);
    Task<(int totalCount,IEnumerable<VoucherResource> list)> searchVoucherByKeywordForSurcharge(int sabhaId, int pageNo, int  pageSize, string? filterKeyWord);
    Task<(int totalCount,IEnumerable<VoucherResource> list)> getVoucherProgressRejected(int sabhaId, List<int?> stage, int pageNo, int  pageSize, string? filterKeyWord);
    Task<IEnumerable<VoucherResource>> getVoucherForPsReport(int sabhaId, int year, int month);
    Task<bool> MakeApproval(MakeVoucherApproveRejectResource request, HTokenClaim token);
    Task<VoucherResource> GetVoucherById(int id);
    Task<(bool,string)> PostSettlement(Voucher Voucher, HTokenClaim token);
    Task<(bool,string)> CreateDepositVoucherCheques(List<VoucherCheque> voucherCheques, List<Voucher> Vouchers, HTokenClaim token);

    Task<(bool, string)> WithdrawVoucher(int voucherId, HTokenClaim token);

}