namespace TBS.Application.Features.ItemSets.Queries.GetItemSets;

public record ItemSetSummaryDto(
    int Id,
    string Title,
    bool IsPublic,
    int ItemCount
);