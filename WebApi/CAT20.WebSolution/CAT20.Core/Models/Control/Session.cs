using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class Session
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public DateTime StartAt
        {
            get => _StartAt;
            set
            {
                _StartAt = value;
                SessionDate = DateOnly.FromDateTime(_StartAt);
            }
        }
        private DateTime _StartAt;

        public DateTime? StopAt { get; set; }
        public sbyte Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int OfficeId { get; set; }
        public int Rescue { get; set; }
        public DateTime? RescueStartedAt { get; set; }

        private DateOnly _SessionDate;
        public DateOnly SessionDate
        {
            get => _SessionDate;
            set => _SessionDate = value;
        }

        public Session()
        {
            SessionDate = DateOnly.FromDateTime(StartAt);
        }

        public void SetActiveFalse()
        {
            Active = 0;
        }
    }

}