using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Control
{
    public partial class DocumentSequenceNumber
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int OfficeId { get; set; }
        public string Prefix { get; set; }
        public int LastIndex { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}