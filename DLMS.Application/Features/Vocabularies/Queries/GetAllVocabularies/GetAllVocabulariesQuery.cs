using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Vocabularies.Queries.GetAllVocabularies;

public record GetAllVocabulariesQuery : IRequest<List<VocabularyDto>>;