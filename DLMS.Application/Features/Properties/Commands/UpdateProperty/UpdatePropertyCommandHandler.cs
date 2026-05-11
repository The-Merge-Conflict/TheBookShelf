using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdatePropertyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        UpdatePropertyCommand request,
        CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Property), request.Id);

        property.LocalName = request.LocalName;
        property.Label = request.Label;
        property.TermUri = request.TermUri;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}