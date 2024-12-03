using System;
using System.Collections.Generic;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using CAT20.WebApi.Resources.Control;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class PropertyResource
    {
        public PropertyResource()
        {
            Shops = new HashSet<ShopResource>();
            //VoteAssignments = new HashSet<VoteAssignment>();
        }

        public int? Id { get; set; }
        public string? PropertyNo { get; set; }
        public int? Status { get; set; }  //0-not assigned,  1- assigned
        public int PropertyTypeId { get; set; }
       // public int RentalPlaceId { get; set; }
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
        //public virtual RentalPlaceResource? RentalPlace { get; set; }
        public virtual PropertyTypeResource? PropertyType { get; set; }
        public virtual PropertyNatureResource? PropertyNature { get; set; }

        public virtual ICollection<ShopResource>? Shops { get; set; } //Mapping 1(property): many (shop)

        public virtual ICollection<ShopRentalBalanceResource>? Balances { get; set; }

        //Mapping 1(shop): many (ShopRentalVoteAssign)
        public virtual ICollection<ShopRentalVoteAssignResource>? ShopRentalVoteAssigns { get; set; }
    }
}
