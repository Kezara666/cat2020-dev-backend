using System;

namespace CAT20.Core.Models.Interfaces
{
    public interface IEntityBase
    {
        int? Id { get; set; }
        int? CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? TimeStamp { get; set; }
        State State { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        string AuditReference { get; set; }
    }

    public enum State
    {
        Added,
        Unchanged,
        Modified,
        Deleted
    }
}
