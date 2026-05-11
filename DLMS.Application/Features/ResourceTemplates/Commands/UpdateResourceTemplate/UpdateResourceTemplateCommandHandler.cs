using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateResourceTemplate;

public class UpdateResourceTemplateCommandHandler
    : IRequestHandler<UpdateResourceTemplateCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateResourceTemplateCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        UpdateResourceTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var template = await _context.ResourceTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ResourceTemplate), request.Id);

        template.Label = request.Label;
        template.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}