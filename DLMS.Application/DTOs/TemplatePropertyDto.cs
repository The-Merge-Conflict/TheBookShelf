namespace DLMS.Application.DTOs;

public class TemplatePropertyDto
{
    public int PropertyId { get; set; }
    public string PropertyLabel { get; set; } = string.Empty;
    public string TermUri { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public int DisplayOrder { get; set; }
    public string AlternateLabel { get; set; } = string.Empty;
}