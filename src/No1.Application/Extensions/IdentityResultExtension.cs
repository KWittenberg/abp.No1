using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.Validation;

namespace No1.Extensions;

public static class IdentityResultExtension
{
    public static void ThrowIfInvalid(this IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var validationResults = result.Errors.Select(error => new ValidationResult(error.Description)).ToList();

            //if (result.Errors.Any(error => error.Code == "YourErrorCode"))
            //{
            //    throw new EntityNotFoundException("Custom error message");
            //}

            throw new AbpValidationException(validationResults);
        }
    }
}