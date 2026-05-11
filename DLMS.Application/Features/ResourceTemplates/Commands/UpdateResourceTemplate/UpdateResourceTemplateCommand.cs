using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateResourceTemplate;

public record UpdateResourceTemplateCommand(
    int Id,
    string Label,
    string Description
) : IRequest<Unit>;