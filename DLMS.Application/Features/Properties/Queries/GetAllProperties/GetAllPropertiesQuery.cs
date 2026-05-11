using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Properties.Queries.GetAllProperties;

public record GetAllPropertiesQuery : IRequest<List<PropertyDto>>;