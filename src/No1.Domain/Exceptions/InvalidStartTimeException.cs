using Volo.Abp;

namespace No1.Exceptions;

internal class InvalidStartTimeException : BusinessException, IUserFriendlyException
{
    public InvalidStartTimeException() : this("Invalid start time.")
    {
    }

    public InvalidStartTimeException(string message) : base(No1DomainErrorCodes.InvalidStartTime, message)
    {
    }
}