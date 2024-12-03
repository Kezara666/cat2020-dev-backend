using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterConnectionResource
    {


        public int? Id { get; set; }
        public string? ConnectionId { get; set; }

        public virtual MeterConnectInfoResource? MeterConnectInfo { get; set; }

        public int? PartnerId { get; set; }
        public int BillingId { get; set; }

        public int? SubRoadId { get; set; } // Foreign key
        public virtual WaterProjectSubRoadResource? SubRoad { get; set; }

        public int ActiveStatus { get; set; }
        public int ActiveNatureId { get; set; }
        public virtual WaterProjectNatureResource? ActiveNature { get; set; }

        public virtual PartnerResource? PartnerAccount { get; set; }
        public virtual PartnerResource? BillingAccount { get; set; }
        public DateOnly? InstallDate { get; set; }

        public bool? StatusChangeRequest { get; set; }
        public bool? NatureChangeRequest { get; set; }


        // Navigation property to represent the one-to-one relationship

        public virtual OpeningBalanceInformationResource? OpeningBalanceInformation { get; set; }


        //Navigation property to represent the one-to-many relationship
        public virtual ICollection<WaterConnectionNatureLogResource>? NatureInfos { get; set; }
        public virtual ICollection<WaterConnectionStatusLogResource>? StatusInfos { get; set; }
        public virtual ICollection<WaterBillDocumentResource>? Documents { get; set; }
        public virtual ICollection<WaterConnectionBalanceResource>? Balances { get; set; }



        //additional one to one filed 

        public decimal? RunningOverPay { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
