using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class TaxType
    {
        public int ID { get; set; }
        public String Description { get; set; }
        public int IsMainTax { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}