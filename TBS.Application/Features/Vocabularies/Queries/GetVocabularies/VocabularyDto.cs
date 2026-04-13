namespace TBS.Application.Features.Vocabularies.Queries.GetVocabularies;

public record VocabularyDto(
    int Id,
    string Prefix,
    string NamespaceUri,
    string Label,
    int PropertyCount
);