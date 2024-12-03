using CAT20.WebApi.Resources.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterConnectionNatureLogResource
    {
        public int? Id { get; set; }

        public int? connectionId { get; set; }

        public int? NatureId { get; set; }

        public virtual WaterProjectNatureResource? Nature { get; set; }

        public string? Comment { get; set; }
        public int? ActionBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }

        public string? ActionNote { get; set; }
        public DateTime? ActivatedDate { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
