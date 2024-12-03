using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class PaymentCategory
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Description { get; set; }

        [Required] 
        public int? SabhaId { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<VoteAssign>? VoteAssigns { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
