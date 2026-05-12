using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Items.Queries.GetItemById;

public record GetItemByIdQuery(int Id) : IRequest<ItemDto>;