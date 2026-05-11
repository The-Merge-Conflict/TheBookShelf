namespace DLMS.Application.DTOs;

public class ResourceTemplateDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<TemplatePropertyDto> Properties { get; set; } = [];
}