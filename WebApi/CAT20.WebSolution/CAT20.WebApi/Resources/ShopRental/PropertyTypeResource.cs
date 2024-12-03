using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class PropertyTypeResource
    {
        public PropertyTypeResource()
        {
            //Properties = new HashSet<PropertyResource>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        //public virtual ICollection<PropertyResource> Properties { get; set; }
    }
}
