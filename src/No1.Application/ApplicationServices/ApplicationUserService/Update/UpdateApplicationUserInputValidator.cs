using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Entities.ApplicationUserAggregate;
using No1.Localization;
using System;
using System.Text.RegularExpressions;
using Volo.Abp.Identity;

namespace No1.ApplicationServices.ApplicationUserService.Update;

public class UpdateApplicationUserInputValidator : AbstractValidator<UpdateApplicationUserInput>
{
    private const string LettersOnlyRegexPattern = "([^\\x00-\\x7F]|\\w)+";
    private const string DateRegexPattern = @"^\d{2}\.\d{2}\.\d{4}$";
    private const string PhoneNumberRegexPattern = @"^\+?\d{1,15}$";
    private readonly IStringLocalizer<No1Resource> _l;

    public UpdateApplicationUserInputValidator(IStringLocalizer<No1Resource> l)
    {
        _l = l;

        RuleFor(x => x.FirstName).NotEmpty().WithMessage(_l["User:Error:Required:FirstName"])
            .MaximumLength(IdentityUserConsts.MaxNameLength).WithMessage(_l["User:Error:FirstNameTooLong"])
            .Matches(LettersOnlyRegexPattern).WithMessage(_l["User:Error:FirstNameLettersOnly"]);

        RuleFor(x => x.LastName).NotEmpty().WithMessage(_l["User:Error:Required:LastName"])
            .MaximumLength(IdentityUserConsts.MaxSurnameLength).WithMessage(_l["User:Error:LastNameTooLong"])
            .Matches(LettersOnlyRegexPattern).WithMessage(_l["User:Error:LastNameLettersOnly"]);

        RuleFor(x => x.DateOfBirth)
            .Must(BeValidDate).WithMessage(_l["Date:Error:DateNotValid"]);

        RuleFor(x => x.PhoneNumber)
            .Matches(PhoneNumberRegexPattern).WithMessage(_l["User:Error:PhoneNumber"]);

        RuleFor(x => x.Country)
            .MaximumLength(ApplicationUserConsts.CountryLength).WithMessage(_l["Country:Error:NameTooLong"]);

        RuleFor(x => x.PostCode)
            .MaximumLength(ApplicationUserConsts.PostCodeLength).WithMessage(_l["PostCode:Error:TooLong"]);

        RuleFor(x => x.City)
            .MaximumLength(ApplicationUserConsts.CityLength).WithMessage(_l["City:Error:NameTooLong"]);

        RuleFor(x => x.Street)
            .MaximumLength(ApplicationUserConsts.CountryLength).WithMessage(_l["Street:Error:NameTooLong"]);
    }

    private bool BeValidDate(DateTime date)
    {
        var formattedDate = date.ToString("dd.MM.yyyy");

        return Regex.IsMatch(formattedDate, DateRegexPattern);
    }
}