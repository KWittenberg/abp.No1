using Volo.Abp;

namespace No1.Exceptions;

public class InvalidDurationException : BusinessException, IUserFriendlyException
{
    public InvalidDurationException() : this("Invalid duration")
    {
    }

    public InvalidDurationException(string message) : base(No1DomainErrorCodes.InvalidDuration, message)
    {
    }
}