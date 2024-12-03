using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class BusinessPlace
    {
        public int? Id { get; set; }
        public int? GnDivision { get; set; }
        public int? Ward { get; set; }
        public int? Street { get; set; }
        public string? AssessmentNo { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? SabhaId { get; set; }
        public int? BusinessId { get; set; }
        public int? PropertyOwnerId { get; set; }
        public int? RIUserId { get; set; }
    }
}