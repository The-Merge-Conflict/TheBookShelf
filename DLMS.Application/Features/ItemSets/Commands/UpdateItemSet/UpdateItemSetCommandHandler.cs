using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Commands.UpdateItemSet;

public class UpdateItemSetCommandHandler : IRequestHandler<UpdateItemSetCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateItemSetCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(
        UpdateItemSetCommand request,
        CancellationToken cancellationToken)
    {
        var itemSet = await _context.ItemSets
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemSet), request.Id);

        itemSet.Title = request.Title;
        itemSet.Description = request.Description;
        itemSet.IsPublic = request.IsPublic;
        itemSet.ModifiedAt = DateTime.UtcNow;
        itemSet.ModifiedBy = _currentUser.UserName ?? "system";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}