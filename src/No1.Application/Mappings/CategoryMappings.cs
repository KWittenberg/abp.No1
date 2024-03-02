using AutoMapper;
using No1.Entities.CategoryAggregate;
using No1.Models;

namespace No1.Mappings;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<Category, CategoryOutput>();

        // CreateMap<CategoryTranslation, CategoryTranslationOutput>();
    }
}