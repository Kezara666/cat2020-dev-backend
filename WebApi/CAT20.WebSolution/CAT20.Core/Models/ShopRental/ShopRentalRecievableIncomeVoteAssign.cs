using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalRecievableIncomeVoteAssign
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? PropertyId { get; set; } //FK

        [Required]
        public int ShopId { get; set; } //FK

        //---------------- [Start - vote detail id] --------------
        [Required]
        public int PropertyRentalIncomeVoteId { get; set; }

        [Required]
        public int PropertyServiceChargeIncomeVoteId { get; set; }

        [Required]
        public int PropertyFineIncomeVoteId { get; set; }
        //---------------- [End - vote detail id] -----------------


        //Mapping 1(property): many (ShopRentalRecievableIncomeVoteAssign)
        public virtual Property? Property { get; set; }

        //Mapping 1(Shop): 1 (ShopRentalRecievableIncomeVoteAssign)
        public virtual Shop? Shop { get; set; }

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
