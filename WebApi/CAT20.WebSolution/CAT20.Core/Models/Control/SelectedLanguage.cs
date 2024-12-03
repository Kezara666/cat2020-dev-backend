using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class SelectedLanguage
    {
        public int ID { get; set; }
        public int? LanguageID { get; set; }
        public int? SabhaID { get; set; }
        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}