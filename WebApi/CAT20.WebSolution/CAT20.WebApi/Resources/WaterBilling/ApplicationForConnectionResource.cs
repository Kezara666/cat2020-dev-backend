using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.User;
using CAT20.WebApi.Resources.WaterBilling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class ApplicationForConnectionResource
    {
      
        public string? ApplicationNo { get; set; }
       
        public int?  PartnerId { get; set; }
       
        public int BillingId { get; set; }

        public int? SubRoadId { get; set; } // Foreign key
        public int? officeId { get; set; } //key pattern
        public int RequestedNatureId { get; set; } // Foreign key

        public int? RequestedConnectionId { get; set; }

        public int? ApprovedBy { get; set; }

        public string? Comment { get; set; }
        public virtual PartnerResource? PartnerAccount { get; set; }
        public virtual PartnerResource? BillingAccount { get; set; }

        public virtual WaterProjectSubRoadResource? SubRoad { get; set; }
        public virtual WaterProjectNature? Nature { get; set; }

        //public int? ApplicationType { get; set; }
        //// Navigation property to represent the one-to-many relationship
        //[JsonIgnore]
        public virtual ICollection<ApplicationForConnectionDocumentResource>? SubmittedDocuments { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual UserActionByResources? UserApprovedBy { get; set; }

    }
}
