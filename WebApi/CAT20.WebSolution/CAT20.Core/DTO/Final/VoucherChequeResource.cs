using CAT20.Core.DTO.Final;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.Vote;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final
{

    public class VoucherChequeResource
    {
        public int? Id { get; set; }
        public string ChequeNo { get; set; }
        public int BankId { get; set; }
        public string PayeeName { get; set; }

        public int PayeeId { get; set; }

        public decimal Amount { get; set; }
        public int SabhaId { get; set; }


        public bool? IsPrinted { get; set; }

        //public virtual List<VoucherChequeActionLogResource>? ActionLogs { get; set; }
        //public virtual List<VoucherChequeLogResource>? VoucherChequeLogs { get; set; }

        public virtual List<VoucherItemsForChequeResource>? VoucherItemsForCheque { get; set; }

        public VoucherCategory? ChequeCategory { get; set; }
        public string? ChequeCategoryString { get; set; }
        public string? VoucherItemIds { get; set; }



        //// mandatory fields
        //public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }

        //public int? UpdatedBy { get; set; }




        public virtual AccountDetailOnlyBankId? AccountDetail { get; set; }
        public virtual IEnumerable<VoucherResource>? Vouchers { get; set; } 
      
    }

}