using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace No1.Web.Pages.Roles;

public class CreateModalModel : PageModel
{
    public CreateRoleViewModel Role { get; set; }

    private readonly IIdentityRoleRepository _roleRepository;

    public CreateModalModel(IIdentityRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task OnGetAsync()
    {
        Role = new CreateRoleViewModel();

        //var role = await _roleRepository.Get(RoleId);
    }


    //public async Task<IActionResult> OnPostAsync()
    //{
    //    await _roleRepository.CreateAsync(ObjectMapper.Map<CreateBookViewModel, CreateUpdateBookDto>(Book));
    //    return NoContent();
    //}

    public class CreateRoleViewModel
    {
        [SelectItems(nameof(Roles))]
        [DisplayName("Author")]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}