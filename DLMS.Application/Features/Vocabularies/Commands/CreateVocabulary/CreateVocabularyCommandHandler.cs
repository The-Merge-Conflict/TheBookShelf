using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;

namespace DLMS.Application.Features.Vocabularies.Commands.CreateVocabulary;

public class CreateVocabularyCommandHandler : IRequestHandler<CreateVocabularyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateVocabularyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<int> Handle(
        CreateVocabularyCommand request,
        CancellationToken cancellationToken)
    {
        var vocabulary = new Vocabulary
        {
            Prefix = request.Prefix,
            NamespaceUri = request.NamespaceUri,
            Label = request.Label
        };

        _context.Vocabularies.Add(vocabulary);
        await _context.SaveChangesAsync(cancellationToken);

        return vocabulary.Id;
    }
}