using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class SessionResource
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? StopAt { get; set; }
        public sbyte Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int OfficeId { get; set; }
        public int Rescue { get; set; }
        public DateTime? RescueStartedAt { get; set; }
        //public DateOnly? SessionDate { get; set; }
    }
}