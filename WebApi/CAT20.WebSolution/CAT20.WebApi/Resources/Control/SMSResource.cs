using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class SMSResource
    {
        public String MobileNo { get; set; }
        public string Text { get; set; }
        public int SabhaId { get; set; }

        public string? Module { get; set; }
        public string? Subject { get; set; }
    }
}
