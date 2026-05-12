using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.ItemSets.Queries.GetItemSetById;

public record GetItemSetByIdQuery(int Id) : IRequest<ItemSetDto>;