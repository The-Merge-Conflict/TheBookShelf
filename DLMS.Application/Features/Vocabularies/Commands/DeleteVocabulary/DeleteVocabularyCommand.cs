using MediatR;

namespace DLMS.Application.Features.Vocabularies.Commands.DeleteVocabulary;

public record DeleteVocabularyCommand(int Id) : IRequest<Unit>;