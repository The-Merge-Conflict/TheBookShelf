using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.RemovePropertyFromTemplate;

public record RemovePropertyFromTemplateCommand(
    int TemplateId,
    int PropertyId
) : IRequest<Unit>;