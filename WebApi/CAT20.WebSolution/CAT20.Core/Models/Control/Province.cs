using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class Province
    {
        public Province()
        {
            district = new HashSet<District>();
        }

        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<District>  district { get; set; }
    }
}