using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using No1.MultiTenancy;
using No1.Options;
using No1.Settings;
using System;
using System.Collections.Generic;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.TextTemplating.Razor;
using Volo.Abp.Timing;
using Volo.Abp.VirtualFileSystem;

namespace No1;

[DependsOn(
    typeof(No1DomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(AbpEmailingModule),
    typeof(AbpTextTemplatingRazorModule)
    )]
public class No1DomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureMultiTenancy();
        ConfigureLocalization();
        ConfigureClock();
        ConfigureVirtualFileSystem();

        // Remove BultIn IdentityDataSeedContributor Services
        ConfigureIdentity(context);

        // Get Data From appsetings.json section Smtp to SmtpOptions
        ConfigureEmailSmtpOptions(context);
        ConfigureRazorTemplates();

        // For SelfUrl
        ConfigureApp(context);

        // Get Data From appsetings.json section 'Crm' to 'CrmOptions'
        ConfigureCrm(context);
        ConfigureSms(context);
        ConfigureOpenWeather(context);
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("hr", "hr", "Croatian"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский", "ru"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe", "tr"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }

    private void ConfigureClock()
    {
        Configure<AbpClockOptions>(options => { options.Kind = DateTimeKind.Utc; });
    }

    private void ConfigureVirtualFileSystem()
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<No1DomainModule>("No1");
        });
    }

    private void ConfigureIdentity(ServiceConfigurationContext context)
    {
        /*
        Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });
        */

        Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        context.Services.RemoveAll(t => t.ImplementationType == typeof(IdentityDataSeedContributor));
    }

    private void ConfigureEmailSmtpOptions(ServiceConfigurationContext context)
    {
        Configure<SmtpOptions>(context.Services.GetConfiguration().GetSection(SmtpOptions.Section));

        context.Services.Replace(ServiceDescriptor.Transient<ISmtpEmailSenderConfiguration, ApplicationSmtpEmailSenderConfiguration>());

#if DEBUG
        // context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }

    private void ConfigureRazorTemplates()
    {
        Configure<AbpRazorTemplateCSharpCompilerOptions>(options =>
        {
            options.References.Add(MetadataReference.CreateFromFile(typeof(No1DomainModule).Assembly.Location));
        });
    }

    private void ConfigureApp(ServiceConfigurationContext context)
    {
        Configure<AppOptions>(context.Services.GetConfiguration().GetSection(AppOptions.Section));
    }

    private void ConfigureCrm(ServiceConfigurationContext context)
    {
        Configure<CrmOptions>(context.Services.GetConfiguration().GetSection(CrmOptions.Section));
    }

    private void ConfigureSms(ServiceConfigurationContext context)
    {
        Configure<SmsOptions>(context.Services.GetConfiguration().GetSection(SmsOptions.Section));
    }

    private void ConfigureOpenWeather(ServiceConfigurationContext context)
    {
        Configure<OpenWeatherOptions>(context.Services.GetConfiguration().GetSection(OpenWeatherOptions.Section));
    }
}