using CAT20.Core.Models.OnlienePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.OtherModule
{
    public class VendorResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NicNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? Email { get; set; }
        public sbyte? Active { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public sbyte? IsEditable { get; set; }
        //public int CreatedBy { get; set; }
        //public int UpdatedBy { get; set; }
        //public int GnDivisionId { get; set; }


        //public int? SabhaId { get; set; }
        //public sbyte? IsTempory { get; set; }
        ////public int? RIUserId { get; set; }
        //public int? IsBusinessOwner { get; set; }
        //public int? IsPropertyOwner { get; set; }

    }
}
