using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Vocabularies.Queries.GetVocabularyById;

public record GetVocabularyByIdQuery(int Id) : IRequest<VocabularyDto>;