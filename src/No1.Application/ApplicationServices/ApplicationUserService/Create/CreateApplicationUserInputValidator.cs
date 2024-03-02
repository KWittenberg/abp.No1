using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;
using System;
using No1.Entities.ApplicationUserAggregate;
using No1.Localization;
using Volo.Abp.Identity;
using No1.Validators;

namespace No1.ApplicationServices.ApplicationUserService.Create;

public class CreateApplicationUserInputValidator : AbstractValidator<CreateApplicationUserInput>
{
    private const string LettersOnlyRegexPattern = "([^\\x00-\\x7F]|\\w)+";
    private const string DateRegexPattern = @"^\d{2}\.\d{2}\.\d{4}$";
    private const string PhoneNumberRegexPattern = @"^\+?\d{1,15}$";
    private readonly IStringLocalizer<No1Resource> _l;

    public CreateApplicationUserInputValidator(IStringLocalizer<No1Resource> l)
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

        RuleFor(x => x.Email).NotEmpty().WithMessage(_l["User:Error:Required:Email"])
            .EmailAddress().WithMessage(_l["User:Error:Email"]).MaximumLength(IdentityUserConsts.MaxEmailLength);

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

        RuleFor(x => x.Password).SetValidator(new PasswordValidationRule(l));
    }

    private bool BeValidDate(DateTime date)
    {
        var formattedDate = date.ToString("dd.MM.yyyy");

        return Regex.IsMatch(formattedDate, DateRegexPattern);
    }
}