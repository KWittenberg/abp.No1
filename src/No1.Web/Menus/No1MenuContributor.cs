using System.Threading.Tasks;
using No1.Localization;
using No1.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace No1.Web.Menus;

public class No1MenuContributor : IMenuContributor
{
	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
	}

	private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		var administration = context.Menu.GetAdministration();
		var l = context.GetLocalizer<No1Resource>();

		context.Menu.Items.Insert(0, new ApplicationMenuItem(No1Menus.Home, l["Menu:Home"], "~/", icon: "fas fa-home", order: 0));
		context.Menu.Items.Insert(1, new ApplicationMenuItem(No1Menus.OpenWeather, l["Menu:OpenWeather"], "~/OpenWeather", icon: "bi bi-cloud", order: 0));

		context.Menu
			.AddItem(new ApplicationMenuItem(No1Menus.Admin, l["Menu:Admin"], icon: "fas fa-user", order: 1)
				.AddItem(new ApplicationMenuItem(No1Menus.Roles, l["Menu:Roles"], url: "/Roles"))
				.AddItem(new ApplicationMenuItem(No1Menus.Roles, l["Menu:Users"], url: "/Users")));

        context.Menu
            .AddItem(new ApplicationMenuItem(No1Menus.Geolocation, l["Menu:Geolocation"], icon: "fas fa-globe", order: 2)
                .AddItem(new ApplicationMenuItem(No1Menus.Geooverview, l["Menu:Geooverview"], url: "/Geooverview"))
                .AddItem(new ApplicationMenuItem(No1Menus.Countries, l["Menu:Countries"], url: "/Countries"))
                .AddItem(new ApplicationMenuItem(No1Menus.Cities, l["Menu:Cities"], url: "/Cities")));

        context.Menu
            .AddItem(new ApplicationMenuItem(No1Menus.Items, l["Menu:Items"], icon: "fas fa-server", order: 3)
                .AddItem(new ApplicationMenuItem(No1Menus.Categories, l["Menu:Categories"], url: "~/Categories", icon: "fas fa-folder"))
                .AddItem(new ApplicationMenuItem(No1Menus.Products, l["Menu:Products"], url: "/Products", icon: "fas fa-database")));
		

        if (MultiTenancyConsts.IsEnabled)
		{
			administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
		}
		else
		{
			administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
		}

		administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
		administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

		return Task.CompletedTask;
	}
}