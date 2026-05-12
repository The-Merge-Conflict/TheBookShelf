using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Enums;
using DLMS.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Media.Commands.UploadMedia;

public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileStorageService _fileStorage;
    private readonly IMediaProcessingService _mediaProcessing;
    private readonly ICurrentUserService _currentUser;

    public UploadMediaCommandHandler(
        IApplicationDbContext context,
        IFileStorageService fileStorage,
        IMediaProcessingService mediaProcessing,
        ICurrentUserService currentUser)
    {
        _context = context;
        _fileStorage = fileStorage;
        _mediaProcessing = mediaProcessing;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(
        UploadMediaCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Verify the parent Item exists
        var itemExists = await _context.Items
            .AnyAsync(i => i.Id == request.ItemId, cancellationToken);

        if (!itemExists)
            throw new NotFoundException(nameof(Domain.Entities.Item), request.ItemId);

        // 2. Save the file to disk
        var storagePath = await _fileStorage.SaveFileAsync(request.File, "media");

        // 3. Extract metadata
        var mimeType = _mediaProcessing.GetMimeType(request.File);
        var fileSize = _mediaProcessing.GetFileSize(request.File);

        // 4. Build the Media entity
        var media = new Domain.Entities.Media
        {
            ItemId = request.ItemId,
            StoragePath = storagePath,
            FileName = request.File.FileName,
            AltText = request.AltText,
            MimeType = MimeType.Create(mimeType),
            FileSize = FileSize.Create(fileSize),
            OwnerId = _currentUser.UserId,
            CreatedBy = _currentUser.UserName ?? "system",
            CreatedAt = DateTime.UtcNow
        };

        // 5. Persist
        _context.Media.Add(media);
        await _context.SaveChangesAsync(cancellationToken);

        return media.Id;
    }
}
