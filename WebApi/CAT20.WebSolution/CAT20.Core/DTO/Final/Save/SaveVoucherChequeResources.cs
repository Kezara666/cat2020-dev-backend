using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.FinalAccount;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherChequeResources
    {

        public int? Id { get; set; }
        public string ChequeNo { get; set; }
        public int BankId { get; set; }
        public VoucherPayeeCategory? PayeeCategory { get; set; }
        public int PayeeId { get; set; }
        public string PayeeName { get; set; }
        public decimal Amount { get; set; }
        public virtual List<SelectedVoucherItemsForCheque>? SubVoucherItems { get; set; }

    }
}
