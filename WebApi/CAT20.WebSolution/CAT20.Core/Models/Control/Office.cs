using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class Office
    {
        public int? ID { get; set; }
        public int? SabhaID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int OfficeTypeID { get; set; }
        public int? Status { get; set; }
        public string Code { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Sabha? sabha { get; set; }
        public virtual OfficeType? officeType { get; set; }
     //   public virtual Sabha sabha { get; set; }
       // public virtual OfficeType officeType { get; set; }

        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}