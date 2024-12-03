using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Models.ShopRental
{
    public partial class Property
    {
        public Property()
        {
            Shops = new HashSet<Shop>();
            OpeningBalances = new HashSet<OpeningBalance>();

            //---------------
            Balances = new HashSet<ShopRentalBalance>();
            ShopRentalVoteAssigns = new HashSet<ShopRentalVoteAssign>();
            //---------------
        }

        public int? Id { get; set; }
        public string? PropertyNo { get; set; }
        public int? Status { get; set; } 
        public int PropertyTypeId { get; set; }
        //public int RentalPlaceId { get; set; }
        public int? FloorId { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int PropertyNatureId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Office? Office { get; set; }
        public virtual Floor? Floor { get; set; }
        //public virtual RentalPlace? RentalPlace { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public virtual PropertyNature? PropertyNature { get; set; }

        //Mapping 1(property): many (shop)
        public virtual ICollection<Shop> Shops { get; set; }

        [JsonIgnore]
        public virtual ICollection<OpeningBalance> OpeningBalances { get; set; } //Mapping 1(Property): many (OpeningBalance)

        //----
        //Mapping 1(property): many (balances)
        [JsonIgnore]
        public virtual ICollection<ShopRentalBalance>? Balances { get; set; }


        //Mapping 1(property): many (ShopRentalVoteAssign)
        [JsonIgnore]
        public virtual ICollection<ShopRentalVoteAssign>? ShopRentalVoteAssigns { get; set; }
        //----


        //Mapping 1(property): many (ShopRentalRecievableIncomeVoteAssign)
        [JsonIgnore]
        public virtual ICollection<ShopRentalRecievableIncomeVoteAssign>? ShopRentalRecievableIncomeVoteAssigns { get; set; }
        //----
    }
}
