using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Entities.CountryAggregate;
using No1.Interfaces;
using No1.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace No1.ApplicationServices.CountryService.CreateCountry;

public class CreateCountryInputValidator : AbstractValidator<CreateCountryInput>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IStringLocalizer<No1Resource> _l;

    public CreateCountryInputValidator(ICountryRepository countryRepository, IStringLocalizer<No1Resource> l)
    {
        _countryRepository = countryRepository;
        _l = l;

        RuleFor(x => x.Name).NotEmpty().WithMessage(_l["Country:Error:Required:Name"])
            .MaximumLength(CountryConsts.NameLength).WithMessage(_l["Country:Error:NameTooLong"]);

        RuleFor(x => x).MustAsync(NotAlreadyExistCountry).WithMessage(_l["Country:Error:CountryExists"]);
    }

    private async Task<bool> NotAlreadyExistCountry(CreateCountryInput input, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetAsync(country => country.Name == input.Name, cancellationToken);

        return country is null;
    }
}