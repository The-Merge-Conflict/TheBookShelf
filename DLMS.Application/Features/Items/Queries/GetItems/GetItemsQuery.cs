using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Items.Queries.GetItems;

public record GetItemsQuery(
    int Page = 1,
    int PageSize = 10,
    int? TemplateId = null
) : IRequest<PaginatedResult<ItemDto>>;