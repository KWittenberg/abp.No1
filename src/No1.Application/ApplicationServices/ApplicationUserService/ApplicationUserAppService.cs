using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using No1.ApplicationServices.ApplicationUserService.ChangePassword;
using No1.ApplicationServices.ApplicationUserService.Create;
using No1.ApplicationServices.ApplicationUserService.Register;
using No1.ApplicationServices.ApplicationUserService.Update;
using No1.ApplicationServices.ApplicationUserService.UpdateCurrentUser;
using No1.Entities.ApplicationUserAggregate;
using No1.Exceptions;
using No1.Extensions;
using No1.Models;
using No1.Options;
using No1.Role;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace No1.ApplicationServices.ApplicationUserService;

// [Authorize]
public class ApplicationUserAppService : No1AppService
{
    private readonly IdentityUserManager _identityUserManager;
    private readonly IdentityRoleManager _identityRoleManager;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly AppOptions _appOptions;

    public ApplicationUserAppService(
        IdentityUserManager identityUserManager,
        IIdentityUserRepository userRepository,
        IIdentityRoleRepository roleRepository,
        IdentityRoleManager identityRoleManager,
        IOptions<AppOptions> appOptions)
    {
        _identityUserManager = identityUserManager;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _identityRoleManager = identityRoleManager;
        _appOptions = appOptions.Value;
    }

    [AllowAnonymous]
    public virtual async Task<ApplicationUserOutput> RegisterUserAsync(RegisterInput input)
    {
        var userEmail = await _identityUserManager.FindByEmailAsync(input.Email);

        if (userEmail is not null)
        {
            throw new EntityAlreadyExistsException("Email Already Exists");
        }

        var userRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.User);

        if (userRole is null)
        {
            throw new EntityNotFoundException(typeof(ApplicationRole), ApplicationRole.User);
        }

        var firstName = RemoveDiacritics(input.FirstName.ToLower());
        var lastName = RemoveDiacritics(input.LastName.ToLower());
        var userName = firstName + lastName;

        var avatar = $"{_appOptions.SelfUrl}/avatar/user.png";

        var user = new IdentityUser(GuidGenerator.Create(), userName, input.Email)
        {
            Name = input.FirstName,
            Surname = input.LastName
        };

        user.SetIsActive(false);
        user.SetAvatarUrl(avatar);
        user.AddRole(userRole.Id);

        var result = await _identityUserManager.CreateAsync(user, input.Password);

        result.ThrowIfInvalid();

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }

    [AllowAnonymous]
    public virtual async Task<ApplicationUserOutput> UpdateCurrentUser(UpdateCurrentUserInput input)
    {
        var currentUserId = CurrentUser.Id ?? Guid.Empty;

        var user = await _identityUserManager.GetByIdAsync(currentUserId);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), currentUserId);
        }

        user.Name = input.FirstName;
        user.Surname = input.LastName;
        user.SetDateOfBirth(input.DateOfBirth);
        user.SetCountry(input.Country);
        user.SetPostCode(input.PostCode);
        user.SetCity(input.City);
        user.SetStreet(input.Street);

        var result = await _identityUserManager.UpdateAsync(user);

        result.ThrowIfInvalid();

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }



    public async Task<IList<ApplicationUserOutput>> GetAllAsync()
    {
        var applicationUsers = await _userRepository.GetListAsync(includeDetails: true);

        return ObjectMapper.Map<IList<IdentityUser>, IList<ApplicationUserOutput>>(applicationUsers);
    }

    public async Task<ApplicationUserOutput> GetCurrentUser()
    {
        var currentUserId = CurrentUser.Id ?? Guid.Empty;

        var user = await _identityUserManager.GetByIdAsync(currentUserId);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), currentUserId);
        }

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }
    
    public async Task<ApplicationUserOutput> GetUserAsync(Guid userId)
    {
        var user = await _identityUserManager.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), userId);
        }

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }

    public virtual async Task<ApplicationUserOutput> CreateUserAsync(CreateApplicationUserInput input)
    {
        var firstName = RemoveDiacritics(input.FirstName.ToLower());
        var lastName = RemoveDiacritics(input.LastName.ToLower());
        var userName = firstName + lastName;

        var avatar = $"{_appOptions.SelfUrl}/avatar/avatar.png";

        var user = new IdentityUser(GuidGenerator.Create(), userName, input.Email)
        {
            Name = input.FirstName,
            Surname = input.LastName
        };

        user.SetIsActive(true);
        user.SetEmailConfirmed(true);
        user.SetPhoneNumber(input.PhoneNumber, true);
        user.SetAvatarUrl(avatar);
        user.SetDateOfBirth(input.DateOfBirth);
        user.SetCountry(input.Country);
        user.SetPostCode(input.PostCode);
        user.SetCity(input.City);
        user.SetStreet(input.Street);

        foreach (var roleId in input.RoleIds)
        {
            user.AddRole(roleId);
        }

        var result = await _identityUserManager.CreateAsync(user, input.Password);

        result.ThrowIfInvalid();

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }

    public virtual async Task<ApplicationUserOutput> UpdateUserAsync(Guid userId, UpdateApplicationUserInput input)
    {
        var user = await _identityUserManager.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), userId);
        }

        user.Name = input.FirstName;
        user.Surname = input.LastName;
        user.SetDateOfBirth(input.DateOfBirth);
        user.SetPhoneNumber(input.PhoneNumber, true);
        user.SetCountry(input.Country);
        user.SetPostCode(input.PostCode);
        user.SetCity(input.City);
        user.SetStreet(input.Street);
        user.SetIsActive(input.IsActive);

        user.Roles.Clear();

        foreach (var roleId in input.RoleIds)
        {
            user.AddRole(roleId);
        }

        var result = await _identityUserManager.UpdateAsync(user);

        result.ThrowIfInvalid();

        return ObjectMapper.Map<IdentityUser, ApplicationUserOutput>(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _identityUserManager.GetByIdAsync(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), userId);
        }

        await _userRepository.HardDeleteAsync(user);

        //var result = await _identityUserManager.DeleteAsync(user);
        //result.ThrowIfInvalid();
    }

    public virtual async Task UserChangePassword(Guid id, ChangePasswordInput input)
    {
        var user = await _identityUserManager.GetByIdAsync(id);

        if (user is null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), id);
        }

        var token = await _identityUserManager.GeneratePasswordResetTokenAsync(user);

        var result = await _identityUserManager.ResetPasswordAsync(user, token, input.NewPassword);

        result.ThrowIfInvalid();
    }

    public async Task<IList<RoleOutput>> GetAllRoles()
    {
        var roles = await _roleRepository.GetListAsync();

        return ObjectMapper.Map<IList<IdentityRole>, IList<RoleOutput>>(roles);
    }

    public async Task<RoleOutput> GetRoleAsync(Guid roleId)
    {
        var role = await _roleRepository.GetAsync(roleId);

        if (role is null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), roleId);
        }

        return ObjectMapper.Map<IdentityRole, RoleOutput>(role);
    }



    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}