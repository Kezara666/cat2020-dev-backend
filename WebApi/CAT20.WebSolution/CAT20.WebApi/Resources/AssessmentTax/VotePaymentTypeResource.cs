using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.ShopRental;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class VotePaymentTypeResource
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<AssessmentVoteAssignResource>? VoteAssigns { get; set; }

        //Mapping 1(VotePaymentType): many (ShopRentalVoteAssign)
        //public virtual ICollection<ShopRentalVoteAssign>? ShopRentalVoteAssigns { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
