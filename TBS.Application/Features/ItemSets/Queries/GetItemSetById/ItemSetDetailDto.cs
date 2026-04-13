namespace TBS.Application.Features.ItemSets.Queries.GetItemSetById;

public record ItemSetDetailDto(
    int Id,
    string Title,
    string? Description,
    bool IsPublic,
    DateTime CreatedAt,
    string CreatedBy,
    int ItemCount
);