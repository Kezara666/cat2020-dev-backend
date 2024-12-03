using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class NumberSequence
    {
        public int? Id { get; set; }
        public int? OfficeId { get; set; }

        public int? CoreNumber { get; set; }
        public int? ApplicationNumber { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
