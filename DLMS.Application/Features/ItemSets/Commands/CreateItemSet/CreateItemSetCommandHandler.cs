using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.CreateItemSet;

public class CreateItemSetCommandHandler : IRequestHandler<CreateItemSetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateItemSetCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(
        CreateItemSetCommand request,
        CancellationToken cancellationToken)
    {
        var itemSet = new ItemSet
        {
            Title = request.Title,
            Description = request.Description,
            IsPublic = request.IsPublic,
            OwnerId = _currentUser.UserId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUser.UserName ?? "system"
        };

        _context.ItemSets.Add(itemSet);
        await _context.SaveChangesAsync(cancellationToken);

        return itemSet.Id;
    }
}