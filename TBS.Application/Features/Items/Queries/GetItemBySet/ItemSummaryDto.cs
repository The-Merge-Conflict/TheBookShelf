namespace TBS.Application.Features.Items.Queries.GetItemsBySet;

public record ItemSummaryDto(
    int Id,
    string? TemplateLabel,
    string? Title,
    DateTime CreatedAt
);