using CAT20.Core.DTO.Final.Save;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IVoucherDocumentService
    {
        Task<(bool,string?)> UploadDocument(SaveVoucherDocument voucherDocument, object environment, string _uploadsFolder);
        Task<(bool,string?)> RemoveDocument(VoucherDocument voucherDocument, object environment, string _uploadsFolder);

        Task<IEnumerable<VoucherDocument>> GetAllDocumentsForVoucher(int voucherId);


    }
}
