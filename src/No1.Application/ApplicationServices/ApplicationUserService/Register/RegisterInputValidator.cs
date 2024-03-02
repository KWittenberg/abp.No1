using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Localization;
using No1.Validators;
using Volo.Abp.Identity;

namespace No1.ApplicationServices.ApplicationUserService.Register;

public class RegisterInputValidator : AbstractValidator<RegisterInput>
{
    private const string LettersOnlyRegexPattern = "([^\\x00-\\x7F]|\\w)+";
    private readonly IStringLocalizer<No1Resource> _l;

    public RegisterInputValidator(IStringLocalizer<No1Resource> l)
    {
        _l = l;

        RuleFor(x => x.FirstName).NotEmpty().WithMessage(_l["User:Error:Required:FirstName"])
            .MaximumLength(IdentityUserConsts.MaxNameLength).WithMessage(_l["User:Error:FirstNameTooLong"])
            .Matches(LettersOnlyRegexPattern).WithMessage(_l["User:Error:FirstNameLettersOnly"]);

        RuleFor(x => x.LastName).NotEmpty().WithMessage(_l["User:Error:Required:LastName"])
            .MaximumLength(IdentityUserConsts.MaxSurnameLength).WithMessage(_l["User:Error:LastNameTooLong"])
            .Matches(LettersOnlyRegexPattern).WithMessage(_l["User:Error:LastNameLettersOnly"]);

        RuleFor(x => x.Email).NotEmpty().WithMessage(_l["User:Error:Required:Email"])
            .EmailAddress().WithMessage(_l["User:Error:Email"]).MaximumLength(IdentityUserConsts.MaxEmailLength);

        RuleFor(x => x.Password).SetValidator(new PasswordValidationRule(l));
    }
}