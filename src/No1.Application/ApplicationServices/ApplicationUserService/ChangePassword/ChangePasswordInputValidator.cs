using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Localization;
using No1.Validators;

namespace No1.ApplicationServices.ApplicationUserService.ChangePassword;

public class ChangePasswordInputValidator : AbstractValidator<ChangePasswordInput>
{
    private readonly IStringLocalizer<No1Resource> _l;

    public ChangePasswordInputValidator(IStringLocalizer<No1Resource> l)
    {
        _l = l;

        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidationRule(l));
    }
}