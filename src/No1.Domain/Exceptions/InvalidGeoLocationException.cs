using Volo.Abp;

namespace No1.Exceptions;

public class InvalidGeoLocationException : BusinessException, IUserFriendlyException
{
    public InvalidGeoLocationException() : this("Invalid GeoLocation")
    {
    }

    public InvalidGeoLocationException(string message) : base(No1DomainErrorCodes.InvalidGeoLocation, message)
    {
    }
}