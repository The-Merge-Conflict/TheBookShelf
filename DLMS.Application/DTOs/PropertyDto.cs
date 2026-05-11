namespace DLMS.Application.DTOs;

public class PropertyDto
{
    public int Id { get; set; }
    public int VocabularyId { get; set; }
    public string VocabularyLabel { get; set; } = string.Empty;
    public string LocalName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string TermUri { get; set; } = string.Empty;
}