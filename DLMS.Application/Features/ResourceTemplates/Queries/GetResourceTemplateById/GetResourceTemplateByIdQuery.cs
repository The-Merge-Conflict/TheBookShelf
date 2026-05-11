using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Queries.GetResourceTemplateById;

public record GetResourceTemplateByIdQuery(int Id) : IRequest<ResourceTemplateDto>;