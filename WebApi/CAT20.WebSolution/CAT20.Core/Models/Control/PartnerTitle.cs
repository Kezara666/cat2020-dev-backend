using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class PartnerTitle
    {
        public PartnerTitle()
        {
            //Partner = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //public virtual ICollection<Partner> Partner { get; set; }
    }
}