using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CAT20.Core.Models.Control
{
    public partial class Sabha
    {
        public Sabha()
        {
           office = new HashSet<Office>();
        }

        public int? ID { get; set; }
        public int? DistrictID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public string LogoPath { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string AddressSinhala { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressTamil { get; set; }
        public int? AccountSystemVersionId { get; set; }
        public int? IsFinalAccountsEnabled { get; set; }
        public int? ChartOfAccountVersionId { get; set; }
        public int? SabhaType { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public virtual District? district { get; set; }
        public virtual ICollection<Office>? office { get; set; }

        // public virtual Province Province { get; set; }

        public string? SecretarySignPath { get; set; }
        public string? ChairmanSignPath { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}