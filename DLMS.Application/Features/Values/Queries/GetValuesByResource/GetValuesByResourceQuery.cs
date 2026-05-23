using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Values.Queries.GetValuesByResource;

public record GetValuesByResourceQuery(int ResourceId) : IRequest<List<ValueDto>>;
