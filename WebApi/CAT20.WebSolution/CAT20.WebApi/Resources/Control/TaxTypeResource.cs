using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class TaxTypeResource
    {
        [Key]
        public int ID { get; set; }
        public String Description { get; set; }
        public int IsMainTax { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
