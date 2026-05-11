using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Properties.Queries.GetPropertiesByVocabulary;

public record GetPropertiesByVocabularyQuery(int VocabularyId) : IRequest<List<PropertyDto>>;