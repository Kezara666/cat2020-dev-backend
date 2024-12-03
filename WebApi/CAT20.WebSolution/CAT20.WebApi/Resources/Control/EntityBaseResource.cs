using CAT20.Core.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.WebApi.Resources.Control
{
    public abstract class EntityBaseResource 
    {
        private State _state = State.Unchanged;

        public int? Id { get; set; }
        //[JsonIgnore]
        //public DateTime? TimeStamp { get; set; }
        //[JsonIgnore]
        //public State State { get { return _state; } set { _state = value; } }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //[JsonIgnore]
        //public DateTime? UpdatedAt { get; set; }
        //public string AuditReference { get; set; }

        //public bool ServiceStatus { get; set; }
        //public string Message { get; set; }
    }
}
