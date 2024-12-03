using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class GnDivisionsResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int OfficeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
    }
}