namespace DLMS.Application.DTOs;

public class ValueDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string PropertyLabel { get; set; } = string.Empty;
    public string? ValueText { get; set; }
    public string? ValueUri { get; set; }
    public int? ValueResourceId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Language { get; set; }
}