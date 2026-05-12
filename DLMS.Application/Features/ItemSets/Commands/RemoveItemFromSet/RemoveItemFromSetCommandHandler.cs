using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Commands.RemoveItemFromSet;

public class RemoveItemFromSetCommandHandler : IRequestHandler<RemoveItemFromSetCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public RemoveItemFromSetCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        RemoveItemFromSetCommand request,
        CancellationToken cancellationToken)
    {
        var itemSet = await _context.ItemSets
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.ItemSetId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemSet), request.ItemSetId);

        var item = itemSet.Items.FirstOrDefault(i => i.Id == request.ItemId)
            ?? throw new NotFoundException(
                $"Item {request.ItemId} in ItemSet {request.ItemSetId}",
                request.ItemId);

        itemSet.Items.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}