using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Media.Commands.DeleteMedia;

public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public DeleteMediaCommandHandler(
        IApplicationDbContext context,
        IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<Unit> Handle(
        DeleteMediaCommand request,
        CancellationToken cancellationToken)
    {
        var media = await _context.Media
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Media), request.Id);

        // Delete the physical file from disk
        _fileStorage.DeleteFile(media.StoragePath);

        // Remove the entity
        _context.Media.Remove(media);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
