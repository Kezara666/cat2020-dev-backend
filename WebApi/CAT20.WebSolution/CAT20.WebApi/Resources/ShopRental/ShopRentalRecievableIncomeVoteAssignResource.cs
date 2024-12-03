using CAT20.Core.Models.ShopRental;

namespace CAT20.WebApi.Resources.ShopRental
{
    public class ShopRentalRecievableIncomeVoteAssignResource
    {
        public int? Id { get; set; }


        public int? PropertyId { get; set; } //FK


        public int ShopId { get; set; } //FK

        //---------------- [Start - vote detail id] -------------------------
        public int PropertyRentalIncomeVoteId { get; set; }

        public int PropertyServiceChargeIncomeVoteId { get; set; }

        public int PropertyFineIncomeVoteId { get; set; }
        //---------------- [End - vote detail id] -------------------------


        //Mapping 1(property): many (ShopRentalRecievableIncomeVoteAssign)
        public virtual BasicPropertyResource? Property { get; set; }

        //Mapping 1(Shop): 1 (ShopRentalRecievableIncomeVoteAssign)
        public virtual BasicShopResource? Shop { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
