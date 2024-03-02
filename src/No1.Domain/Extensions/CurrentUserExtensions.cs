using No1.Interfaces;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace No1.Extensions;

public class CurrentUserExtensions : CurrentUser, ICurrentUserExtensions
{
    public CurrentUserExtensions(ICurrentPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }

    public string? AvatarUrl { get; }

    //public GetAvatarUrl();



    //public static string? GetAvatarUrl(this CurrentUser currentUser) => currentUser.GetProperty<string?>("AvatarUrl");







    //private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

    //public T GetProperty<T>(string propertyName)
    //{
    //    if (_properties.TryGetValue(propertyName, out var value) && value is T typedValue)
    //    {
    //        return typedValue;
    //    }

    //    return default(T);
    //}

    //public void SetProperty<T>(string propertyName, T value)
    //{
    //    _properties[propertyName] = value;
    //}




}