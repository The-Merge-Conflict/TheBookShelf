using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.CreateResourceTemplate;

public class CreateResourceTemplateCommandHandler
    : IRequestHandler<CreateResourceTemplateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateResourceTemplateCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<int> Handle(
        CreateResourceTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var template = new ResourceTemplate
        {
            Label = request.Label,
            Description = request.Description
        };

        _context.ResourceTemplates.Add(template);
        await _context.SaveChangesAsync(cancellationToken);

        return template.Id;
    }
}