using AutoMapper;
using No1.Models;
using System.Linq;

namespace No1.Mappings;

public class OpenWeatherMappings : Profile
{
    public OpenWeatherMappings()
    {
        // CreateMap<OpenWeatherModel, OpenWeatherOutput>();
        
        CreateMap<OpenWeatherModel, OpenWeatherOutput>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Weather.FirstOrDefault()!.Main))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Weather.FirstOrDefault()!.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Weather.FirstOrDefault()!.Icon))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coord.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coord.Lon))
            .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Timezone))
            .ForMember(dest => dest.Cod, opt => opt.MapFrom(src => src.Cod))
            .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main.Temp))
            .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.Main.FeelsLike))
            .ForMember(dest => dest.TempMin, opt => opt.MapFrom(src => src.Main.TempMin))
            .ForMember(dest => dest.TempMax, opt => opt.MapFrom(src => src.Main.TempMax))
            .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
            .ForMember(dest => dest.SeaLevel, opt => opt.MapFrom(src => src.Main.SeaLevel))
            .ForMember(dest => dest.GrndLevel, opt => opt.MapFrom(src => src.Main.GrndLevel))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind.Speed))
            .ForMember(dest => dest.WindDeg, opt => opt.MapFrom(src => src.Wind.Deg))
            .ForMember(dest => dest.WindGust, opt => opt.MapFrom(src => src.Wind.Gust));
    }
}