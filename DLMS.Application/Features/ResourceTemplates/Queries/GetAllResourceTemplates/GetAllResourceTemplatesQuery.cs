using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Queries.GetAllResourceTemplates;

public record GetAllResourceTemplatesQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null
) : IRequest<PaginatedResult<ResourceTemplateDto>>;