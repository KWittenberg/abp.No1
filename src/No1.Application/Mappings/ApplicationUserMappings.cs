using AutoMapper;
using No1.Extensions;
using No1.Models;
using System.Linq;
using Volo.Abp.Identity;

namespace No1.Mappings;

public class ApplicationUserMappings : Profile
{
    public ApplicationUserMappings()
    {
        CreateMap<IdentityUser, ApplicationUserOutput>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Surname))
            .ForMember(d => d.AvatarUrl, o => o.MapFrom(s => s.GetAvatarUrl()))
            .ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.GetDateOfBirth()))
            .ForMember(d => d.Country, o => o.MapFrom(s => s.GetCountry()))
            .ForMember(d => d.PostCode, o => o.MapFrom(s => s.GetPostCode()))
            .ForMember(d => d.City, o => o.MapFrom(s => s.GetCity()))
            .ForMember(d => d.Street, o => o.MapFrom(s => s.GetStreet()))
            .ForMember(d => d.RoleIds, o => o.MapFrom(s => s.Roles.Select(r => r.RoleId)));

        CreateMap<IdentityRole, RoleOutput>();
    }
}