using AutoMapper;
using No1.Entities.CountryAggregate;
using No1.Models;

namespace No1.Mappings;

public class CountryMappings : Profile
{
    public CountryMappings()
    {
        CreateMap<Country, CountryOutput>();

        CreateMap<CountryTranslation, CountryTranslationOutput>();


        CreateMap<City, CityOutput>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y));

        CreateMap<CityTranslation, CityTranslationOutput>();


        CreateMap<City, CityTranslationOutput>();
    }
}