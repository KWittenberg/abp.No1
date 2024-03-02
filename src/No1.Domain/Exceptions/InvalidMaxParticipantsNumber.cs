using Volo.Abp;

namespace No1.Exceptions;

public  class InvalidMaxParticipantsNumber : BusinessException, IUserFriendlyException
{
    public InvalidMaxParticipantsNumber() : this("Invalid max participants number")
    {
    }

    public InvalidMaxParticipantsNumber(string message) : base(No1DomainErrorCodes.InvalidMaxParticipantsNumber, message)
    {
    }
}