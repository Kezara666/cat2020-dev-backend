using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
   public class OBLIApprovalStatus
    {
        [Required]
        public int? Id { get; set; }    
        [Required]
        public int? OpenBalStatus { get; set; }

        public int? OpnBalInfoId { get; set; }
        public virtual OpeningBalanceInformation? OpeningBalanceInformation { get; set; }
        public string? Comment { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
