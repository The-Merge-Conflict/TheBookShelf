using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Queries.GetAllResourceTemplates;

public record GetAllResourceTemplatesQuery : IRequest<List<ResourceTemplateDto>>;