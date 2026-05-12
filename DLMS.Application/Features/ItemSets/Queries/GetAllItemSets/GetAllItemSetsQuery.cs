using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ItemSets.Queries.GetAllItemSets;

public record GetAllItemSetsQuery : IRequest<List<ItemSetDto>>;