using System;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace No1.Extensions;

public static class IdentityUserExtensions
{
    public static string AvatarUrlProperty = "AvatarUrl";
    public static string? GetAvatarUrl(this IdentityUser identityUser) =>
        identityUser.GetProperty<string?>(AvatarUrlProperty);
    public static void SetAvatarUrl(this IdentityUser identityUser, string avatarUrl) =>
        identityUser.SetProperty(AvatarUrlProperty, avatarUrl);
    public static void ResetAvatarUrl(this IdentityUser identityUser) =>
        identityUser.SetProperty(AvatarUrlProperty, null);


    public static string DateOfBirth = "DateOfBirth";
    public static DateTime? GetDateOfBirth(this IdentityUser identityUser) =>
        identityUser.GetProperty<DateTime?>(DateOfBirth);
    public static void SetDateOfBirth(this IdentityUser identityUser, DateTime? dateOfBirth) =>
        identityUser.SetProperty(DateOfBirth, dateOfBirth);


    public static string CountryProperty = "Country";
    public static string GetCountry(this IdentityUser identityUser) =>
        identityUser.GetProperty<string>(CountryProperty);
    public static void SetCountry(this IdentityUser identityUser, string country) =>
        identityUser.SetProperty(CountryProperty, country);


    public static string PostCodeProperty = "PostCode";
    public static string GetPostCode(this IdentityUser identityUser) =>
        identityUser.GetProperty<string>(PostCodeProperty);
    public static void SetPostCode(this IdentityUser identityUser, string postcode) =>
        identityUser.SetProperty(PostCodeProperty, postcode);


    public static string CityProperty = "City";
    public static string GetCity(this IdentityUser identityUser) =>
        identityUser.GetProperty<string>(CityProperty);
    public static void SetCity(this IdentityUser identityUser, string city) =>
        identityUser.SetProperty(CityProperty, city);


    public static string StreetProperty = "Street";
    public static string GetStreet(this IdentityUser identityUser) =>
        identityUser.GetProperty<string>(StreetProperty);
    public static void SetStreet(this IdentityUser identityUser, string street) =>
        identityUser.SetProperty(StreetProperty, street);
}