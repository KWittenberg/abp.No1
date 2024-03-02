using Volo.Abp;

namespace No1.Exceptions;

public class InvalidLocaleFormatException : BusinessException, IUserFriendlyException
{
    public InvalidLocaleFormatException() : this("Invalid locale format exception")
    {
    }

    public InvalidLocaleFormatException(string message) : base(No1DomainErrorCodes.InvalidLocaleFormatException, message)
    {
    }
}