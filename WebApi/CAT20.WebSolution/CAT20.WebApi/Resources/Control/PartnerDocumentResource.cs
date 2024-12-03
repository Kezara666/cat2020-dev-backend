using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Resources.Control;

public class PartnerDocumentResource
{
    public int? Id { get; set; }
    public PartnerDocumentType DocumentType { get; set; }
    public string? Description { get; set; }
    public string FileName { get; set; }
    public int? PartnerId { get; set; }
    //public virtual Partner? Partner { get; set; }
}