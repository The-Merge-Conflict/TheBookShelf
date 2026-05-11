using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Commands.DeleteResourceTemplate;

public class DeleteResourceTemplateCommandHandler
    : IRequestHandler<DeleteResourceTemplateCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteResourceTemplateCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        DeleteResourceTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var template = await _context.ResourceTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ResourceTemplate), request.Id);

        _context.ResourceTemplates.Remove(template);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}