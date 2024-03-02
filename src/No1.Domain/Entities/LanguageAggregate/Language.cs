using System;
using System.Text.RegularExpressions;
using No1.Exceptions;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Entities.LanguageAggregate;

public class Language : AuditedAggregateRoot<Guid>
{
    private const string LocaleRegexPattern = @"^[a-z]{2}(-[A-Z]{2})?$";
    
    protected Language()
    {
    }

    public Language(Guid id, string name, string locale) : base(id)
    {
        SetName(name);
        SetLocale(locale);
    }

    public string Name { get; set; }

    public string Locale { get; set; }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), LanguageConsts.NameLength);
    }

    public void SetLocale(string locale)
    {
        if (!Regex.IsMatch(locale, LocaleRegexPattern))
        {
            throw new InvalidLocaleFormatException();
        }

        Locale = Check.NotNullOrWhiteSpace(locale, nameof(locale), LanguageConsts.LocaleLength);
    }
}