using Volo.Abp;

namespace No1.Exceptions;

public class InvalidResponseException : BusinessException, IUserFriendlyException
{
    public InvalidResponseException() : this("Invalid Response")
    {
    }

    public InvalidResponseException(string message) : base(No1DomainErrorCodes.InvalidResponse, "Invalid Response", message)
    {
    }

    public InvalidResponseException(string message, string details) : base(No1DomainErrorCodes.InvalidResponse, message, details)
    {
    }
}