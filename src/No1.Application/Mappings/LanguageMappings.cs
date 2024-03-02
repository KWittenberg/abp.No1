using AutoMapper;
using No1.Entities.LanguageAggregate;
using No1.Models;

namespace No1.Mappings;

public class LanguageMappings : Profile
{
    public LanguageMappings()
    {
        CreateMap<Language, LanguageOutput>();
    }
}