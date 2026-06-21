using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Properties.Queries.GetAllProperties;

public record GetAllPropertiesQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null
) : IRequest<PaginatedResult<PropertyDto>>;