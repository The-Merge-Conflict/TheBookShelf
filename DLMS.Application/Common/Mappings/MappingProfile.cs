using AutoMapper;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;

namespace DLMS.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Vocabulary
        CreateMap<Vocabulary, VocabularyDto>();

        // Property
        CreateMap<Property, PropertyDto>()
            .ForMember(d => d.VocabularyLabel, o => o.MapFrom(s => s.Vocabulary.Label));

        // TemplateProperty
        CreateMap<TemplateProperty, TemplatePropertyDto>()
            .ForMember(d => d.PropertyLabel, o => o.MapFrom(s => s.Property.Label))
            .ForMember(d => d.TermUri, o => o.MapFrom(s => s.Property.TermUri))
            .ForMember(d => d.PropertyId, o => o.MapFrom(s => s.PropertyId));

        // ResourceTemplate
        CreateMap<ResourceTemplate, ResourceTemplateDto>()
            .ForMember(d => d.Properties, o => o.MapFrom(s => s.TemplateProperties));

        // Value
        CreateMap<Value, ValueDto>()
            .ForMember(d => d.PropertyLabel, o => o.MapFrom(s => s.Property.Label))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.ValueType.ToString()))
            .ForMember(d => d.Language, o => o.MapFrom(s =>
                s.Language != null ? s.Language.Code : null));

        // Media
        CreateMap<Media, MediaDto>()
            .ForMember(d => d.MimeType, o => o.MapFrom(s => s.MimeType.Value))
            .ForMember(d => d.FileSize, o => o.MapFrom(s => s.FileSize.Bytes));

        // Item
        CreateMap<Item, ItemDto>()
            .ForMember(d => d.TemplateLabel, o => o.MapFrom(s =>
                s.Template != null ? s.Template.Label : string.Empty))
            .ForMember(d => d.Medias, o => o.MapFrom(s => s.MediaList));

        // ItemSet
        CreateMap<ItemSet, ItemSetDto>();
    }
}