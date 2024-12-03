using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.Core.Models.FinalAccount
{

    public class VoucherCheque
    {
        public int? Id { get; set; }
        [Required]
        public string ChequeNo { get; set; }
        public string? GrpId { get; set; }
        [Required]
        public int BankId { get; set; }

        [Required]
        public VoucherPayeeCategory? PayeeCategory { get; set; }
        [Required]
        public string? PayeeName { get; set; }

        public int PayeeId { get; set; }
        public string? VoucherIdsAsString { get; set; }


        public decimal Amount { get; set; }
        public int SabhaId { get; set; }


        public bool? IsPrinted { get; set; }

        public virtual List<VoucherChequeActionLog>? ActionLogs { get; set; }
        public virtual List<VoucherChequeLog>? VoucherChequeLogs { get; set; }
        public virtual List<VoucherItemsForCheque>? VoucherItemsForCheque { get; set; }

        public VoucherCategory? ChequeCategory { get; set; }
        public string ? VoucherItemIds { get; set; }

        public List<int> getVoucherIds()
        {
            return this.VoucherIdsAsString!.Split(',').Select(int.Parse).ToList();
        }

        public List<int> getDepositVoucherItemIds()
        {
            return this.VoucherItemIds!.Split(',').Select(int.Parse).ToList();
        }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }


        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /*no db cloumns */

        [NotMapped]
        public string? VoucherNoAsString { get; set; }
    }
}