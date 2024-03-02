using Volo.Abp;

namespace No1.Exceptions;

public class OibException : BusinessException, IUserFriendlyException
{
    public OibException() : this("Invalid OIB format.")
    {
    }

    public OibException(string message) : base(No1DomainErrorCodes.InvalidOibFormat, message)
    {
    }
}