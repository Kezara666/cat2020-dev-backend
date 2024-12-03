using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Control;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class RentalPlaceResource : EntityBaseResource
    {
        public RentalPlaceResource()
        {
            Floors = new HashSet<FloorResource>();
            //Properties = new HashSet<PropertyResource>();
        }

        //public int? Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string? Code { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int? GnDivisionId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        //public string? City { get; set; }
        //public string? Zip { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        public virtual OfficeResource? Office { get; set; }
        public virtual ICollection<FloorResource>? Floors { get; set; }
        //public virtual ICollection<PropertyResource>? Properties { get; set; }

        //public bool ServiceStatus { get; set; }
        //public string Message { get; set; }
    }
}
