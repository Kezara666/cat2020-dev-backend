using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopAgreementActivityLog
    {
        public int? Id { get; set; }
        public int ShopId { get; set; }

      //  public DateOnly? AgreementCloseDate { get; set; }

        public DateOnly? CurrentAgreementEnddate { get; set; }
        public DateOnly? AgreementExtendEndDate { get; set; }


        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        public int? ApprovedBy { get; set; }
        public string? ApproveComment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
