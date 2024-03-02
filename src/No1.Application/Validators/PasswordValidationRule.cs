using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Localization;
using Volo.Abp.Identity;

namespace No1.Validators;

public class PasswordValidationRule : AbstractValidator<string>
{
    private readonly IStringLocalizer<No1Resource> _l;

    public PasswordValidationRule(IStringLocalizer<No1Resource> l)
    {
        _l = l;

        RuleFor(x => x)
            .NotEmpty().WithMessage(_l["User:Error:Required:Password"])
            .MinimumLength(6).WithMessage(_l["User:Error:PasswordMinLength"])
            .MaximumLength(IdentityUserConsts.MaxPasswordLength).WithMessage(_l["User:Error:PasswordTooLong"])
            .Matches("[A-Z]").WithMessage(_l["User:Error:PasswordUppercase"])
            .Matches("[a-z]").WithMessage(_l["User:Error:PasswordLowercase"])
            .Matches("[0-9]").WithMessage(_l["User:Error:PasswordDigit"])
            .Matches(@"[\W_]").WithMessage(_l["User:Error:PasswordCharacter"]);
    }
}