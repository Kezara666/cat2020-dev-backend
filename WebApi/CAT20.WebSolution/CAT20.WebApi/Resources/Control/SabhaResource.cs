using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class SabhaResource
    {
        public int ID { get; set; }
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
        public virtual District District { get; set; }
        // public virtual Province Province { get; set; }
        //public virtual ICollection<Office> Offices { get; set; }
    }
}
