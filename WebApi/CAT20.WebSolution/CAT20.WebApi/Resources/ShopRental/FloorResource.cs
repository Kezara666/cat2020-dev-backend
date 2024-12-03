using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class FloorResource
    {
        public FloorResource()
        {
            //Properties = new HashSet<Property>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public int? Status { get; set; }
        public int RentalPlaceId { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //public virtual RentalPlace RentalPlace { get; set; }
        public virtual ICollection<PropertyResource>? Properties { get; set; }
    }
}
