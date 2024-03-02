using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using No1.ApplicationServices.CategoryService;
using No1.ApplicationServices.CategoryService.CreateCategory;
using System.Threading.Tasks;

namespace No1.Web.Pages.Categories;

public class CreateModalModel : PageModel
{
    private readonly CategoryAppService _categoryAppService;

    public CreateModalModel(CategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }


    [BindProperty]
    public CreateCategoryInput CreateCategory { get; set; }

    public void OnGet()
    {
        CreateCategory = new CreateCategoryInput();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _categoryAppService.CreateCategory(CreateCategory);
            //TempData["success"] = $"{CreateCategory.Name} created successfully!";
        }
        else
        {
            //TempData["error"] = $"{CreateCategory.Name} failed to create!";
        }
    }
}