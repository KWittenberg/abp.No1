using No1.ApplicationServices.CategoryService.CreateCategory;
using No1.ApplicationServices.CategoryService.UpdateCategory;
using No1.Entities.CategoryAggregate;
using No1.Interfaces;
using No1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;


namespace No1.ApplicationServices.CategoryService;

public class CategoryAppService : No1AppService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryAppService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IList<CategoryOutput>> GetCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return ObjectMapper.Map<IList<Category>, IList<CategoryOutput>>(categories);
    }

    public async Task<CategoryOutput> GetCategory(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(category => category.Id == categoryId);

        if (category is null)
        {
            throw new EntityNotFoundException(typeof(Category), categoryId);
        }

        return ObjectMapper.Map<Category, CategoryOutput>(category);
    }

    public virtual async Task<CategoryOutput> CreateCategory(CreateCategoryInput input)
    {
        var category = new Category(GuidGenerator.Create(), input.Name, input.Description);

        await _categoryRepository.InsertAsync(category);

        return ObjectMapper.Map<Category, CategoryOutput>(category);
    }

    public virtual async Task<CategoryOutput> UpdateCategory(UpdateCategoryInput input)
    {
        var category = await _categoryRepository.GetAsync(category => category.Id == input.Id);

        if (category is null)
        {
            throw new EntityNotFoundException(typeof(Category), input.Id);
        }

        category.SetName(input.Name);
        category.SetDescription(input.Description);

        await _categoryRepository.UpdateAsync(category);

        return ObjectMapper.Map<Category, CategoryOutput>(category);
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(category => category.Id == categoryId);

        if (category is null)
        {
            throw new EntityNotFoundException(typeof(Category), categoryId);
        }

        await _categoryRepository.DeleteAsync(category);
    }
}