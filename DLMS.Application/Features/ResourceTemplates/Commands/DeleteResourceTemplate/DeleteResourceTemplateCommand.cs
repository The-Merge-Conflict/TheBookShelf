using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.DeleteResourceTemplate;

public record DeleteResourceTemplateCommand(int Id) : IRequest<Unit>;