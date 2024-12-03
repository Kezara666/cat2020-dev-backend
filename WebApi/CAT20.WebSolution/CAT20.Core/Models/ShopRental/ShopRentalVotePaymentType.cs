using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalVotePaymentType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Description { get; set; }

        // Navigation property to represent the one-to-many relationship
        //[JsonIgnore]
        //public virtual ICollection<ShopRentalVoteAssign>? VoteAssigns { get; set; }

        // mandatory fields
        public int? Status { get; set; }


        //Last Year Fine Vote           -1
        //Last Year Arreas Vote         -2
        //This Year Fine Vote           -3
        //This Year Arreas Vote         -4
        //Shop Renal Vote               -5
        //S Charge Arreas Vote          -6
        //Service Charge Vote           -7
        //Overpayment Vote              -8
    }
}
