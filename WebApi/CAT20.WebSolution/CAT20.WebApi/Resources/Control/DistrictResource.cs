using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class DistrictResource
    {
        //public District()
        //{
        //    Sabhas = new HashSet<Sabha>();
        //}

        public int ID { get; set; }
        public int ProvinceID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? Status { get; set; }

        //public virtual Province province { get; set; }
        //public virtual ICollection<Sabha> Sabhas { get; set; }
    }
}
