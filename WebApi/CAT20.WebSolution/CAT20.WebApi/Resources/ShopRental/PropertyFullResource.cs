using System;
using System.Collections.Generic;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.ShopRental;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class PropertyFullResource
    {

        
        public PropertyFullResource()
        {
            Shops = new HashSet<ShopResource>();
        }
        

        public int? Id { get; set; }
        public string? PropertyNo { get; set; }
        public int? Status { get; set; }
        public int PropertyTypeId { get; set; }
        public int RentalPlaceId { get; set; }
        public int? FloorId { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int PropertyNatureId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual OfficeResource? Office { get; set; }
        public virtual FloorResource? Floor { get; set; }
        public virtual RentalPlaceResource? RentalPlace { get; set; }
        public virtual PropertyTypeResource? PropertyType { get; set; }
        public virtual PropertyNatureResource? PropertyNature { get; set; }

        public virtual ICollection<ShopResource>? Shops { get; set; }
    }
}
