using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class District
    {
        public District()
        {
            sabha = new HashSet<Sabha>();
        }

        public int ID { get; set; }
        public int ProvinceID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Province province { get; set; }
        public virtual ICollection<Sabha> sabha { get; set; }
    }
}