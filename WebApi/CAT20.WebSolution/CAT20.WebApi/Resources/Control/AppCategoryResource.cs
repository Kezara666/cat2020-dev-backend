using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class AppCategoryResource
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
