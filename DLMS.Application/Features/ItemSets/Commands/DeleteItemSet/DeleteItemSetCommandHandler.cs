using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Commands.DeleteItemSet;

public class DeleteItemSetCommandHandler : IRequestHandler<DeleteItemSetCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteItemSetCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        DeleteItemSetCommand request,
        CancellationToken cancellationToken)
    {
        var itemSet = await _context.ItemSets
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemSet), request.Id);

        _context.ItemSets.Remove(itemSet);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}