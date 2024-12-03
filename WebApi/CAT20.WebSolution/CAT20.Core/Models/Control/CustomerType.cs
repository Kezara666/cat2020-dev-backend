using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class CustomerType
    {
        public int ID { get; set; }
        public string NameInSinhala { get; set; }
        public string NameInEnglish { get; set; }
        public string NameInTamil { get; set; }
        public int? Status { get; set; }
    }
}