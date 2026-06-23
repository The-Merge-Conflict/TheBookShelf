namespace DLMS.Application.DTOs;

public class ItemSummaryDto
{
    public int Id { get; set; }
    public int? TemplateId { get; set; }
    public string TemplateLabel { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
