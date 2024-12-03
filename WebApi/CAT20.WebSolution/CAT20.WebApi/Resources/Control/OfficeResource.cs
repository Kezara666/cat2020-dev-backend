using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CAT20.Core.Models.Control;

namespace CAT20.WebApi.Resources.Control
{
    public partial class OfficeResource
    {
        public int ID { get; set; }
        public int? SabhaID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? OfficeTypeID { get; set; }
        public int? Status { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }

        //public virtual Sabha sabha { get; set; }
        //public virtual OfficeType officeType { get; set; }
    }
}