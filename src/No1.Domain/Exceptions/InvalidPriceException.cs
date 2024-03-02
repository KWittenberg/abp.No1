using Volo.Abp;

namespace No1.Exceptions;

public class InvalidPriceException : BusinessException, IUserFriendlyException
{
    public InvalidPriceException() : this("Invalid price")
    {
    }

    public InvalidPriceException(string message) : base(No1DomainErrorCodes.InvalidPrice, message)
    {
    }
}