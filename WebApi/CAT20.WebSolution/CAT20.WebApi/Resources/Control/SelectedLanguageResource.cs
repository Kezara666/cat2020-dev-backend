using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class SelectedLanguageResource
    {
        [Key]
        public int ID { get; set; }
        public int? LanguageId { get; set; }
        public int? SabhaID { get; set; }
        public int? Status { get; set; }
    }
}
