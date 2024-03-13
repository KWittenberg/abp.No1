using No1.Models;
using Volo.Abp;

namespace No1.Exceptions;

public class InvalidResponseException : BusinessException, IUserFriendlyException
{
    public InvalidResponseException() : this("Invalid Response")
    {
    }

    public InvalidResponseException(string message) : base(No1DomainErrorCodes.InvalidResponse, message)
    {
    }

    public InvalidResponseException(string message, string details) : base(No1DomainErrorCodes.InvalidResponse, message, details)
    {
    }

    public InvalidResponseException(string details, TmdbErrorModel data) : base(No1DomainErrorCodes.InvalidResponse, "Invalid Response", details)
    {
        WithData("TmdbResponse", data);
    }
}