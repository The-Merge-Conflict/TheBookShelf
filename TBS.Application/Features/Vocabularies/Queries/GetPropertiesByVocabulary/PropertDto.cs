namespace TBS.Application.Features.Vocabularies.Queries.GetPropertiesByVocabulary;

public record PropertyDto(
    int Id,
    string LocalName,
    string Label,
    string TermUri,
    int VocabularyId,
    string VocabularyPrefix
);