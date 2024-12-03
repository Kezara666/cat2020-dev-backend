using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount.logs
{
    public class VoucherChequeLog
    {
        public int? Id { get; set; }
        [Required]
        public int? VoucherChequeId { get; set; }
        [Required]
        public string? ChequeNo { get; set; }
        public bool? IsPrinted { get; set; }

        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }

        public virtual VoucherCheque? VoucherCheque { get; set; }
    }
}
