using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.CreateResourceTemplate;

public record CreateResourceTemplateCommand(
    string Label,
    string Description
) : IRequest<int>;