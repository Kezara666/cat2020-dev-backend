using CAT20.Core.Models.OnlienePayment;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class GnDivisions
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int OfficeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;

        //public virtual ICollection<Partner>? Partners { get; set; }
    }
}