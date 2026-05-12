using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Media.Queries.GetMediaByItem;

public record GetMediaByItemQuery(int ItemId) : IRequest<IReadOnlyList<MediaDto>>;
