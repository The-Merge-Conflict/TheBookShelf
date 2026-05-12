using FluentValidation;

namespace DLMS.Application.Features.Media.Commands.UploadMedia;

public class UploadMediaCommandValidator : AbstractValidator<UploadMediaCommand>
{
    private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB

    private static readonly string[] AllowedMimeTypes =
    [
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/webp",
        "application/pdf"
    ];

    public UploadMediaCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .GreaterThan(0).WithMessage("ItemId must be a valid positive integer.");

        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(f => f.Length > 0).WithMessage("File must not be empty.")
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage($"File size must not exceed {MaxFileSizeBytes / (1024 * 1024)} MB.")
            .Must(f => AllowedMimeTypes.Contains(f.ContentType.ToLowerInvariant()))
                .WithMessage($"File type must be one of: {string.Join(", ", AllowedMimeTypes)}.");
    }
}
