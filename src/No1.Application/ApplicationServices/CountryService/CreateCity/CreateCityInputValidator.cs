using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Entities.CountryAggregate;
using No1.Interfaces;
using No1.Localization;
using Volo.Abp.Domain.Entities;

namespace No1.ApplicationServices.CountryService.CreateCity;

public class CreateCityInputValidator : AbstractValidator<CreateCityInput>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IStringLocalizer<No1Resource> _l;

    public CreateCityInputValidator(ICountryRepository countryRepository, IStringLocalizer<No1Resource> l)
    {
        _countryRepository = countryRepository;
        _l = l;

        RuleFor(x => x.Name).NotEmpty().WithMessage(_l["City:Error:Required:Name"])
            .MaximumLength(CityConsts.NameLength).WithMessage(_l["City:Error:NameTooLong"]);

        RuleFor(x => x).MustAsync(NotAlreadyExistCity).WithMessage(_l["City:Error:CityExists"]);
    }

    private async Task<bool> NotAlreadyExistCity(CreateCityInput input, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetAsync(country => country.Id == input.CountryId, cancellationToken);

        if (country is null)
        {
            throw new EntityNotFoundException(typeof(Country), input.CountryId);
        }

        var city = country.Cities.FirstOrDefault(city => city.Name == input.Name);

        return city is null;
    }
}