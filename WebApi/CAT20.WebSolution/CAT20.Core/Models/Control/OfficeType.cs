﻿using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class OfficeType
    {
        public OfficeType()
        {
            office = new HashSet<Office>();
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Office> office { get; set; }
    }
}