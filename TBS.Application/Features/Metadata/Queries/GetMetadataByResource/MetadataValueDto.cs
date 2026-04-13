namespace TBS.Application.Features.Metadata.Queries.GetMetadataByResource;

public record MetadataValueDto(
    int Id,
    int PropertyId,
    string PropertyLabel,
    string TermUri,
    string Type,      
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    string? Language
);