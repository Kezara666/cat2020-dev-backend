using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class YearResource
    {
        [Key]
        public int ID { get; set; }
        public int Description { get; set; }
    }
}
