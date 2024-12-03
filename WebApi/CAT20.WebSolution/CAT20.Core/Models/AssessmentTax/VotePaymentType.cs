using CAT20.Core.Models.ShopRental;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class VotePaymentType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Description { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<AssessmentVoteAssign>? VoteAssigns { get; set; }

        //Mapping 1(VotePaymentType): many (ShopRentalVoteAssign)
        //[JsonIgnore]
        //public virtual ICollection<ShopRentalVoteAssign>? ShopRentalVoteAssigns { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
