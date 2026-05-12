using MediatR;
using Microsoft.AspNetCore.Http;

namespace DLMS.Application.Features.Media.Commands.UploadMedia;

public record UploadMediaCommand(
    int ItemId,
    IFormFile File,
    string? AltText
) : IRequest<int>;
