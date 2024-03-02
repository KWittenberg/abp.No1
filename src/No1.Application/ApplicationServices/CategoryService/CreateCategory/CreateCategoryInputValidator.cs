using FluentValidation;
using Microsoft.Extensions.Localization;
using No1.Entities.CategoryAggregate;
using No1.Interfaces;
using No1.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace No1.ApplicationServices.CategoryService.CreateCategory;

public class CreateCategoryInputValidator : AbstractValidator<CreateCategoryInput>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStringLocalizer<No1Resource> _l;

    public CreateCategoryInputValidator(ICategoryRepository categoryRepository, IStringLocalizer<No1Resource> l)
    {
        _categoryRepository = categoryRepository;
        _l = l;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_l["Category:Error:Required:Name"])
            .MaximumLength(CategoryConsts.NameLength).WithMessage(_l["Category:Error:NameTooLong"]);

        RuleFor(x => x.Description)
            .MaximumLength(CategoryConsts.DescriptionLength).WithMessage(_l["Category:Error:NameTooLong"]);

        RuleFor(x => x)
            .MustAsync(NotAlreadyExistCategory).WithMessage(_l["Category:Error:CategoryExists"]);
    }

    private async Task<bool> NotAlreadyExistCategory(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(category => category.Name == input.Name, cancellationToken);

        return category is null;
    }
}