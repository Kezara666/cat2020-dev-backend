using CAT20.Core.Models.WaterBilling;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class PaymentCategoryResource
    {

        public int? Id { get; set; }

        public string? Description { get; set; }


        // Navigation property to represent the one-to-many relationship
        //public virtual ICollection<VoteAssignResource>? VoteAssigns { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
    }
}
