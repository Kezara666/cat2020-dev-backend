using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SelectedDepositResource
    {
        public int? Id { get; set; }

        //public int DepositSubCategoryId { get; set; }
        //public int LedgerAccountId { get; set; }
        //public int? SubInfoId { get; set; }
        //public DateTime DepositDate { get; set; }
        //public int? MixOrderId { get; set; }
        //public int? MixOrderLineId { get; set; }
        //public String ReceiptNo { get; set; }
        //public String Description { get; set; }

        //public int PartnerId { get; set; }
        //[Precision(18, 2)]
        //public decimal? InitialDepositAmount { get; set; }
        //[Precision(18, 2)]
        //public decimal? ReleasedAmount { get; set; }
        //[Precision(18, 2)]
        //public decimal? HoldAmount { get; set; }
        
        [Precision(18, 2)]
        public decimal PayingAmount { get; set; }

        //public int? SabhaId { get; set; }
        //public int? OfficeId { get; set; }



    }
}
