using No1.ApplicationServices.CategoryService.CreateCategory;
using System;

namespace No1.ApplicationServices.CategoryService.UpdateCategory;

public class UpdateCategoryInput : CreateCategoryInput
{
    public Guid Id { get; set; }
}