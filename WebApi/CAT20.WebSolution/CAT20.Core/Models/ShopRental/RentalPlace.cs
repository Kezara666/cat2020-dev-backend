using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.ShopRental
{
    public partial class RentalPlace : EntityBase
    {
        public RentalPlace()
        {
            Floors = new HashSet<Floor>();
            //Properties = new HashSet<Property>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                AuditReference = value;
            }
        }

        public int? Status { get; set; }
        public string? Code { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int? GnDivisionId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        //public string? City { get; set; }
        //public string? Zip { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public virtual Office? Office { get; set; }
        public virtual ICollection<Floor>? Floors { get; set; }
        //public virtual ICollection<Property>? Properties { get; set; }
        
    }
}
