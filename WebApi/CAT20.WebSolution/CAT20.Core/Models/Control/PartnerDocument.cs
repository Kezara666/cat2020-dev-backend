using System.ComponentModel.DataAnnotations;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Models.Control;

public class PartnerDocument
{
    public int? Id { get; set; }
    public PartnerDocumentType DocumentType { get; set; }
    public string? Description { get; set; }
    public string FileName { get; set; }
    public int? PartnerId { get; set; }
    public Boolean Status { get; set; } = true;
    public virtual Partner? Partner { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}