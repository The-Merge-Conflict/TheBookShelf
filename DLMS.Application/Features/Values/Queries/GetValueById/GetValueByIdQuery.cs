using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Values.Queries.GetValueById;

public record GetValueByIdQuery(int Id) : IRequest<ValueDto>;
