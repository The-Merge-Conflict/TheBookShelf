namespace TBS.Application.Features.ResourceTemplates.Queries.GetTemplateWithProperties;

public record ResourceTemplateDto(
    int Id,
    string Label,
    string? Description,
    IEnumerable<TemplatePropertyDto> Properties
);

public record TemplatePropertyDto(
    int PropertyId,
    string PropertyLabel,
    string TermUri,
    bool IsRequired,
    int DisplayOrder,
    string? AlternateLabel
);