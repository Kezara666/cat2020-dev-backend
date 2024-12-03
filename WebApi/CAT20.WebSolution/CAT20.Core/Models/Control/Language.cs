using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class Language
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}