using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Properties.Queries.GetPropertyById;

public record GetPropertyByIdQuery(int Id) : IRequest<PropertyDto>;