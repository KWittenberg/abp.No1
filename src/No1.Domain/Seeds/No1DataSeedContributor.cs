using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using No1.Entities.CategoryAggregate;
using No1.Entities.CountryAggregate;
using No1.Entities.LanguageAggregate;
using No1.Extensions;
using No1.Interfaces;
using No1.Options;
using No1.Role;
using No1.Seeds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace No1.Seeds;

public class No1DataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private const string AdminUserNameDefaultValue = "admin";
    private const string AdminEmailDefaultValue = "kejo@net.hr";
    private const string AdminPasswordDefaultValue = "1q2w3E*";
    private const string UserPasswordDefaultValue = "123Qwe#";

    private readonly IGuidGenerator _guidGenerator;
    private readonly ICountryRepository _countryRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IdentityRoleManager _identityRoleManager;
    private readonly IdentityUserManager _identityUserManager;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly AppOptions _appOptions;

    public No1DataSeedContributor(
        IdentityUserManager identityUserManager,
        IdentityRoleManager identityRoleManager,
        IIdentityUserRepository userRepository,
        IIdentityRoleRepository roleRepository,
        IGuidGenerator guidGenerator,
        ICountryRepository countryRepository,
        ILanguageRepository languageRepository,
        ICategoryRepository categoryRepository,
        IOptions<AppOptions> appOptions)
    {
        _identityUserManager = identityUserManager;
        _identityRoleManager = identityRoleManager;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _guidGenerator = guidGenerator;
        _countryRepository = countryRepository;
        _languageRepository = languageRepository;
        _categoryRepository = categoryRepository;
        _appOptions = appOptions.Value;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await SeedRolesAsync();

        await SeedAdminAsync();

        await SeedLanguage("./Seeds/Data/Languages.json");

        //await SeedCountries("./Seeds/Data/Countries.json");

        await SeedCountriesAndCities("./Seeds/Data/CountriesAndCities.json");

        await SeedDemoUsersAsync("./Seeds/Data/DemoUsers.json", 10);

        await SeedCategories("./Seeds/Data/Categories.json");
    }

    private async Task SeedRolesAsync()
    {
        var roles = new List<IdentityRole>();

        var adminRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.Admin);
        var userRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.User);
        var testRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.Test);

        if (adminRole is null)
        {
            roles.Add(new IdentityRole(_guidGenerator.Create(), ApplicationRole.Admin)
            {
                IsStatic = true,
                IsPublic = true
            });
        }

        if (userRole is null)
        {
            roles.Add(new IdentityRole(_guidGenerator.Create(), ApplicationRole.User)
            {
                IsStatic = true,
                IsPublic = true
            });
        }

        if (testRole is null)
        {
            roles.Add(new IdentityRole(_guidGenerator.Create(), ApplicationRole.Test)
            {
                IsStatic = false,
                IsPublic = true
            });
        }

        if (roles.Count > 0)
        {
            await _roleRepository.InsertManyAsync(roles, true);
        }

        var roleDataCount = await _roleRepository.GetCountAsync();

        if (roleDataCount == roles.Count)
        {
            var timeNow = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"{timeNow} | SEED: Roles OK - Added {roleDataCount} roles");
        }

    }

    private async Task SeedAdminAsync()
    {
        var userCount = await _userRepository.GetCountAsync();

        if (userCount > 0)
        {
            return;
        }

        var adminRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.Admin);

        if (adminRole is null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), ApplicationRole.Admin);
        }

        var avatar = $"{_appOptions.SelfUrl}/avatar/admin.png";

        var user = new IdentityUser(_guidGenerator.Create(), AdminUserNameDefaultValue, AdminEmailDefaultValue)
        {
            Name = "Krešimir",
            Surname = "Wittenberg"
        };

        user.AddRole(adminRole.Id);
        user.SetEmailConfirmed(true);
        user.SetPhoneNumber("+38598870888", true);
        user.SetAvatarUrl(avatar);
        user.SetDateOfBirth(new DateTime(1973, 10, 11));
        user.SetCountry("Croatia");
        user.SetPostCode("34000");
        user.SetCity("Požega");
        user.SetStreet("J.J. Strossmayera 4");

        await _identityUserManager.CreateAsync(user, AdminPasswordDefaultValue);

        var userAdminCount = await _userRepository.GetCountAsync();

        if (userAdminCount > 0)
        {
            var timeNow = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"{timeNow} | SEED: User OK - Added Admin {user.Name}");
        }
    }

    private async Task SeedLanguage(string filePath)
    {
        if (await _languageRepository.GetCountAsync() <= 0)
        {
            var jsonText = await File.ReadAllTextAsync(filePath);
            var languages = JsonConvert.DeserializeObject<List<Language>>(jsonText);

            foreach (var language in languages.Select(data =>
                         new Language(_guidGenerator.Create(), data.Name, data.Locale)))
            {
                await _languageRepository.InsertAsync(language);
            }

            var languageDataCount = await _languageRepository.GetCountAsync();
            var expectedLanguageCount = languages.Count;

            if (languageDataCount == expectedLanguageCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"{timeNow} | SEED: Languages OK - Added {languageDataCount} records");
            }
        }
    }

    private async Task SeedCountries(string filePath)
    {
        if (await _countryRepository.GetCountAsync() <= 0)
        {
            var jsonText = await File.ReadAllTextAsync(filePath);
            var countryNames = JsonConvert.DeserializeObject<List<string>>(jsonText);

            foreach (var country in countryNames.Select(
                         countryName => new Country(_guidGenerator.Create(), countryName)))
            {
                await _countryRepository.InsertAsync(country);
            }

            var countryDataCount = await _countryRepository.GetCountAsync();
            var expectedCountryCount = countryNames.Count;

            if (countryDataCount == expectedCountryCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"{timeNow} | SEED: Countries OK - Added {countryDataCount} records");
            }
        }
    }

    private async Task SeedCountriesAndCities(string filePath)
    {
        if (await _countryRepository.GetCountAsync() <= 0)
        {
            var jsonText = await File.ReadAllTextAsync(filePath);
            var data = JsonConvert.DeserializeObject<List<SeedCountriesAndCities>>(jsonText);

            foreach (var countryData in data)
            {
                var country = new Country(_guidGenerator.Create(), countryData.Name);

                foreach (var translationData in countryData.Translations)
                {
                    country.Translations.Add(new CountryTranslation
                    {
                        Language = translationData.Language,
                        Name = translationData.Name
                    });
                }

                foreach (var city in countryData.Cities.Select(cityData => new City(
                             _guidGenerator.Create(),
                             cityData.Name,
                             cityData.PostCode,
                             cityData.Longitude,
                             cityData.Latitude)))
                {
                    country.Cities.Add(city);
                }

                await _countryRepository.InsertAsync(country);
            }

            var expectedCountryCount = data.Count;
            var countryDataCount = await _countryRepository.GetCountAsync();

            if (countryDataCount == expectedCountryCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"{timeNow} | SEED: Countries OK - Added {countryDataCount} records");
            }

            var expectedCountryTranslationCount = data.SelectMany(c => c.Translations).Count();
            var countryTranslations = await _countryRepository.GetCountryTranslationsAsync();
            var countryTranslationDataCount = countryTranslations.Count;

            if (countryTranslationDataCount == expectedCountryTranslationCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine(
                    $"{timeNow} | SEED: CountryTranslation OK - Added {countryTranslationDataCount} records");
            }

            var expectedCityCount = data.SelectMany(c => c.Cities).Count();
            var cities = await _countryRepository.GetCitiesAsync();
            var cityDataCount = cities.Count;

            if (cityDataCount == expectedCityCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"{timeNow} | SEED: Cities OK - Added {cityDataCount} records");
            }
        }
    }

    private async Task SeedDemoUsersAsync(string filePath, int numberOfUsers)
    {
        var userRole = await _identityRoleManager.FindByNameAsync(ApplicationRole.User);

        if (userRole is null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), ApplicationRole.User);
        }

        var normalUserExists = await _identityUserManager.GetUsersInRoleAsync(userRole.Name);

        var random = new Random();

        if (normalUserExists.Count == 0)
        {
            var jsonText = await File.ReadAllTextAsync(filePath);
            var data = JsonConvert.DeserializeObject<SeedDemoUsers>(jsonText);

            var cities = await _countryRepository.GetCitiesAsync();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var randomName = random.Next(data.FirstName.Count);
                var firstName = data.FirstName[randomName];
                var lastName = data.LastName[randomName];
                var userName = $"{firstName.ToLower()}{lastName.ToLower()}";

                var email = $"{firstName.ToLower()}.{lastName.ToLower()}@gmail.com";
                var phoneNumber = $"+38598{random.Next(100000, 999999)}";

                var avatar = $"{_appOptions.SelfUrl}/avatar/user.png";

                var day = random.Next(1, 31);
                var month = random.Next(1, 13);
                var year = random.Next(1990, 2011);
                var dob = new DateTime(year, month, day);

                var randomCity = cities[random.Next(0, (cities.Count))];
                var postCode = randomCity.PostCode;
                var cityName = randomCity.Name;
                var countryId = randomCity.CountryId;

                var country = await _countryRepository.GetAsync(c => c.Id == countryId);
                //var country = await _countryRepository.GetCountryAsync(countryId);
                var countryName = country.Name;

                var street = data.Street[random.Next(data.Street.Count)] + " " + random.Next(1, 200);

                var user = new IdentityUser(_guidGenerator.Create(), userName, email)
                {
                    Name = firstName,
                    Surname = lastName
                };

                user.AddRole(userRole.Id);
                user.SetEmailConfirmed(true);
                user.SetPhoneNumber(phoneNumber, true);
                user.SetAvatarUrl(avatar);
                user.SetDateOfBirth(dob);
                user.SetCountry(countryName);
                user.SetPostCode(postCode);
                user.SetCity(cityName);
                user.SetStreet(street);

                await _identityUserManager.CreateAsync(user, UserPasswordDefaultValue);
            }

        }

        var usersInUserRole = await _identityUserManager.GetUsersInRoleAsync(userRole.Name);
        var userDataCount = usersInUserRole.Count;

        if (userDataCount > 0)
        {
            var timeNow = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"{timeNow} | SEED: Users OK - Added {userDataCount} records");
        }
    }

    private async Task SeedCategories(string filePath)
    {
        if (await _categoryRepository.GetCountAsync() <= 0)
        {
            var jsonText = await File.ReadAllTextAsync(filePath);
            var categories = JsonConvert.DeserializeObject<List<Category>>(jsonText);

            foreach (var category in categories.Select(data =>
                         new Category(_guidGenerator.Create(), data.Name, data.Description)))
            {
                await _categoryRepository.InsertAsync(category);
            }

            var categoryDataCount = await _categoryRepository.GetCountAsync();
            var expectedCategoryCount = categories.Count;

            if (categoryDataCount == expectedCategoryCount)
            {
                var timeNow = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"{timeNow} | SEED: Categories OK - Added {categoryDataCount} records");
            }
        }
    }
}