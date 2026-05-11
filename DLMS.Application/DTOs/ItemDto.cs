namespace DLMS.Application.DTOs;

public class ItemDto
{
    public int Id { get; set; }
    public int? TemplateId { get; set; }
    public string TemplateLabel { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public IEnumerable<ValueDto> Values { get; set; } = [];
    public IEnumerable<MediaDto> Medias { get; set; } = [];
}