using Volo.Abp;

namespace No1.Exceptions;

public  class InvalidAverageRatingsNumber : BusinessException, IUserFriendlyException
{
    public InvalidAverageRatingsNumber() : this("Invalid average ratings number")
    {
    }

    public InvalidAverageRatingsNumber(string message) : base(No1DomainErrorCodes.InvalidAverageRatingsNumber, message)
    {
    }
}