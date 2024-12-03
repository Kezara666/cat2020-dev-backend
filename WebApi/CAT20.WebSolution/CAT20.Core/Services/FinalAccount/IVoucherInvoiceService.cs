using CAT20.Core.DTO.Final.Save;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IVoucherInvoiceService
    {
         Task<(bool,string?)> UploadInvoice(SaveVoucherInvoice voucherInvoice, object environment, string _uploadsFolder);
         Task<(bool,string?)> RemoveInvoice(VoucherInvoice voucherInvoice, object environment, string _uploadsFolder);
         Task<IEnumerable<VoucherInvoice>> GetAllInvoicesForVoucher(int voucherId );
    }
}
