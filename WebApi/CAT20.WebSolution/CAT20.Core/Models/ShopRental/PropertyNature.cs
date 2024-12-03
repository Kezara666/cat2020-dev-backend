using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.ShopRental
{
    public partial class PropertyNature
    {
        public PropertyNature()
        {
            Properties = new HashSet<Property>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
