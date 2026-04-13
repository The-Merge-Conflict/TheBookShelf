namespace TBS.Application.Features.Items.Queries.GetItemById;

public record ItemDetailDto(
    int Id,
    string CreatedBy,
    DateTime CreatedAt,
    string? ModifiedBy,
    DateTime? ModifiedAt,
    int? TemplateId,
    string? TemplateLabel,
    IEnumerable<MetadataValueDto> Values,
    IEnumerable<MediaDto> Media
);