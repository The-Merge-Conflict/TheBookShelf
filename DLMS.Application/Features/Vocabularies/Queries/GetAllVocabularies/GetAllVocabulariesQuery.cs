using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;

namespace DLMS.Application.Features.Vocabularies.Queries.GetAllVocabularies;

public record GetAllVocabulariesQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null
) : IRequest<PaginatedResult<VocabularyDto>>;