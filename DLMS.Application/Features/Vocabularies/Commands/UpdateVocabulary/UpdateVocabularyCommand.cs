using MediatR;

namespace DLMS.Application.Features.Vocabularies.Commands.UpdateVocabulary;

public record UpdateVocabularyCommand(
    int Id,
    string Prefix,
    string NamespaceUri,
    string Label
) : IRequest<Unit>;