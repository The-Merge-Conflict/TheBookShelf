using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ItemSets.Queries.GetAllItemSets;

public record GetAllItemSetsQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null
) : IRequest<PaginatedResult<ItemSetDto>>;