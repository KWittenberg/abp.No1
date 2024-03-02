using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using IdentityRole = Volo.Abp.Identity.IdentityRole;

namespace No1.Web.Pages.Roles;

public class IndexModel : PageModel
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IdentityRoleManager _identityRoleManager;

    public IndexModel(IdentityRoleManager identityRoleManager, IGuidGenerator guidGenerator)
    {
        _identityRoleManager = identityRoleManager;
        _guidGenerator = guidGenerator;
    }

    [BindProperty]
    public string Name { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var roles = new List<IdentityRole>();

        var addRole = await _identityRoleManager.FindByNameAsync(Name);

        if (addRole is null)
        {
            var role = new IdentityRole(_guidGenerator.Create(), Name)
            {
                IsStatic = false,
                IsPublic = true
            };

            var result = await _identityRoleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["success"] = "Role created successfully!";
            }

        }
        else
        {
            TempData["error"] = "Role exist!";
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostEdit(string id)
    {
        var existingRole = await _identityRoleManager.FindByIdAsync(id);

        if (existingRole == null)
        {
            TempData["error"] = "Role not found!";
            return RedirectToPage("Index");
        }

        if (!ModelState.IsValid)
        {
            TempData["error"] = "Invalid input!";
            return RedirectToPage("Index");
        }

        //existingRole.Name = Name; // Postavite novo ime uloge
        var result = await _identityRoleManager.UpdateAsync(existingRole);

        if (result.Succeeded)
        {
            TempData["success"] = "Role updated successfully!";
        }
        else
        {
            TempData["error"] = "Failed to update role!";
        }

        return RedirectToPage("Index");
    }
}